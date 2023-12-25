using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    /*
     *   ЭЛЕМЕНТЫ UI:
     *
     *   Уравнение #X (#1 или #2):
     *   - eqXvalueA - поле с коэффициентом для x^2
     *   - eqXvalueB - поле с коэффициентом для x
     *   - eqXvalueC - поле со свободным коэффициентом
     *   - eqXoutD - поле с дискриминантом
     *   - eqXoutSolution - поле с решением (с корнями уравнения)
     *   - eqXbtnSolve - кнопка "решить"
     *   - eqXbtnIncrement - кнопка "++" (унарные операции)
     *   - eqXbtnDecrement - кнопка "--" (унарные операции)
     *   - eqXoutDoubleCast - поле с результатом приведения к double
     *   - eqXbtnDoubleCast - кнопка для приведения к double
     *   - eqXoutBoolCast - поле с результатом приведения к bool
     *   - eqXbtnBoolCast - кнопка для приведения к bool
     *   
     *   Операции сравнения:
     *   - compOutEqX - поле с уравнением #X
     *   - compOutResult - поле с результатом сравнения
     *   - compBtnDefault - кнопка сравнения через "=="
     *   - compBtnNegate - кнопка сравнения через "!="
     *   
     *   Различные функции:
     *   - miscBtnClearEqX - кнопка для очистки ввода уравнения #X
     *   - miscBtnClearAll - кнопка для очистки всего ввода
     */

    public partial class MainWindow : Window
    {
        private static readonly string[] RESET_PREV_RESULTS_ON_CHANGED_PROPS = new string[] {"A","B","C"};

        private readonly QuadraticEquation[] equations;

        public MainWindow()
        {
            InitializeComponent();

            this.equations = new QuadraticEquation[]
            {
                (QuadraticEquation)FindResource("eq1"),
                (QuadraticEquation)FindResource("eq2")
            };

            RegisterListeners();
        }

        private void RegisterListeners()
        {
            // сброс предыдущих результатов тестирования методов при изменении уравнений
            equations[0].PropertyChanged += (sender, args) => ResetPreviousResults(args.PropertyName, eq1outDoubleCast, eq1outBoolCast);
            equations[1].PropertyChanged += (sender, args) => ResetPreviousResults(args.PropertyName, eq2outDoubleCast, eq2outBoolCast);

            // решение уравнений при клике на соответствующие кнопки
            eq1btnSolve.Click += (sender, args) => equations[0].SolveEquation();
            eq2btnSolve.Click += (sender, args) => equations[1].SolveEquation();

            // увеличение коэффициентов уравнений на 1 при клике на соответствующие кнопки
            eq1btnIncrement.Click += (sender, args) => equations[0]++;
            eq2btnIncrement.Click += (sender, args) => equations[1]++;

            // уменьшение коэффициентов уравнений на 1 при клике на соответствующие кнопки
            eq1btnDecrement.Click += (sender, args) => equations[0]--;
            eq2btnDecrement.Click += (sender, args) => equations[1]--;

            // неявное приведение к double при клике на соответствующие кнопки
            eq1btnDoubleCast.Click += (sender, args) => ShowDoubleCast(equations[0], eq1outDoubleCast);
            eq2btnDoubleCast.Click += (sender, args) => ShowDoubleCast(equations[1], eq2outDoubleCast);

            // явное приведение к bool при клике на соответствующие кнопки
            eq1btnBoolCast.Click += (sender, args) => ShowBoolCast(equations[0], eq1outBoolCast);
            eq2btnBoolCast.Click += (sender, args) => ShowBoolCast(equations[1], eq2outBoolCast);

            // сравнение уравнений
            compBtnDefault.Click += (sender, args) => CompareEquations(false);
            compBtnNegate.Click += (sender, args) => CompareEquations(true);

            // функции очистки ввода
            miscBtnClearEq1.Click += (sender, args) => ClearTextBoxes(eq1valueA, eq1valueB, eq1valueC);
            miscBtnClearEq2.Click += (sender, args) => ClearTextBoxes(eq2valueA, eq2valueB, eq2valueC);
            miscBtnClearAll.Click += (sender, args) => ClearTextBoxes(eq1valueA, eq1valueB, eq1valueC, eq2valueA, eq2valueB, eq2valueC);
        }

        private void ShowDoubleCast(QuadraticEquation equation, TextBox targetTextBox)
        {
            double value = equation;
            targetTextBox.Text = value.ToString();
        }

        private void ShowBoolCast(QuadraticEquation equation, TextBox targetTextBox)
        {
            bool value = (bool)equation;
            targetTextBox.Text = value.ToString();
        }

        private void CompareEquations(bool negate)
        {
            QuadraticEquation eq1 = equations[0];
            QuadraticEquation eq2 = equations[1];

            if (eq1.Valid && eq2.Valid)
                compOutResult.Text = (negate ? (eq1 != eq2) : (eq1 == eq2)).ToString();
            else
                compOutResult.Text = "Проверьте введённые уравнения!";
        }

        private void ResetPreviousResults(string? propertyName, TextBox doubleCastTextBox, TextBox boolCastTextBox)
        {
            // если имя свойства указано, и это одно из 'A', 'B', 'C' (коэффициенты), то...
            if (propertyName != null && RESET_PREV_RESULTS_ON_CHANGED_PROPS.Contains(propertyName))
            {
                // чистим текст боксы для приведений типов
                ClearTextBoxes(doubleCastTextBox, boolCastTextBox, compOutResult);
            }
        }

        private static void ClearTextBoxes(params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Text = "";
            }
        }
    }
}
