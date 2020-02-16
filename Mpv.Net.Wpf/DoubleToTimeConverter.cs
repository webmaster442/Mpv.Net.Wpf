using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Mpv.Net.Wpf
{
    public class DoubleToTimeConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double duration)
            {
                var ts = TimeSpan.FromSeconds(duration);
                return $"{ts.Hours}:{ts.Minutes}:{ts.Seconds}";
            }
            else
                return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
