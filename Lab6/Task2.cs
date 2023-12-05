// Бинарные файлы, содержащие числовые данные.
// Исходный файл заполнить случайными данными.
// Заполнение организовать отдельным методом.

// Скопировать элементы заданного файла в квадратную матрицу размером n×n.
// Если элементов файла недостает, заполнить оставшиеся элементы матрицы нулями.
// Заменить все столбцы на столбец с минимальной суммой элементов.

using System.Text;

namespace Lab6
{
    internal class Task2
    {
        private static readonly string FILE_NAME = "numbers.bin";

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача:");
            Console.WriteLine("Скопировать элементы заданного файла в квадратную матрицу размером n X n.");
            Console.WriteLine("Если элементов файла недостает, заполнить оставшиеся элементы матрицы нулями.");
            Console.WriteLine("Заменить все столбцы на столбец с минимальной суммой элементов.");
            Console.WriteLine();

            int count = UserInput.RequestPositiveInteger("Введите количество исходных чисел: ");
            int n = UserInput.RequestPositiveInteger("Введите размерность матрицы 'n': ");
            Console.WriteLine();

            // --- генерация исходного файла со случайными числами ---

            if (!GenerateRandomNumbersFile(count, 1000))
                return;

            // --- создание 2D матрицы ---

            int[][] matrix2D = new int[n][];
            for (int i = 0; i < n; i++)
                matrix2D[i] = new int[n];

            // --- чтение сгенерированных чисел в матрицу ---

            if (!ReadNumbersToMatrix2D(matrix2D, n))
                return;

            Console.WriteLine(" ИСХОДНАЯ МАТРИЦА:");
            Console.WriteLine(Matrix2DToString(matrix2D, n));

            // --- решение задачи ---

            int leastSum = 0;
            int leastSumColumnIndex = FindLeastSumColumnIndex(matrix2D, n, out leastSum);
            Console.WriteLine($"Столбец #{leastSumColumnIndex+1} имеет минимальную сумму элементов: {leastSum}");
            Console.WriteLine();

            SolveTask(matrix2D, n, leastSumColumnIndex);

            Console.WriteLine(" РЕЗУЛЬТАТ:");
            Console.WriteLine(Matrix2DToString(matrix2D, n));
        }

        private static void SolveTask(int[][] matrix2D, int n, int leastSumColumnIndex)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == leastSumColumnIndex)
                        continue;

                    matrix2D[i][j] = matrix2D[i][leastSumColumnIndex];
                }
            }
        }

        private static int FindLeastSumColumnIndex(int[][] matrix2D, int n, out int leastSum)
        {
            leastSum = int.MaxValue;
            int columnIndex = 0;

            for (int i = 0; i < n; i++)
            {
                int currentSum = 0;

                for (int j = 0; j < n; j++)
                {
                    currentSum += matrix2D[j][i];
                }

                if (currentSum <= leastSum)
                {
                    leastSum = currentSum;
                    columnIndex = i;
                }
            }

            return columnIndex;
        }

        private static bool ReadNumbersToMatrix2D(int[][] matrix2D, int n)
        {
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(FILE_NAME, FileMode.Open));

                int index = 0;
                int limit = n * n;

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    if (index >= limit)
                        break;

                    int number = reader.ReadInt32();
                    matrix2D[index / n][index % n] = number;
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
                BinaryWriter writer = new BinaryWriter(File.Open(FILE_NAME, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                    writer.Write(random.Next(maxBound));

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

        private static string Matrix2DToString(int[][] matrix2D, int n)
        {
            StringBuilder builder = new StringBuilder();
            string number, spacesLine = "    ";
            int spacesCount;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    number = matrix2D[i][j].ToString();
                    spacesCount = 4 - number.Length;

                    builder.Append(spacesLine[..spacesCount]);
                    builder.Append(number);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static int AcquireLengthOfNumber(int number)
        {
            if (number == 0)
                return 1;

            int length = number < 0 ? 1 : 0;
            while (number != 0)
            {
                number %= 10;
                length++;
            }

            return length;
        }
    }
}