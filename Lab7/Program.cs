namespace Lab7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная #7 (Вариант 6)");
            int taskNumber = RequestInteger("Укажите номер задачи: ", 1, 5);

            Console.WriteLine();
            Console.WriteLine($"----- ЗАДАЧА #{taskNumber} -----");
            Console.WriteLine();

            switch (taskNumber)
            {
                case 1:
                    Tasks.RunTask1();
                    break;
                case 2:
                    Tasks.RunTask2();
                    break;
                case 3:
                    Tasks.RunTask3();
                    break;
                case 4:
                    Tasks.RunTask4();
                    break;
                case 5:
                    Tasks.RunTask5();
                    break;
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

        // печать сообщения в консоль красным цветом
        private static void PrintError(string message)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = tmp;
        }
    }
}