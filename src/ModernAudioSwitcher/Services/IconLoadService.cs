using AudioSwitcher.Presentation.Drawing;
using AudioSwitcher.Presentation.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ModernAudioSwitcher.Services
{
    internal class IconLoadService
    {
        private static Lazy<IconLoadService> _service = new Lazy<IconLoadService>(() => new IconLoadService());
        public static IconLoadService Instance { get {  return _service.Value; } }
        private static readonly Size IconSize = DpiServices.Scale(new Size(48, 48));

        private Dictionary<string, BitmapSource> _cache = new Dictionary<string, BitmapSource>();

        private IconLoadService() { }

        public BitmapSource? GetIcon(string iconPath)
        {
            lock (_cache)
            {
                if (_cache.TryGetValue(iconPath, out var result))
                {
                    return result;
                }
            }

            try
            {
                if (ShellIcon.TryExtractIconByIdOrIndex(iconPath, IconSize, out var icon))
                {
                    var bmsSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                        icon.Handle,
                        System.Windows.Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                    bmsSrc.Freeze();

                    lock (_cache)
                    {
                        _cache.Add(iconPath, bmsSrc);
                    }

                    return bmsSrc;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}
