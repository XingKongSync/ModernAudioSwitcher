using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace ModernAudioSwitcher.Services
{
    internal class UIService
    {
        private static Lazy<UIService> _service = new Lazy<UIService>(() => new UIService());

        public static UIService Instance { get { return _service.Value; } }

        public IContentDialogService ContentDialogService { get; } = new ContentDialogService();

        private UIService() { }
    }
}
