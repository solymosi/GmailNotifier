using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MailNotifier
{
    public partial class Context
    {
        WindowsFormsSynchronizationContext UpdateSynchronizationContext;
        Thread UpdateThread;

        public bool Updating { get { return this.UpdateThread != null && (this.UpdateThread.ThreadState == ThreadState.Running || this.UpdateThread.ThreadState == ThreadState.Background); } }

        public void RunCheck()
        {
            if (this.Updating) { return; }
            UpdateThread = new Thread(RunUpdateThread);
            UpdateThread.Name = "Update Thread";
            UpdateThread.IsBackground = true;
            UpdateThread.Priority = ThreadPriority.Lowest;
            UpdateThread.Start();
        }

        void RunUpdateThread()
        {
            UpdateSynchronizationContext = new WindowsFormsSynchronizationContext();
            try
            {
                Gmail.RunCheck();

                Icon.Icon = Gmail.Emails.Count > 0 ? Properties.Resources.Unread : Properties.Resources.Read;

                SetIconStatus(Gmail.Emails.Count == 0 ? "No unread mail" : Gmail.Emails.Count.ToString() + " unread mail");
            }
            catch (GmailException ex)
            {
                Icon.Icon = Properties.Resources.Problem;

                if (ex.Type == GmailException.ErrorType.AccountError)
                {
                    SetIconStatus("Invalid credentials");
                }
                else
                {
                    SetIconStatus(ex.Message);
                }
            }
            catch(Exception ex)
            {
                Tools.ErrorMessage("An unknown error occured while trying to check for new mail:\r\n" + ex.Message);
            }
        }
    }
}
