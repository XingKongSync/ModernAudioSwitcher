using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ModernAudioSwitcher.Converters
{
    [MarkupExtensionReturnType(typeof(EmptyCollectionToVisibilityConverter))]
    public class EmptyCollectionToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private static readonly EmptyCollectionToVisibilityConverter _converter = new EmptyCollectionToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool show;
            if (value is int count)
            {
                show = count > 0;
                if (parameter is not null)
                    show = !show;
                return show ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
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
