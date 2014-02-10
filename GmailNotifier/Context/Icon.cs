using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace GmailNotifier
{
    public partial class Context
    {
        NotifyIcon Icon;

        ContextMenuStrip Menu;

        ToolStripMenuItem mViewInbox;
        ToolStripMenuItem mCheckNow;
        ToolStripMenuItem mShowUnread;
        ToolStripMenuItem mSettings;
        ToolStripMenuItem mExit;

        public void InstallIcon()
        {
            Icon = new NotifyIcon();
            Icon.Icon = Properties.Resources.Read;

            Menu = new ContextMenuStrip();

            mViewInbox = new ToolStripMenuItem("View Inbox", null, new EventHandler(OpenMail_Click));
            mViewInbox.Font = new Font(mViewInbox.Font, FontStyle.Bold);
            mCheckNow = new ToolStripMenuItem("Check Now", null, new EventHandler(CheckNow_Click));
            mShowUnread = new ToolStripMenuItem("Show Unread", null, new EventHandler(ShowUnread_Click));
            mSettings = new ToolStripMenuItem("Settings...", null, new EventHandler(Settings_Click));
            mExit = new ToolStripMenuItem("Exit                      ", null, new EventHandler(Exit_Click));

            Menu.RenderMode = ToolStripRenderMode.System;
            Menu.Opening += new CancelEventHandler(ContextMenuStrip_Opening);

            Menu.Items.Add(mViewInbox);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add(mShowUnread);
            Menu.Items.Add(mCheckNow);
            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add(mSettings);
            Menu.Items.Add(mExit);

            Icon.DoubleClick += new EventHandler(OpenMail_Click);
            Icon.ContextMenuStrip = Menu;

            SetIconStatus("");

            Icon.Visible = true;
        }

        void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mCheckNow.Text = (Updating ? "Checking mail..." : "Check Now");
            mCheckNow.Enabled = Mail.Settings.Present && !Updating;

            mShowUnread.Text = (Mail.Emails.Count == 0 ? "No unread mail" : "Show Unread");
            mShowUnread.Enabled = Mail.Settings.Present && Mail.Emails.Count > 0;
        }

        void UpdateIcon(Icon Icon, string Message)
        {
            MainSynchronizationContext.Post(delegate
            {
                Program.Context.Icon.Icon = Icon;
                SetIconStatus(Message);
            }, null);
        }

        public void RemoveIcon()
        {
            Icon.Visible = false;
        }

        public void SetIconStatus(string Status)
        {
            Icon.Text = "Gmail Notifier" + (Status == "" ? "" : " - " + Status);
        }
    }
}
