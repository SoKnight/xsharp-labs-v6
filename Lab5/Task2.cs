// Реализовать определение класса (поля, свойства, конструкторы, перегрузка метода ToString() для вывода полей, заданный метод согласно варианту).
// Протестировать все методы, включая конструкторы, не забывайте проверять вводимые пользователем данные на корректность!

// Вычисление корней квадратного уравнения.
// Результат должен быть массивом величин типа double (в нем от 0 до 2-х элементов, в зависимости от количества корней).

namespace Lab5
{
    // класс, описывающий квадратное уравнение в виде набора коэффициентов
    public class QuadraticEquation2
    {
        private readonly double a, b, c;

        // конструктор со всеми коэффициентами
        public QuadraticEquation2(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        // метод для вычисления дискриминанта
        public double ComputeDiscriminant()
        {
            return (b * b) - (4 * a * c);
        }

        // метод для решения уравнения (по заданию)
        public double[] SolveEquation()
        {
            if (a == 0)
            {
                if (b == 0)
                    return c == 0 ? new double[] { Double.NaN } : new double[0];
                else
                    return c == 0 ? new double[] { 0 } : new double[] { -c / b };
            }
            else if (b == 0 && c == 0)
            {
                return new double[] { 0 };
            }

            double discriminant = ComputeDiscriminant();

            if (discriminant < 0.0)
                return new double[0];

            if (discriminant == 0.0)
                return new double[] { -b / (2 * a) };

            double dsqrt = Math.Sqrt(discriminant);

            return new double[]
            {
                (-b - dsqrt) / (2 * a),
                (-b + dsqrt) / (2 * a)
            };
        }

        // перегрузка #ToString()
        public override string ToString()
        {
            return "{a=" + a
                + ", b=" + b
                + ", c=" + c
                + "}";
        }
    }

    internal class Task2
    {
        public static void Run()
        {
            Console.WriteLine("--- Квадратное уравнение ---");
            double a = InputData.RequestDouble("Введите коэффициент при x^2: ");
            double b = InputData.RequestDouble("Введите коэффициент при x: ");
            double c = InputData.RequestDouble("Введите свободный коэффициент: ");
            Console.WriteLine();

            QuadraticEquation2 equation = new QuadraticEquation2(a, b, c);
            double[] roots = equation.SolveEquation();

            switch (roots.Length)
            {
                case 0:
                    Console.WriteLine("Данное уравнение не имеет решений!");
                    break;
                case 1:
                    if (Double.IsNaN(roots[0]))
                        Console.WriteLine($"Уравнение верно для любого X.");
                    else
                        Console.WriteLine($"Уравнение имеет одно решение: {roots[0]}");
                    break;
                case 2:
                    Console.WriteLine($"Уравнение имеет два решения: {roots[0]} и {roots[1]}");
                    break;
            }
        }
    }
}
