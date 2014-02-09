using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MailNotifier
{
    public partial class Context
    {
        void LoadSettings()
        {
            try
            {
                Gmail.LoadSettings();
                if (!Gmail.Settings.Present) { throw new Exception(); }
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        void EditSettings()
        {
            SettingsForm Form = new SettingsForm();
            Form.User.Text = Gmail.Settings.User;
            Form.AutoStart.Checked = Gmail.Settings.Present ? Gmail.Settings.AutoStart : true;

            if (Form.ShowDialog() == DialogResult.OK)
            {
                Gmail.Settings.User = Form.User.Text;
                if (Gmail.Settings.Password == "" || Form.Password.Text != "")
                {
                    Gmail.Settings.Password = Form.Password.Text;
                }
                Gmail.Settings.AutoStart = Form.AutoStart.Checked;
                Gmail.SaveSettings();

                Initialize();
                RunCheck();
            }
        }
    }
}
