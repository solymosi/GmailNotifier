using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GmailNotifier
{
    public partial class Context
    {
        public NotificationForm Notification;

        void ShowNotification(List<Email> Emails)
        {
            List<string> Messages = new List<string>();

            int ID = 1;
            foreach (Email Email in Emails)
            {
                Messages.Add(
                    @"{\rtf1\ansi\deff0" + "\r\n" +
                    ID.ToString() + " of " + Emails.Count.ToString() + " » " + Tools.FormatDate(Email.Date) + @" \b " + Tools.EscapeRtf(Email.Subject) + @" \b0 \line" + "\r\n" +
                    Tools.EscapeRtf(Email.Body) + "\r\n" +
                    "}"
                );

                ID++;
            }

            ShowNotification(Messages);
        }

        void ShowNotification(string Header, string Body)
        {
            List<string> Messages = new List<string>();

            Messages.Add(
                @"{\rtf1\ansi\deff0" + "\r\n" +
                @"\b " + Tools.EscapeRtf(Header) + @" \b0 \line" + "\r\n" +
                Tools.EscapeRtf(Body) + "\r\n" +
                "}"
            );

            ShowNotification(Messages);
        }

        void ShowNotification(List<string> Messages)
        {
            MainSynchronizationContext.Post(new SendOrPostCallback(delegate
            {
                if (Notification != null && Notification.Visible)
                {
                    Notification.Close();
                }

                Notification = new NotificationForm();
                Notification.Messages.AddRange(Messages);
                Notification.Show();
            }), null);
        }
    }
}
