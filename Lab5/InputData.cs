namespace Lab5
{
    internal class InputData
    {
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
                    Console.Write("Укажите целое число: ");
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
                    Console.Write("Укажите вещественное число: ");
                    Console.ForegroundColor = tmp;
                }
            } while (!success);

            return value;
        }
    }
}
