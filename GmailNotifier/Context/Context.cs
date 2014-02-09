using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace GmailNotifier
{
    public partial class Context : ApplicationContext
    {
        public const int UpdateFrequency = 60;

        System.Timers.Timer UpdateTimer;
        WindowsFormsSynchronizationContext MainSynchronizationContext;

        bool Initialized = false;

        public Context()
        {
            MainSynchronizationContext = new WindowsFormsSynchronizationContext();
            ThreadExit += new EventHandler(Context_ThreadExit);

            InstallIcon();

            try
            {
                LoadSettings();
            }
            catch
            {
                Icon.Icon = Properties.Resources.Error;
                SetIconStatus("Not configured");
                return;
            }

            Initialize();
        }

        public void Initialize()
        {
            if (Initialized) { return; }

            Mail.Received += new Mail.ReceivedDelegate(ReceiveCallback);

            RunUpdate();

            UpdateTimer = new System.Timers.Timer(UpdateFrequency * 1000);
            UpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateTimer_Tick);
            UpdateTimer.Start();

            Initialized = true;
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
            try
            {
                EditSettings();
            }
            catch { }
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