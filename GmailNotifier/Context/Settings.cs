using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GmailNotifier
{
    public partial class Context
    {
        SettingsForm Form;

        void LoadSettings()
        {
            try
            {
                Mail.LoadSettings();
            }
            catch { }
        }

        void EditSettings()
        {
            if (Form != null && Form.Visible)
            {
                Form.Activate();
                return;
            }

            Form = new SettingsForm();

            Form.User.Text = Mail.Settings.User;
            Form.Password.Text = Mail.Settings.Password;
            Form.AutoStart.Checked = Mail.Settings.Present ? Mail.Settings.AutoStart : true;

            if (Form.ShowDialog() == DialogResult.OK)
            {
                Mail.Settings.User = Form.User.Text;
                Mail.Settings.Password = Form.Password.Text;
                Mail.Settings.AutoStart = Form.AutoStart.Checked;

                SaveSettings();
            }
        }

        void SaveSettings()
        {
            try
            {
                Mail.SaveSettings();

                UpdateState();
            }
            catch (Exception e)
            {
                Tools.ErrorMessage("Your settings could not be saved because of the following error:\r\n" + e.Message);
            }
        }
    }
}
