﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Net;
using System.Linq;
using System.Xml.Linq;

namespace GmailNotifier
{
    static partial class Mail
    {
        public const string FeedAddress = "https://mail.google.com/mail/feed/atom/";
        public const string InboxAddress = "https://mail.google.com/mail/#inbox";
        public const string AppPasswordAddress = "https://accounts.google.com/b/0/IssuedAuthSubTokens?hide_authsub=1";

        public static Settings Settings = new Settings();
        public static List<Email> Emails = new List<Email>();

        public static DateTime LastModified;

        public delegate void ReceivedDelegate(List<Email> Emails);
        public static event ReceivedDelegate Received = delegate { };

        public static void RunUpdate()
        {
            XmlReader Reader = null;

            try
            {
                Reader = XmlReader.Create(FeedAddress, new XmlReaderSettings
                {
                    XmlResolver = new XmlUrlResolver
                    {
                        Credentials = new NetworkCredential(Settings.User, Settings.Password)
                    }
                });
            }
            catch (WebException e)
            {
                if (e.Response != null && ((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            XDocument Document = XDocument.Load(Reader);
            XNamespace Namespace = Document.Root.Attribute("xmlns").Value;

            DateTime Modified = DateTime.Parse(Document.Root.Element(Namespace + "modified").Value);

            if (LastModified == Modified)
            {
                return;
            }

            LastModified = Modified;

            List<Email> Unread = new List<Email>();

            foreach (XElement Entry in Document.Root.Elements(Namespace + "entry"))
            {
                Unread.Add(new Email
                {
                    ID = Entry.Element(Namespace + "id").Value,
                    Subject = Entry.Element(Namespace + "title").Value,
                    Sender = Entry.Element(Namespace + "author").Element(Namespace + "name").Value,
                    From = Entry.Element(Namespace + "author").Element(Namespace + "email").Value,
                    Body = Entry.Element(Namespace + "summary").Value,
                    Date = DateTime.Parse(Entry.Element(Namespace + "issued").Value),
                    Link = Entry.Element(Namespace + "link").Attribute("href").Value,
                    Modified = DateTime.Parse(Entry.Element(Namespace + "modified").Value),
                });
            }

            List<Email> New = Unread.Where(e => Emails.Where(i => i.ID == e.ID).Count() == 0).ToList();

            if(New.Count() > 0)
            {
                Received(New);
            }

            Emails = Unread;
        }
    }
}