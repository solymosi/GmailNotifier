using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MailNotifier
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

            try
            {
                LoadSettings();
                Initialize();
            }
            catch(ApplicationException)
            {
                InstallIcon(false);
            }
        }

        public void Initialize()
        {
            if (Initialized) { return; }

            Gmail.Received += new Gmail.ReceivedDelegate(ReceiveCallback);
            RunCheck();

            UpdateTimer = new System.Timers.Timer(UpdateFrequency * 1000);
            UpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(CheckTimer_Tick);
            UpdateTimer.Start();

            InstallIcon(true);

            Initialized = true;
        }

        void OpenGmail_Click(object sender, EventArgs e)
        {
            Process.Start(Gmail.Settings.ServerAddress);
        }

        void ShowUnread_Click(object sender, EventArgs e)
        {
            ShowNotification(Gmail.Emails);
        }

        void CheckNow_Click(object sender, EventArgs e)
        {
            RunCheck();
        }

        void Settings_Click(object sender, EventArgs e)
        {
            try
            {
                EditSettings();
                RunCheck();
            }
            catch { }
        }

        void Exit_Click(object sender, EventArgs e)
        {
            ExitThread();
        }

        void CheckTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            RunCheck();
        }

        void Context_ThreadExit(object sender, EventArgs e)
        {
            RemoveIcon();
        }

        void ReceiveCallback(List<Email> Emails)
        {
            if (Emails.Count > 0) { Tools.PlayNotificationSound(); }
            ShowNotification(Emails);
        }

    }
}