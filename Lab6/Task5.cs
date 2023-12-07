// Решить задачу с использованием структуры «текстовый файл».
// В файле хранятся целые числа по несколько в строке.

// Вычислить произведение элементов, которые кратны заданному числу k.

namespace Lab6
{
    internal class Task5
    {
        private static readonly string FILE_NAME = "task5_numbers.txt";

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача:");
            Console.WriteLine("Вычислить произведение элементов, которые кратны заданному числу k.");
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

                if (!GenerateRandomNumbersFile(count, count, 0.25))
                    return;
            }
            else
            {
                // --- заполнение исходного файла вручную ---
                if (!UserInput.AwaitManualInput(FILE_NAME))
                    return;
            }

            int k = UserInput.RequestNonZeroInteger("Введите значение 'k': ");
            Console.WriteLine();

            // --- решение задачи ---

            long mult;

            if (!SolveTask(k, out mult))
                return;

            Console.WriteLine($"Результат: {mult}");
        }

        private static bool SolveTask(int k, out long mult)
        {
            mult = 0;

            try
            {
                StreamReader reader = new StreamReader(File.Open(FILE_NAME, FileMode.Open));

                string? line;
                int number;

                while (true)
                {
                    line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;

                    foreach (string arg in line.Split(' '))
                    {
                        if (arg.Length != 0 && int.TryParse(arg, out number) && number % k == 0)
                        {
                            if (mult == 0)
                                mult = 1;

                            Console.WriteLine($"Найдено число {number}, кратное {k}...");
                            mult *= number;
                        }
                    }
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

        private static bool GenerateRandomNumbersFile(int count, int maxBound, double lineBreakChance)
        {
            try
            {
                StreamWriter writer = new StreamWriter(File.Open(FILE_NAME, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                {
                    writer.Write(random.Next(maxBound));

                    if (random.NextDouble() < lineBreakChance)
                        writer.WriteLine();
                    else
                        writer.Write(' ');
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

