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
        public const int UpdateFrequency = 60;

        WindowsFormsSynchronizationContext UpdateSynchronizationContext;
        System.Timers.Timer UpdateTimer;
        Thread UpdateThread;

        Response LastResult = Response.None;

        public bool Updating
        {
            get
            {
                return this.UpdateThread != null && (this.UpdateThread.ThreadState == ThreadState.Running || this.UpdateThread.ThreadState == ThreadState.Background);
            }
        }

        public void RunUpdate()
        {
            if (this.Updating)
            {
                return;
            }

            UpdateThread = new Thread(RunUpdateThread);
            UpdateThread.IsBackground = true;
            UpdateThread.Priority = ThreadPriority.Lowest;
            UpdateThread.Start();
        }

        void RunUpdateThread()
        {
            UpdateSynchronizationContext = new WindowsFormsSynchronizationContext();

            try
            {
                Mail.RunUpdate();

                Icon.Icon = Mail.Emails.Count > 0 ? Properties.Resources.Unread : Properties.Resources.Read;
                SetIconStatus(Mail.Emails.Count == 0 ? "No unread mail" : Mail.Emails.Count.ToString() + " unread mail");
                LastResult = Response.Success;
            }
            catch (UnauthorizedAccessException)
            {
                UpdateIcon(Properties.Resources.Error, "Invalid credentials");

                if (LastResult != Response.AccessDenied)
                {
                    ShowNotification("Invalid credentials", "The configured email address or password is invalid. Right-click the Gmail Notifier icon and choose Settings to update your credentials.");
                    LastResult = Response.AccessDenied;
                }
            }
            catch (Exception)
            {
                UpdateIcon(Properties.Resources.Error, "Unable to load your inbox");

                if (LastResult != Response.Error)
                {
                    ShowNotification("Unable to load your inbox", "Gmail Notifier was unable to load the contents of your inbox. Make sure you are connected to the internet and verify that you can reach Gmail from your browser.");
                    LastResult = Response.Error;
                }
            }
        }

        enum Response { None, Success, AccessDenied, Error }
    }
}