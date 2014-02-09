using System;
using System.Collections.Generic;
using System.Text;

namespace MailNotifier
{
    public static partial class Gmail
    {
        public const string SettingsFileName = "Settings.data";

        public static void SaveSettings()
        {
            Tools.Serialize<Settings>(Settings, SettingsFileName);
        }

        public static void LoadSettings()
        {
            Settings = Tools.Deserialize<Settings>(SettingsFileName);
        }
    }
}
