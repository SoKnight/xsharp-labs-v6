using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp
{
    [ValueConversion(typeof(double?), typeof(string))]
    public class EquationDiscriminantConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double?)
            {
                double? valueAsDouble = (double?)value;
                if (valueAsDouble.HasValue)
                {
                    return valueAsDouble.Value;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
