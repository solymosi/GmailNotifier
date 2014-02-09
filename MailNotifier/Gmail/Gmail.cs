using System;
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

namespace MailNotifier
{
    static partial class Gmail
    {
        public static Settings Settings = new Settings();
        public static List<Email> Emails = new List<Email>();
        public static DateTime LastModified;

        public delegate void ReceivedDelegate(List<Email> Emails);
        public static event ReceivedDelegate Received = delegate { };

        public static void RunCheck()
        {
            XmlReader Reader = null;

            try
            {
                Reader = XmlReader.Create(Settings.ServerAddress, new XmlReaderSettings
                {
                    XmlResolver = new XmlUrlResolver
                    {
                        Credentials = new NetworkCredential(Settings.User, Settings.Password)
                    }
                });
            }
            catch (WebException ex)
            {
                if (ex.Response != null && ((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new GmailException(GmailException.ErrorType.AccountError, "Invalid user name or password.");
                }
                else
                {
                    throw new GmailException(GmailException.ErrorType.NetworkError, ex.Message);
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
                    FromName = Entry.Element(Namespace + "author").Element(Namespace + "name").Value,
                    FromAddress = Entry.Element(Namespace + "author").Element(Namespace + "email").Value,
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

    public class GmailException : Exception
    {
        public ErrorType Type { get; set; }

        public GmailException(ErrorType Type, string Message)
            : base(Message)
        {
            this.Type = Type;
        }

        public enum ErrorType { NetworkError, AccountError }
    }
}