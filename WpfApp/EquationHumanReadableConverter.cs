using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp
{
    [ValueConversion(typeof(QuadraticEquation), typeof(string))]
    public class EquationHumanReadableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is QuadraticEquation)
            {
                QuadraticEquation equation = (QuadraticEquation)value;
                return equation.ToString();
            }

            return "Коэффициенты заданы неверно!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
