using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace GmailNotifier
{
    public partial class Context : ApplicationContext
    {
        WindowsFormsSynchronizationContext MainSynchronizationContext;

        bool Initialized = false;

        public Context()
        {
            MainSynchronizationContext = new WindowsFormsSynchronizationContext();
            Mail.Received += new Mail.ReceivedDelegate(ReceiveCallback);
            ThreadExit += new EventHandler(Context_ThreadExit);

            UpdateTimer = new System.Timers.Timer(UpdateFrequency * 1000);
            UpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateTimer_Tick);

            InstallIcon();

            LoadSettings();

            UpdateState();
        }

        public void UpdateState()
        {
            if (!Mail.Settings.Present)
            {
                UpdateTimer.Stop();
                Mail.Emails.Clear();
                Icon.Icon = Properties.Resources.Error;
                SetIconStatus("Not configured");
            }
            else
            {
                UpdateTimer.Start();
                RunUpdate();
            }
        }

        void OpenMail_Click(object sender, EventArgs e)
        {
            Process.Start("https://mail.google.com/mail/#inbox");
        }

        void ShowUnread_Click(object sender, EventArgs e)
        {
            ShowNotification(Mail.Emails);
        }

        void CheckNow_Click(object sender, EventArgs e)
        {
            RunUpdate();
        }

        void Settings_Click(object sender, EventArgs e)
        {
            EditSettings();
        }

        void Exit_Click(object sender, EventArgs e)
        {
            ExitThread();
        }

        void UpdateTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            RunUpdate();
        }

        void Context_ThreadExit(object sender, EventArgs e)
        {
            RemoveIcon();
        }

        void ReceiveCallback(List<Email> Emails)
        {
            if (Emails.Count > 0)
            {
                Tools.PlayNotificationSound();
            }

            ShowNotification(Emails);
        }
    }
}