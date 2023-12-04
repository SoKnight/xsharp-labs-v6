// Бинарные файлы, содержащие числовые данные.
// Исходный файл заполнить случайными данными.
// Заполнение организовать отдельным методом.

// Переписать в другой файл последовательного доступа те элементы, которые кратны k.

namespace Lab6
{
    internal class Task1
    {
        private static readonly string INPUT_FILE_NAME = "input.bin";
        private static readonly string OUTPUT_FILE_NAME = "output.bin";

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача: Переписать в другой файл последовательного доступа те элементы, которые кратны k.");
            Console.WriteLine();

            int count = UserInput.RequestPositiveInteger("Введите количество исходных чисел: ");
            int k = UserInput.RequestNonZeroInteger("Введите значение 'k': ");
            Console.WriteLine();

            // --- генерация исходного файла со случайными числами ---

            int[] writtenNumbers = new int[count];
            if (!GenerateRandomNumbersFile(count, 1000, writtenNumbers))
                return;

            Console.WriteLine($"Сгенерированные числа: [{String.Join(", ", writtenNumbers)}]");
            Console.WriteLine();

            // --- решение задачи ---

            int countOfWritten;
            if (!SolveTask(k, writtenNumbers, out countOfWritten))
                return;

            if (countOfWritten == 0)
            {
                Console.WriteLine($"Числа, кратные {k}, отсутствуют.");
            }
            else
            {
                Array.Resize(ref writtenNumbers, countOfWritten);
                Console.WriteLine($"Числа, кратные {k}: [{String.Join(", ", writtenNumbers)}]");
            }
        }

        private static bool SolveTask(int k, int[] writtenNumbers, out int countOfWritten)
        {
            countOfWritten = 0;

            try
            {
                BinaryReader reader = new BinaryReader(File.Open(INPUT_FILE_NAME, FileMode.Open));
                BinaryWriter writer = new BinaryWriter(File.Open(OUTPUT_FILE_NAME, FileMode.Create));

                int writtenNumbersIndex = 0;
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();

                    if (number % k == 0)
                    {
                        writer.Write(number);
                        writtenNumbers[writtenNumbersIndex++] = number;
                        countOfWritten++;
                    }
                }

                reader.Close();
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка файловой системы при попытке решить задачу!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return false;
            }
        }

        private static bool GenerateRandomNumbersFile(int count, int maxBound, int[] writtenNumbers)
        {
            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(INPUT_FILE_NAME, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                {
                    int number = random.Next(maxBound);
                    writer.Write(number);
                    writtenNumbers[i] = number;
                }

                writer.Flush();
                writer.Close();
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