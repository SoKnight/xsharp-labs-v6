using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp
{
    [ValueConversion(typeof(double[]), typeof(string))]
    public class EquationSolutionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double[])
            {
                double[] roots = (double[])value;
                switch (roots.Length)
                {
                    case 0:
                        return "Решений нет";
                    case 1:
                        if (double.IsNaN(roots[0]))
                            return "Верно для любого X";
                        else
                            return $"{roots[0]}";
                    case 2:
                        return $"{roots[0]} и {roots[1]}";
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
