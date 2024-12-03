using System.Globalization;

namespace TodoMauiAppAuth0.Converters
{
    public class UtcToLocalTimeConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                var targetCulture = parameter as CultureInfo ?? culture;
                return dateTime.ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss", targetCulture);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
