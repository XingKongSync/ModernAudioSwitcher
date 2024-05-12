using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ModernAudioSwitcher.Converters
{
    [MarkupExtensionReturnType(typeof(NullToVisibilityConverter))]
    public class NullToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private static readonly NullToVisibilityConverter _converter = new NullToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = value != null;
            if (parameter != null)
            {
                b = !b;
            }
            return b ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter;
        }
    }
}
