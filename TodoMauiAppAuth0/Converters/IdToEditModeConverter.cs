using System.Globalization;

namespace TodoMauiAppAuth0.Converters;

/// <summary>
/// Hvis Id = 0 returneres false. Ellers returneres true
/// </summary>
public class IdToEditModeConverter : IValueConverter, IMarkupExtension
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int id = (int)value;
        return id != 0;
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
