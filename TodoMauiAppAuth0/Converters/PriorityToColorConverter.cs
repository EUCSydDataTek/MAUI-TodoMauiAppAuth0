using System.Globalization;
using TodoMauiAppAuth0.Models;

namespace TodoMauiAppAuth0.Converters;
/// <summary>
/// Low = Green, Normal = Orange, High = Red
/// </summary>
public class PriorityToColorConverter : IValueConverter, IMarkupExtension
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((PriorityLevel)value == PriorityLevel.Low)
        {
            return Colors.Green;
        }
        if ((PriorityLevel)value == PriorityLevel.Normal)
        {
            return Colors.Orange;
        }
        else
        {
            return Colors.Red;
        }
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
