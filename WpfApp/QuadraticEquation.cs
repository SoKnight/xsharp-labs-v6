using System;
using System.ComponentModel;

namespace WpfApp
{
    // класс, описывающий квадратное уравнение в виде набора коэффициентов
    public class QuadraticEquation : INotifyPropertyChanged
    {
        private double a, b, c; // коэффициенты уравнения
        private double? discriminant; // дискриминант (может не быть вычислен - null)
        private double[]? solution; // решение уравнения (может не быть вычислено - null)

        // делегат: обработчик события изменения значения какого-либо из свойств
        // нужен для оповещения UI о том, что какое-то свойство изменено
        public event PropertyChangedEventHandler? PropertyChanged;

        // свойство: коэффициент при x^2 (A)
        public double A { 
            get { return a; } 
            set { 
                this.a = value; 
                NotifyCoefficientChanged("A");
                ResetComputationsCache();
            }
        }
        // свойство: коэффициент при x (B)
        public double B
        {
            get { return b; }
            set
            {
                this.b = value;
                NotifyCoefficientChanged("B");
                ResetComputationsCache();
            }
        }
        // свойство: свободный коэффициент (C)
        public double C
        {
            get { return c; }
            set
            {
                this.c = value;
                NotifyCoefficientChanged("C");
                ResetComputationsCache();
            }
        }
        // свойство: вычисленный дискриминант
        public double? Discriminant { 
            get { return discriminant; }
            private set
            {
                this.discriminant = value;
                NotifyPropertyChanged("Discriminant");
            }
        }
        // свойство: вычисленное решение уравнения (корни)
        public double[]? Solution { 
            get { return solution; } 
            private set
            {
                this.solution = value;
                NotifyPropertyChanged("Solution");
            }
        }
        // свойство: состояние валидности уравнения
        // валидность уравнения определяется наличием у него всех трёх коэффициентов
        public bool Valid
        {
            get { return HasValidCoefficients(); }
        }
        // свойство: опциональный валидный экземпляр
        // это небольшой костыль для отображения уравнения в поле раздела "Операции сравнения"
        public QuadraticEquation? ValidInstance
        {
            get { return HasValidCoefficients() ? this : null; }
        }

        // конструктор без коэффициентов
        public QuadraticEquation() : this(double.NaN, double.NaN, double.NaN) { }

        // конструктор со всеми коэффициентами
        public QuadraticEquation(double a, double b, double c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        // метод для проверки валидности коэффициентов
        public bool HasValidCoefficients()
        {
            return !double.IsNaN(A) && !double.IsNaN(B) && !double.IsNaN(C);
        }

        // метод для вычисления дискриминанта
        public double ComputeDiscriminant()
        {
            if (Discriminant.HasValue)
                return Discriminant.Value;

            Discriminant = (b * b) - (4 * a * c);
            return Discriminant.Value;
        }

        // метод для решения уравнения (по заданию)
        public double[] SolveEquation()
        {
            if (Solution != null)
                return Solution;

            if (a == 0)
            {
                if (b == 0)
                    Solution = c == 0 ? new double[] { double.NaN } : new double[0];
                else
                    Solution = c == 0 ? new double[] { 0 } : new double[] { -c / b };
                return Solution;
            }
            else if (b == 0 && c == 0)
            {
                Solution = new double[] { 0 };
                return Solution;
            }

            double discriminant = ComputeDiscriminant();

            if (discriminant < 0.0)
            {
                Solution = new double[0];
                return Solution;
            }

            if (discriminant == 0.0)
            {
                Solution = new double[] { -b / (2 * a) };
                return Solution;
            }

            double dsqrt = Math.Sqrt(discriminant);

            Solution = new double[]
            {
                (-b - dsqrt) / (2 * a),
                (-b + dsqrt) / (2 * a)
            };
            return Solution;
        }

        // перегрузка #ToString()
        public override string ToString()
        {
            return "{a=" + A
                + ", b=" + B
                + ", c=" + C
                + "}";
        }

        // сброс кэша вычислений
        private void ResetComputationsCache()
        {
            Discriminant = null;
            Solution = null;
        }
        
        // оповещение об изменении указанного свойства
        private void NotifyPropertyChanged(string? propName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        // оповещение об изменении указанного коэффициента
        // при его изменении может также измениться состояние валидности уравнения
        private void NotifyCoefficientChanged(string? propName)
        {
            NotifyPropertyChanged(propName);
            NotifyPropertyChanged("Valid");
            NotifyPropertyChanged("ValidInstance");
        }

        // ++ увеличивает коэффициенты уравнения на 1
        public static QuadraticEquation operator ++(QuadraticEquation eq)
        {
            eq.A++;
            eq.B++;
            eq.C++;
            return eq;
        }

        // -- уменьшает коэффициенты уравнения на 1
        public static QuadraticEquation operator --(QuadraticEquation eq)
        {
            eq.A--;
            eq.B--;
            eq.C--;
            return eq;
        }

        // double (неявная) – результатом является дискриминант уравнения
        public static implicit operator double(QuadraticEquation eq)
        {
            return eq.ComputeDiscriminant();
        }

        // bool (явная) – результатом является true, если корни существуют и false в противном случае
        public static explicit operator bool(QuadraticEquation eq)
        {
            double[] roots = eq.SolveEquation();
            return roots.Length != 0 && !double.IsNaN(roots[0]);
        }

        // == QuadraticEquation eq1, QuadraticEquation eq2 — уравнения равны, если равны их коэффициенты
        public static bool operator ==(QuadraticEquation eq1, QuadraticEquation eq2)
        {
            return eq1.a == eq2.a && eq1.b == eq2.b && eq1.c == eq2.c;
        }

        // != QuadraticEquation eq1, QuadraticEquation eq2 — уравнения не равны, если не равны их коэффициенты
        public static bool operator !=(QuadraticEquation eq1, QuadraticEquation eq2)
        {
            return eq1.a != eq2.a || eq1.b != eq2.b || eq1.c != eq2.c;
        }
    }
}
