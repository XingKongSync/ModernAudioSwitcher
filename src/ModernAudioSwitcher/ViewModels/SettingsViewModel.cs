using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernAudioSwitcher.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernAudioSwitcher.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _autoRun;

        public SettingsViewModel()
        {
            AutoRun = AutoRunHelper.IsAutoRun();
        }

        partial void OnAutoRunChanged(bool value)
        {
            AutoRunHelper.SetAutoRun(value);
            //Wpf.Ui.Appearance.ApplicationThemeManager.GetAppTheme()
        }
    }
}
