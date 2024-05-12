using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernAudioSwitcher.ViewModels
{
    public partial class TrayContextMenuViewModel : ObservableObject
    {
        [RelayCommand]
        private void ShowWindow()
        {
            WindowExtensions.Show(MainWindow.Instance, true);
            MainWindow.Instance.Focus();
        }

        [RelayCommand]
        private void ExitApplication() 
        {
            App.Current.Shutdown();
        }
    }
}
