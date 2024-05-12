using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ModernAudioSwitcher.Converters
{
    [MarkupExtensionReturnType(typeof(AudioDeviceMarkConverter))]
    public class AudioDeviceMarkConverter : MarkupExtension, IMultiValueConverter
    {
        private static readonly AudioDeviceMarkConverter _converter= new AudioDeviceMarkConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || (values.Length > 0 && values[0] is not bool))
            {
                return Visibility.Collapsed;
            }
            try
            {
                bool isDef = (bool)values[0];
                bool isDefMedia = (bool)values[1];
                bool isDefCommu = (bool)values[2];

                string param = (string)parameter;
                switch (param)
                {
                    case "default":
                        return isDef ? Visibility.Visible : Visibility.Collapsed;
                    case "defaultMedia":
                        return !isDef && isDefMedia ? Visibility.Visible : Visibility.Collapsed;
                    case "defaultCommu":
                        return !isDef && isDefCommu ? Visibility.Visible : Visibility.Collapsed;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter;
        }
    }
}
