using H.NotifyIcon;
using ModernAudioSwitcher.Services;
using System.Windows;

namespace ModernAudioSwitcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        TaskbarIcon? NotifyIcon;

        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _ = UIService.Instance;
            _ = AudioService.Instance;

            MainWindow = ModernAudioSwitcher.MainWindow.Instance;

            ParseArgs(e.Args, out bool hideUI);

            NotifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            try
            {
                NotifyIcon.ForceCreate();
                NotifyIcon.TrayLeftMouseDoubleClick += (sender, args) =>
                {
                    MainWindow.Show(disableEfficiencyMode: true);
                    MainWindow.Focus();
                };

                if (!hideUI)
                    MainWindow.Show(disableEfficiencyMode: true);
            }
            catch (Exception)
            {
                ModernAudioSwitcher.MainWindow.ExitOnClose = true;
                ShutdownMode = ShutdownMode.OnMainWindowClose;
                MainWindow.Show();
            }
        }

        private void ParseArgs(string[] args, out bool hideUI)
        {
            hideUI = false;
            if (args != null && args.Length > 0 && args[0] == "--hide")
            {
                hideUI = true;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            NotifyIcon?.Dispose();
            NotifyIcon = null;
            base.OnExit(e);
        }
    }

}
