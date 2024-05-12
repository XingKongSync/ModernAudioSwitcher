using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernAudioSwitcher.Win32
{
    public static class AutoRunHelper
    {
        private const string CONST_APP_NAME = "AudioSwitcher";

        private static string GetExePath()
        {
            var process = Process.GetCurrentProcess();
            return process.MainModule!.FileName;
        }

        public static bool IsAutoRun()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp == null)
                return false;
            using (rkApp)
            {

                if (rkApp.GetValue(CONST_APP_NAME) == null)
                    // The value doesn't exist, the application is not set to run at startup
                    return false;
                else
                    // The value exists, the application is set to run at startup
                    return true;
            }
        }

        public static void SetAutoRun(bool value)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp == null)
                return;
            using (rkApp)
            {
                if (value)
                {
                    rkApp.SetValue(CONST_APP_NAME, $"\"{GetExePath()}\" --hide");
                }
                else
                {
                    rkApp.DeleteValue(CONST_APP_NAME, false);
                }
            }
        }
    }
}
