// Решить задачу, используя класс LinkedList.

// Составить программу, которая проверяет, есть ли в списке L хотя бы два одинаковых элемента.

namespace Lab7
{
    internal class Task2
    {
        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача: Удалить из списка L первое вхождение заданного элемента, если такой есть.");
            Console.WriteLine();

            LinkedList<string> list = new LinkedList<string>();

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

                list.AddLast(line);
            }

            Console.WriteLine($"Введено строк: {list.Count}");
            string removing = UserInput.RequestNotEmptyString("Введите строку для удаления: ");
            Console.WriteLine();

            // --- решение задачи ---

            LinkedListNode<string>? node = list.First;
            bool removed = false;

            while (node != null)
            {
                LinkedListNode<string>? next = node.Next;

                if (node.Value == removing)
                {
                    list.Remove(node);
                    removed = true;
                    break;
                }

                node = next;
            }

            // --- вывод результата ---

            if (!removed)
            {
                Console.WriteLine($"Список не содержит элемента '{removing}'!");
                return;
            }
            else
            {
                Console.WriteLine("--- РЕЗУЛЬТАТ ---");
                node = list.First;
                while (node != null)
                {
                    Console.WriteLine(node.Value);
                    node = node.Next;
                }
            }
        }
    }
}
