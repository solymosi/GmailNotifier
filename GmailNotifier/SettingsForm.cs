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
            Text = "Gmail Notifier " + Application.ProductVersion + " - Configuration";
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GetAppPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Mail.AppPasswordAddress);
        }
    }
}
