// Решить задачу с использованием структуры «текстовый файл».
// В файле хранятся целые числа по одному в строке.

// Найти сумму тех элементов файла, которые равны своему индексу.
// Индексацию элементов файла в этой задаче начинать с нуля.

using System.Diagnostics;

namespace Lab6
{
    internal class Task4
    {
        private static readonly string FILE_NAME = "task4_numbers.txt";

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача:");
            Console.WriteLine("Найти сумму тех элементов файла, которые равны своему индексу.");
            Console.WriteLine("Индексацию элементов файла в этой задаче начинать с нуля.");
            Console.WriteLine();

            Console.WriteLine("Доступные методы создания исходного файла:");
            Console.WriteLine("1. Сгенерировать автоматически");
            Console.WriteLine("2. Заполнить вручную");
            int action = UserInput.RequestIntegerInBounds("Выберите метод: ", 1, 2);
            Console.WriteLine();

            if (action == 1)
            {
                // --- генерация исходного файла со случайными числами ---
                int count = UserInput.RequestPositiveInteger("Введите количество исходных чисел: ");
                Console.WriteLine();

                if (!GenerateRandomNumbersFile(count, count))
                    return;
            }
            else
            {
                // --- заполнение исходного файла вручную ---
                if (!UserInput.AwaitManualInput(FILE_NAME))
                    return;
            }

            // --- решение задачи ---

            long sum;

            if (!SolveTask(out sum))
                return;

            Console.WriteLine($"Результат: {sum}");
        }

        private static bool SolveTask(out long sum)
        {
            sum = 0;

            try
            {
                StreamReader reader = new StreamReader(File.Open(FILE_NAME, FileMode.Open));

                int index = 0;
                string? line;
                int number;

                while (true)
                {
                    line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;

                    if (int.TryParse(line, out number) && number == index)
                    {
                        Console.WriteLine($"Найдено число {number}, соответствующее своему индексу...");
                        sum += number;
                    }

                    index++;
                }

                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при чтении исходного файла!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return false;
            }
        }

        private static bool GenerateRandomNumbersFile(int count, int maxBound)
        {
            try
            {
                StreamWriter writer = new StreamWriter(File.Open(FILE_NAME, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                {
                    writer.Write(random.Next(maxBound));
                    writer.WriteLine();
                }

                writer.Flush();
                writer.Close();

                Console.WriteLine("Файл со случайными числами сохранён по пути:\n" + Path.GetFullPath(FILE_NAME));
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при создании исходного файла!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return false;
            }
        }

    }
}
