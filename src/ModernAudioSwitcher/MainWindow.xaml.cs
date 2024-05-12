using ModernAudioSwitcher.Services;
using ModernAudioSwitcher.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using H.NotifyIcon;

namespace ModernAudioSwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        private static Lazy<MainWindow> _instance = new Lazy<MainWindow>(() => new MainWindow());

        public static MainWindow Instance { get => _instance.Value; }

        public static bool ExitOnClose = false;

        public MainWindow()
        {
            InitializeComponent();
         
            UIService.Instance.ContentDialogService.SetDialogHost(RootContentDialog);
            Loaded += (sender, args) =>
            {
                Wpf.Ui.Appearance.SystemThemeWatcher.Watch(
                    this,                                  // Window class
                    WindowBackdropType.Mica, // Background type
                    true                                   // Whether to change accents automatically
                );
                RootNavigation.Navigate(typeof(Views.PresetPage));

                int count = VisualTreeHelper.GetChildrenCount(RootNavigation);
                for (int i = 0; i < count; i++)
                {
                    if (VisualTreeHelper.GetChild(RootNavigation, i) is Grid gd)
                    {
                        int gdChildCount = VisualTreeHelper.GetChildrenCount(gd);
                        for (int j = 0; j < gdChildCount; j++)
                        {
                            if (VisualTreeHelper.GetChild(gd, j) is Border bd)
                            {
                                bd.Background = null;
                                bd.BorderBrush = null;
                                return;
                            }
                        }
                    }
                }
            };
            Closing += (sender, args) =>
            {
                if (!ExitOnClose)
                {
                    args.Cancel = true;
                    WindowExtensions.Hide(this, true);
                }
            };
        }
    }
}