using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace MailNotifier
{
    public partial class Context
    {
        NotifyIcon Icon;

        ContextMenuStrip Menu;

        ToolStripMenuItem mViewInbox;
        ToolStripMenuItem mShowUnread;
        ToolStripMenuItem mCheckNow;
        ToolStripMenuItem mSettings;
        ToolStripMenuItem mExit;

        public void InstallIcon(bool Configured)
        {
            if (Icon != null)
            {
                RemoveIcon();
            }

            Icon = new NotifyIcon();
            Icon.Icon = Properties.Resources.Problem;

            if (Configured)
            {
                Menu = new ContextMenuStrip();

                mViewInbox = new ToolStripMenuItem("View Inbox", null, new EventHandler(OpenGmail_Click));
                mViewInbox.Font = new Font(mViewInbox.Font, FontStyle.Bold);
                mShowUnread = new ToolStripMenuItem("Show Unread", null, new EventHandler(ShowUnread_Click));
                mCheckNow = new ToolStripMenuItem("Check Now", null, new EventHandler(CheckNow_Click));
                mSettings = new ToolStripMenuItem("Settings...", null, new EventHandler(Settings_Click));
                mExit = new ToolStripMenuItem("Exit                                ", null, new EventHandler(Exit_Click));

                Menu.RenderMode = ToolStripRenderMode.System;
                Menu.Opening += new CancelEventHandler(ContextMenuStrip_Opening);

                Menu.Items.Add(mViewInbox);
                Menu.Items.Add(new ToolStripSeparator());
                Menu.Items.Add(mShowUnread);
                Menu.Items.Add(mCheckNow);
                Menu.Items.Add(new ToolStripSeparator());
                Menu.Items.Add(mSettings);
                Menu.Items.Add(mExit);

                Icon.DoubleClick += new EventHandler(OpenGmail_Click);
                Icon.ContextMenuStrip = Menu;

                SetIconStatus("");
            }
            else
            {
                Icon.Click += new EventHandler(delegate
                {
                    EditSettings();
                });

                SetIconStatus("Not configured");
            }

            Icon.Visible = true;
        }

        void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mCheckNow.Text = (Updating ? "Checking Mail..." : "Check Now");
            mCheckNow.Enabled = !Updating;

            mShowUnread.Text = (Gmail.Emails.Count == 0 ? "No Unread Mail" : "Show Unread");
            mShowUnread.Enabled = Gmail.Emails.Count > 0;
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
