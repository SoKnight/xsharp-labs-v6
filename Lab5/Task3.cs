// Добавить к реализованному во втором задании классу указанные в варианте перегруженные операции.
// Протестировать все методы.

// Унарные операции:
//   ++ увеличивает коэффициенты уравнения на 1;
//   -- уменьшает коэффициенты уравнения на 1.
// Операции приведения типа:
//   double(неявная) – результатом является дискриминант уравнения.
//   bool (явная) – результатом является true, если корни существуют и false в противном случае;
// Бинарные операции
//   == QuadraticEquation t1, QuadraticEquation t2 — уравнения равны, если равны их коэффициенты;
//   != QuadraticEquation, QuadraticEquation — уравнения не равны, если не равны их коэффициенты

namespace Lab5
{
    // класс, описывающий квадратное уравнение в виде набора коэффициентов
    public class QuadraticEquation3
    {
        private double a, b, c;

        // конструктор со всеми коэффициентами
        public QuadraticEquation3(double a, double b, double c)
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

        // ++ увеличивает коэффициенты уравнения на 1
        public static QuadraticEquation3 operator ++(QuadraticEquation3 eq)
        {
            eq.a++;
            eq.b++;
            eq.c++;
            return eq;
        }

        // -- уменьшает коэффициенты уравнения на 1
        public static QuadraticEquation3 operator --(QuadraticEquation3 eq)
        {
            eq.a--;
            eq.b--;
            eq.c--;
            return eq;
        }

        // double (неявная) – результатом является дискриминант уравнения
        public static implicit operator double(QuadraticEquation3 eq)
        {
            return eq.ComputeDiscriminant();
        }

        // bool (явная) – результатом является true, если корни существуют и false в противном случае
        public static explicit operator bool(QuadraticEquation3 eq)
        {
            double[] roots = eq.SolveEquation();
            return roots.Length != 0 && !Double.IsNaN(roots[0]);
        }

        // == QuadraticEquation eq1, QuadraticEquation eq2 — уравнения равны, если равны их коэффициенты
        public static bool operator ==(QuadraticEquation3 eq1, QuadraticEquation3 eq2)
        {
            return eq1.a == eq2.a && eq1.b == eq2.b && eq1.c == eq2.c;
        }

        // != QuadraticEquation eq1, QuadraticEquation eq2 — уравнения не равны, если не равны их коэффициенты
        public static bool operator !=(QuadraticEquation3 eq1, QuadraticEquation3 eq2)
        {
            return eq1.a != eq2.a || eq1.b != eq2.b || eq1.c != eq2.c;
        }
    }

    internal class Task3
    {
        public static void Run()
        {
            Console.WriteLine("--- Квадратное уравнение #1 ---");
            double a = InputData.RequestDouble("Введите коэффициент при x^2: ");
            double b = InputData.RequestDouble("Введите коэффициент при x: ");
            double c = InputData.RequestDouble("Введите свободный коэффициент: ");
            Console.WriteLine();

            QuadraticEquation3 equation1 = new QuadraticEquation3(a, b, c);

            Console.WriteLine("--- Квадратное уравнение #2 ---");
            a = InputData.RequestDouble("Введите коэффициент при x^2: ");
            b = InputData.RequestDouble("Введите коэффициент при x: ");
            c = InputData.RequestDouble("Введите свободный коэффициент: ");
            Console.WriteLine();

            QuadraticEquation3 equation2 = new QuadraticEquation3(a, b, c);
            Console.WriteLine($"Исходное уравнение #2: {equation2}");
            Console.WriteLine();

            Console.WriteLine($"С увеличенными коэффициентами: {++equation2}");
            Console.WriteLine($"С уменьшенными коэффициентами: {--equation2}");
            Console.WriteLine();

            double implicitCast = equation1;
            Console.WriteLine($"Неявное приведение уравнения #1: {implicitCast}");
            Console.WriteLine($"Явное приведение уравнения #2: {(bool) equation2}");
            Console.WriteLine();

            Console.WriteLine($"Данные уравнения равны: {equation1 == equation2}");
            Console.WriteLine($"Данные уравнения не равны: {equation1 != equation2}");
        }
    }
}
