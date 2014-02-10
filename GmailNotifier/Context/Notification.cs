using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace GmailNotifier
{
    public partial class Context
    {
        public NotificationForm Notification;

        void ShowNotification(List<Email> Emails) { ShowNotification(Emails, 5000); }
        void ShowNotification(List<Email> Emails, int Duration)
        {
            List<string> Messages = new List<string>();

            int ID = 1;
            foreach (Email Email in Emails)
            {
                RichTextBox Box = new RichTextBox();
                Box.Font = new Font("Tahoma", 8.25F);
                Box.AppendText(ID.ToString() + " of " + Emails.Count.ToString() + " » " + Tools.FormatDate(Email.Date) + " ");
                Box.SelectionFont = new Font(Box.Font, FontStyle.Bold);
                Box.AppendText(Email.Subject);
                Box.SelectionFont = Box.Font;
                Box.AppendText("\r\n" + Email.Body);

                Messages.Add(Box.Rtf);

                ID++;
            }

            ShowNotification(Messages, Duration);
        }

        void ShowNotification(string Header, string Body) { ShowNotification(Header, Body, 15000); }
        void ShowNotification(string Header, string Body, int Duration)
        {
            List<string> Messages = new List<string>();

            RichTextBox Box = new RichTextBox();
            Box.Font = new Font("Tahoma", 8.25F);
            Box.SelectionColor = Color.FromArgb(204, 0, 0);
            Box.SelectionFont = new Font(Box.Font, FontStyle.Bold);
            Box.AppendText(Header);
            Box.SelectionColor = Box.ForeColor;
            Box.SelectionFont = Box.Font;
            Box.AppendText("\r\n" + Body);

            Messages.Add(Box.Rtf);

            ShowNotification(Messages, Duration);
        }

        void ShowNotification(List<string> Messages, int Duration)
        {
            MainSynchronizationContext.Post(new SendOrPostCallback(delegate
            {
                if (Notification != null && Notification.Visible)
                {
                    Notification.Close();
                }

                Notification = new NotificationForm();
                Notification.Messages.AddRange(Messages);
                Notification.DisplayTimer.Interval = Duration;
                Notification.Show();
            }), null);
        }
    }
}
