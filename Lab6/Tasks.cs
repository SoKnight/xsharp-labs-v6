using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Lab6
{
    internal class Tasks
    {
        // Бинарные файлы, содержащие числовые данные (исходный файл заполнить случайными данными, заполнение организовать отдельным методом).
        // Переписать в другой файл последовательного доступа те элементы, которые кратны k.
        public static void RunTask1()
        {
            // --- генерация исходного файла

            int count = RequestPositiveInteger("Введите количество генерируемых чисел: ");
            Console.WriteLine();

            int[] numbers = new int[count];
            string inputFile = GetFileName("input", 1, true);

            if (!GenerateRandomNumbersBinaryFile(inputFile, count, 1, 1000, numbers))
                return;

            Console.WriteLine($"Сгенерированные числа: [{String.Join(", ", numbers)}]");
            Console.WriteLine();

            // --- решение поставленной задачи

            int k = RequestNonZeroInteger("Введите значение k: ");
            Console.WriteLine();

            int countOfWritten = 0;
            string outputFile = GetFileName("output", 1, true);

            try
            {
                BinaryReader reader = new BinaryReader(File.Open(inputFile, FileMode.Open));
                BinaryWriter writer = new BinaryWriter(File.Open(outputFile, FileMode.Create));

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();
                    if (number % k == 0)
                    {
                        writer.Write(number);
                        numbers[countOfWritten++] = number;
                    }
                }

                reader.Close();
                writer.Close();
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при перезаписи содержимого между файлами!\n{ex.Message}");
                return;
            }

            Console.WriteLine("Файл с результатами сохранён по пути:\n" + Path.GetFullPath(outputFile));
            Console.WriteLine();

            // --- вывод результата

            Array.Resize(ref numbers, countOfWritten);
            Console.WriteLine($"Результат: [{String.Join(", ", numbers)}]");
        }

        // Бинарные файлы, содержащие числовые данные (исходный файл заполнить случайными данными, заполнение организовать отдельным методом).
        // Скопировать элементы заданного файла в квадратную матрицу размером n×n (если элементов файла недостает, заполнить оставшиеся элементы матрицы нулями).
        // Заменить все столбцы на столбец с минимальной суммой элементов.
        public static void RunTask2()
        {
            // --- генерация исходного файла со случайными числами ---

            int count = RequestPositiveInteger("Введите количество генерируемых чисел: ");
            Console.WriteLine();

            string fileName = GetFileName("matrix", 2, true);
            if (!GenerateRandomNumbersBinaryFile(fileName, count, 1, 100))
                return;

            // --- создание матрицы

            int n = RequestPositiveInteger("Введите размерность матрицы 'n': ");
            Console.WriteLine();

            int[,] matrix2D = new int[n, n];

            // --- чтение данных в матрицу

            int index = 0;
            int limit = n * n;

            try
            {
                BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    if (index >= limit)
                        break;

                    int number = reader.ReadInt32();
                    matrix2D[index / n, index % n] = number;
                    index++;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при построении матрицы из исходных данных!\n{ex.Message}");
                return;
            }

            Console.WriteLine(" ИСХОДНАЯ МАТРИЦА:");
            Console.WriteLine(Matrix2DToString(matrix2D, n));

            // --- поиск столбца с наименьшей суммой элементов

            int leastSum = int.MaxValue;
            int leastSumColumnIndex = 0;

            for (int i = 0; i < n; i++)
            {
                int currentSum = 0;

                for (int j = 0; j < n; j++)
                {
                    currentSum += matrix2D[j, i];
                }

                if (currentSum <= leastSum)
                {
                    leastSum = currentSum;
                    leastSumColumnIndex = i;
                }
            }

            Console.WriteLine($"Столбец #{leastSumColumnIndex + 1} имеет минимальную сумму элементов: {leastSum}");
            Console.WriteLine();

            // --- решение поставленной задачи

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == leastSumColumnIndex)
                        continue;

                    matrix2D[i, j] = matrix2D[i, leastSumColumnIndex];
                }
            }

            // --- вывод результата

            Console.WriteLine(" РЕЗУЛЬТАТ:");
            Console.WriteLine(Matrix2DToString(matrix2D, n));
        }

        // Бинарные файлы, содержащие величины типа struct (заполнение исходного файла организовать отдельным методом).
        // Файл содержит сведения об игрушках: название игрушки, ее стоимость в рублях и возрастные границы (например, игрушка может предназначаться для детей от двух до пяти лет).
        // Определить стоимость самого дорогого конструктора
        public static void RunTask3()
        {
            // --- создание исходного файла ---

            Toy[] toyz = new Toy[]
            {
                new Toy("LEGO Bugatti Bolide", 10149),
                new Toy("Star Wars Йода Animatronic", 6799, 3, 6),
                new Toy("LEGO Technic Ferrari 488 GTE", 13299, 18, null)
            };

            Console.WriteLine("Список игрушек:");
            foreach (Toy toy in toyz) Console.WriteLine($"- {toy}");
            Console.WriteLine();

            string fileName = GetFileName("toyz", 3, true);
            if (!WriteToyzToBinaryFile(fileName, toyz))
                return;

            // --- чтение файла со сведениями об игрушках ---

            Toy[]? loadedToyz = ReadToyzFromBinaryFile(fileName);
            if (loadedToyz == null)
                return;

            // --- решение задачи ---

            Toy? mostExpensive = null;

            foreach (Toy toy in toyz)
                if (!mostExpensive.HasValue || mostExpensive.Value.Price <= toy.Price)
                    mostExpensive = toy;

            if (!mostExpensive.HasValue)
                return;

            // --- вывод результата

            Console.WriteLine($"Самая дорогая игрушка: '{mostExpensive.Value.Name}' ({mostExpensive.Value.Price} Р)");
        }

        // Решить задачу с использованием структуры «текстовый файл» (в файле хранятся целые числа по одному в строке).
        // Найти сумму тех элементов файла, которые равны своему индексу (индексацию элементов файла в этой задаче начинать с нуля).
        public static void RunTask4()
        {
            Console.WriteLine("Доступные методы создания исходного файла:");
            Console.WriteLine("1. Сгенерировать автоматически");
            Console.WriteLine("2. Заполнить вручную");
            int action = RequestInteger("Выберите метод: ", 1, 2);
            Console.WriteLine();

            string fileName = GetFileName("numbers", 4);
            if (action == 1)
            {
                // --- генерация исходного файла со случайными числами ---
                int count = RequestPositiveInteger("Введите количество генерируемых чисел: ");
                Console.WriteLine();

                if (!GenerateRandomNumbersTextFile(fileName, count, 0, count))
                    return;
            }
            else
            {
                // --- заполнение исходного файла вручную ---
                if (!AwaitManualInput(fileName))
                    return;
            }

            // --- решение поставленной задачи ---

            long sum = 0;
            int index = 0;
            string? line;
            int number;

            try
            {
                StreamReader reader = new StreamReader(File.Open(fileName, FileMode.Open));
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
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при чтении исходного файла!\n{ex.Message}");
                return;
            }

            // --- вывод результата

            Console.WriteLine($"Результат: {sum}");
        }

        // Решить задачу с использованием структуры «текстовый файл» (в файле хранятся целые числа по несколько в строке).
        // Вычислить произведение элементов, которые кратны заданному числу k.
        public static void RunTask5()
        {
            Console.WriteLine("Доступные методы создания исходного файла:");
            Console.WriteLine("1. Сгенерировать автоматически");
            Console.WriteLine("2. Заполнить вручную");
            int action = RequestInteger("Выберите метод: ", 1, 2);
            Console.WriteLine();

            string fileName = GetFileName("number-sequences", 5);
            if (action == 1)
            {
                // --- генерация исходного файла со случайными числами ---
                int count = RequestPositiveInteger("Введите количество генерируемых чисел: ");
                Console.WriteLine();

                if (!GenerateRandomNumberSequencesTextFile(fileName, count, 1, 100, 0.2))
                    return;
            }
            else
            {
                // --- заполнение исходного файла вручную ---
                if (!AwaitManualInput(fileName))
                    return;
            }

            // --- решение поставленной задачи ---

            int k = RequestNonZeroInteger("Введите значение 'k': ");
            Console.WriteLine();

            long mult = 0;
            string? line;
            int number;

            try
            {
                StreamReader reader = new StreamReader(File.Open(fileName, FileMode.Open));
                while (true)
                {
                    line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;

                    foreach (string arg in line.Split(' '))
                    {
                        if (arg.Length != 0 && int.TryParse(arg, out number) && number != 0 && number % k == 0)
                        {
                            if (mult == 0)
                                mult = 1;

                            Console.WriteLine($"Найдено число {number}, кратное {k}...");
                            mult *= number;
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при чтении исходного файла!\n{ex.Message}");
                return;
            }

            // --- вывод результата

            Console.WriteLine($"Результат: {mult}");
        }

        // Решить задачу с использованием структуры «текстовый файл» (в файле хранится текст)
        // Переписать в другой файл строки, в которых нет русских букв.
        public static void RunTask6()
        {
            // --- заполнение исходного файла вручную ---

            string inputFile = GetFileName("input", 6);
            if (!AwaitManualInput(inputFile))
                return;

            // --- решение задачи ---

            string outputFile = GetFileName("output", 6);
            string? line;
            bool linePassed;

            try
            {
                StreamReader reader = new StreamReader(File.Open(inputFile, FileMode.Open));
                StreamWriter writer = new StreamWriter(File.Open(outputFile, FileMode.Create));

                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;

                    linePassed = true;
                    foreach (char ch in line)
                    {
                        if (ch >= 'А' && ch <= 'Я' || ch >= 'а' && ch <= 'я')
                        {
                            linePassed = false;
                        }
                    }

                    if (linePassed)
                    {
                        Console.WriteLine($"Строка '{line}' добавлена в файл...");
                        writer.WriteLine(line);
                    }
                    else
                    {
                        Console.WriteLine($"Строка '{line}' пропущена, т.к. содержит русские буквы...");
                    }
                }

                reader.Close();
                writer.Close();
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при перезаписи строк между файлами!\n{ex.Message}");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Файл с результатами сохранён по пути:\n" + Path.GetFullPath(outputFile));
            Console.WriteLine();

            // --- вывод результата

            try
            {
                Process process = Process.Start("notepad.exe", outputFile);
                Console.WriteLine("Готово!");
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при открытии файла с результатами в Блокноте!\n{ex.Message}");
            }
        }

        // чтение массива игрушек из бинарного файла
        private static Toy[]? ReadToyzFromBinaryFile(string fileName)
        {
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));
                BinaryFormatter formatter = new BinaryFormatter();
                Toy[]? toyz = formatter.Deserialize(reader.BaseStream) as Toy[];
                reader.Close();
                return toyz;
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при чтении сведений об игрушках из файла!\n{ex.Message}");
                return null;
            }
        }

        // запись массива игрушек в бинарный файл
        private static bool WriteToyzToBinaryFile(string fileName, Toy[] toyz)
        {
            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(writer.BaseStream, toyz);
                writer.Flush();
                writer.Close();

                Console.WriteLine("Файл со сведениями об игрушках сохранён по пути:\n" + Path.GetFullPath(fileName));
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при записи сведений об игрушках в файл!\n{ex.Message}");
                return false;
            }
        }

        // заполнение текстового файла случайно сгенерированными последовательностями чисел
        private static bool GenerateRandomNumberSequencesTextFile(string fileName, int count, int min, int max, double lineBreakChance)
        {
            try
            {
                StreamWriter writer = new StreamWriter(File.Open(fileName, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                {
                    writer.Write(random.Next(min, max));

                    if (random.NextDouble() < lineBreakChance)
                        writer.WriteLine();
                    else
                        writer.Write(' ');
                }

                writer.Flush();
                writer.Close();

                Console.WriteLine("Файл со случайными числами сохранён по пути:\n" + Path.GetFullPath(fileName));
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при генерации исходного файла!\n{ex.Message}");
                return false;
            }
        }

        // заполнение текстового файла случайно сгенерированными числами
        private static bool GenerateRandomNumbersTextFile(string fileName, int count, int min, int max)
        {
            try
            {
                StreamWriter writer = new StreamWriter(File.Open(fileName, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                {
                    writer.Write(random.Next(min, max));
                    writer.WriteLine();
                }

                writer.Flush();
                writer.Close();

                Console.WriteLine("Файл со случайными числами сохранён по пути:\n" + Path.GetFullPath(fileName));
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при генерации исходного файла!\n{ex.Message}");
                return false;
            }
        }

        // заполнение бинарного файла случайно сгенерированными числами
        private static bool GenerateRandomNumbersBinaryFile(string fileName, int count, int min, int max, int[]? writtenNumbers = null)
        {
            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                {
                    int number = random.Next(min, max);
                    writer.Write(number);

                    if (writtenNumbers != null)
                        writtenNumbers[i] = number;
                }

                writer.Flush();
                writer.Close();

                Console.WriteLine("Файл со случайными числами сохранён по пути:\n" + Path.GetFullPath(fileName));
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при генерации исходного файла!\n{ex.Message}");
                return false;
            }
        }

        // ожидание ручного ввода данных в файл
        private static bool AwaitManualInput(string fileName)
        {
            if (!File.Exists(fileName))
            {
                try
                {
                    File.Create(fileName).Close();
                }
                catch (Exception ex)
                {
                    PrintError($"Ошибка при создании исходного файла!\n{ex.Message}");
                    return false;
                }
            }

            try
            {
                Process process = Process.Start("notepad.exe", fileName);

                Console.WriteLine("Ожидание ручного ввода исходных данных в файл...");
                Console.WriteLine();

                process.WaitForExit();
                return true;
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при открытии исходного файла в Блокноте!\n{ex.Message}");
                return false;
            }
        }

        // запрос ввода целого числа в заданном диапазоне
        private static int RequestInteger(string prompt, int min, int max)
        {
            bool success;
            int value;

            do
            {
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max;
                if (!success)
                    PrintError($"Введенные данные имеют неверный формат!\nУкажите целое число в диапазоне от {min} до {max}.");
            } while (!success);

            return value;
        }

        // запрос ввода целого числа, отличного от 0
        private static int RequestNonZeroInteger(string prompt)
        {
            bool success;
            int value;

            do
            {
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out value) && value != 0;
                if (!success)
                    PrintError("Введенные данные имеют неверный формат!\nУкажите целое число, отличное от 0.");
            } while (!success);

            return value;
        }

        // запрос ввода целого положительного числа
        private static int RequestPositiveInteger(string prompt)
        {
            bool success;
            int value;

            do
            {
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out value) && value > 0;
                if (!success)
                    PrintError("Введенные данные имеют неверный формат!\nУкажите целое положительное число.");
            } while (!success);

            return value;
        }

        // форматирование матрицы в строку
        private static string Matrix2DToString(int[,] matrix2D, int n)
        {
            StringBuilder builder = new StringBuilder();
            string number, spacesLine = "    ";
            int spacesCount;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    number = matrix2D[i, j].ToString();
                    spacesCount = 3 - number.Length;

                    builder.Append(spacesLine[..spacesCount]);
                    builder.Append(number);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        // печать сообщения в консоль красным цветом
        private static void PrintError(string message)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = tmp;
        }

        // форматирование имени файла
        private static string GetFileName(string baseName, int taskNumber, bool isBinaryFile = false)
        {
            string extension = isBinaryFile ? "bin" : "txt";
            return $"task{taskNumber}-{baseName}.{extension}";
        }
    }
}
