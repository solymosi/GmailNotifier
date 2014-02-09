using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MailNotifier
{
    public partial class Context
    {
        public NotificationForm Notification;

        void ShowNotification(List<Email> Emails)
        {
            MainSynchronizationContext.Post(new SendOrPostCallback(delegate
            {
                if (Emails.Count == 0) { return; }
                if (Notification != null && Notification.Visible) { Notification.Close(); }
                Notification = new NotificationForm();
                Notification.Emails = Emails;
                Notification.Show();
            }), new object());
        }
    }
}
