using System;
using System.Collections.Generic;
using System.Text;

namespace GmailNotifier
{
    static partial class Mail
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
