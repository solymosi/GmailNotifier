using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace GmailNotifier
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            List<string> Errors = new List<string>();

            if (User.Text.Trim() == "")
            {
                Errors.Add("- Please provide your email address or user name");
            }
            if (Password.Text.Trim() == "" && Mail.Settings.Password == "")
            {
                Errors.Add("- Please enter your password");
            }

            if (Errors.Count > 0)
            {
                Tools.ErrorMessage("The following errors need to be corrected:\r\n" + string.Join("\r\n", Errors.ToArray()));
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GetAppPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Mail.AppPasswordAddress);
        }
    }
}
