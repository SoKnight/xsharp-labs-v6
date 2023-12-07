using System.Diagnostics;
using System.Text;

namespace Lab6
{
    internal class UserInput
    {
        // ожидание подтверждения
        public static void AwaitConfirmation(string prompt)
        {
            Console.Write(prompt);
            Console.ReadLine();
        }

        // ожидание ввода данных вручную
        public static bool AwaitManualInput(string fileName)
        {
            if (!File.Exists(fileName))
            {
                try
                {
                    File.Create(fileName).Close();
                }
                catch (Exception ex)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка при создании заполняемого файла!");
                    Console.WriteLine(ex.ToString());
                    Console.ForegroundColor = tmp;
                    return false;
                }
            }

            try
            {
                Process process = Process.Start("notepad.exe", fileName);
                process.WaitForExit();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при попытке предоставлении возможности заполнения файла вручную!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return false;
            }
        }

        // запрос ввода целого числа с проверкой
        public static int RequestInteger(string prompt)                                                                                        
        {
            bool success;
            int value;

            do
            {
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out value);
                if (!success)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Введенные данные имеют неверный формат!");
                    Console.WriteLine("Укажите целое число.");
                    Console.ForegroundColor = tmp;
                }
            } while (!success);

            return value;
        }

        // запрос ввода целого числа в указанном диапазоне с проверкой
        public static int RequestIntegerInBounds(string prompt, int minInclusive, int maxInclusive)
        {
            bool success;
            int value;

            do
            {
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out value) && value >= minInclusive && value <= maxInclusive;
                if (!success)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Введенные данные имеют неверный формат!");
                    Console.WriteLine($"Укажите целое число в диапазоне от {minInclusive} до {maxInclusive}.");
                    Console.ForegroundColor = tmp;
                }
            } while (!success);

            return value;
        }

        // запрос ввода отличного от 0 целого числа с проверкой
        public static int RequestNonZeroInteger(string prompt)
        {
            bool success;
            int value;

            do
            {
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out value) && value != 0;
                if (!success)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Введенные данные имеют неверный формат!");
                    Console.WriteLine("Укажите целое число, отличное от 0.");
                    Console.ForegroundColor = tmp;
                }
            } while (!success);

            return value;
        }

        // запрос ввода целого числа с проверкой
        public static int RequestPositiveInteger(string prompt)
        {
            bool success;
            int value;

            do
            {
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out value) && value > 0;
                if (!success)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Введенные данные имеют неверный формат!");
                    Console.WriteLine("Укажите целое положительное число.");
                    Console.ForegroundColor = tmp;
                }
            } while (!success);

            return value;
        }

        // запрос ввода вещественного числа с проверкой
        public static double RequestDouble(string prompt)
        {
            bool success;
            double value;

            do
            {
                Console.Write(prompt);
                success = double.TryParse(Console.ReadLine(), out value);
                if (!success)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Введенные данные имеют неверный формат!");
                    Console.WriteLine("Укажите вещественное число.");
                    Console.ForegroundColor = tmp;
                }
            } while (!success);

            return value;
        }
    }
}
