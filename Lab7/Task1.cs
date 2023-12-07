// Решить задачу, используя класс List.

// Составить программу, которая проверяет, есть ли в списке L хотя бы два одинаковых элемента.

namespace Lab7
{
    internal class Task1
    {
        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача: Составить программу, которая проверяет, есть ли в списке L хотя бы два одинаковых элемента.");
            Console.WriteLine();

            List<string> list = new List<string>();

            Console.WriteLine("Начинайте вводить строки ниже...");
            Console.WriteLine("Когда ввод нужно будет закончить - отправьте пустую строку.");
            Console.WriteLine();

            Console.WriteLine("--- СОДЕРЖИМОЕ СПИСКА ---");
            while (true)
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    if (list.Count == 0)
                        continue;
                    else
                        break;
                }

                list.Add(line);
            }
            Console.WriteLine($"Введено строк: {list.Count}");

            // --- решение задачи ---

            // альтернативы: Distinct, Count
            for (int i = 0; i < list.Count - 1; i++)
            {
                string item = list[i];
                if (list.LastIndexOf(item) != i)
                {
                    Console.WriteLine($"Элемент '{item}' встречается в списке более 1 раза.");
                    return;
                }
            }

            Console.WriteLine("В списке нет повторяющихся элементов!");
        }
    }
}
