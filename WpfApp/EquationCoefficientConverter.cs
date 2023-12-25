using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp
{
    [ValueConversion(typeof(double), typeof(string))]
    public class EquationCoefficientConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                double valueAsDouble = (double)value;
                if (!double.IsNaN(valueAsDouble))
                {
                    return valueAsDouble;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double valueAsDouble;

            if (double.TryParse(value.ToString(), out valueAsDouble))
                return valueAsDouble;
            else
                return double.NaN;
        }
    }
}
