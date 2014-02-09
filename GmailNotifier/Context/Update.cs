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
        WindowsFormsSynchronizationContext UpdateSynchronizationContext;
        Thread UpdateThread;

        public bool Updating
        {
            get { return this.UpdateThread != null && (this.UpdateThread.ThreadState == ThreadState.Running || this.UpdateThread.ThreadState == ThreadState.Background); }
        }

        public void RunUpdate()
        {
            if (this.Updating) { return; }
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
            }
            catch (UnauthorizedAccessException)
            {
                UpdateIcon(Properties.Resources.Error, "Invalid credentials");
                ShowNotification("Invalid credentials", "The configured email address or password is invalid. Right-click the Gmail Notifier icon and choose Settings to update your credentials.");
            }
            catch(Exception e)
            {
                UpdateIcon(Properties.Resources.Error, e.Message != null && e.Message != "" ? e.Message : "Unable to check mail");
                ShowNotification("Your inbox could not be checked", e.Message != null && e.Message != "" ? e.Message : "An unknown error occured.");
            }
        }
    }
}