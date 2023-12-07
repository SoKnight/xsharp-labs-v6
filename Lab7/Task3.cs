// Решить задачу, используя класс HashSet.

// Есть перечень музыкальных произведений.
// Определить для каждого произведения, какие из них нравятся всем n меломанам, какие — некоторым из меломанов, и какие — никому из меломанов.

namespace Lab7
{
    internal class Task3
    {
        private class MusicLover : HashSet<string> {}

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача:");
            Console.WriteLine("Есть перечень музыкальных произведений.");
            Console.WriteLine("Определить для каждого произведения, какие из них нравятся всем n меломанам, какие — некоторым из меломанов, и какие — никому из меломанов.");
            Console.WriteLine();

            HashSet<string> compositions = InputMusicCompositions();
            Console.WriteLine();

            HashSet<MusicLover> musicLovers = InputMusicLovers();
            Console.WriteLine();

            // --- решение задачи ---

            Console.WriteLine("--- РЕЗУЛЬТАТ ---");
            int musicLoversCount = musicLovers.Count;
            foreach (string composition in compositions)
            {
                int matches = 0;

                foreach (MusicLover lover in musicLovers)
                    if (lover.Contains(composition))
                        matches++;

                if (matches == 0)
                    Console.WriteLine($"Произведение '{composition}' не нравится никому из меломанов :(");
                else if (matches == musicLoversCount)
                    Console.WriteLine($"Произведение '{composition}' нравится всем меломанам :)");
                else
                    Console.WriteLine($"Произведение '{composition}' нравится некоторым из меломанов.");
            }
        }

        private static HashSet<string> InputMusicCompositions()
        {
            Console.WriteLine("Начинайте вводить названия произведений ниже...");
            Console.WriteLine("Когда ввод нужно будет закончить - отправьте пустую строку.");
            Console.WriteLine();

            Console.WriteLine("--- ПРОИЗВЕДЕНИЯ ---");
            HashSet<string> result = new HashSet<string>();
            while (true)
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    if (result.Count == 0)
                        continue;
                    else
                        break;
                }

                result.Add(line);
            }

            Console.WriteLine($"Добавлено произведений: {result.Count}");
            return result;
        }

        private static HashSet<MusicLover> InputMusicLovers()
        {
            Console.WriteLine("Начинайте вводить информацию о меломанах ниже...");
            Console.WriteLine("Формат: '<произведение1>;; <произведение2>;; ...;; <произведениеN>'");
            Console.WriteLine("Когда ввод нужно будет закончить - отправьте пустую строку.");
            Console.WriteLine();

            Console.WriteLine("--- МЕЛОМАНЫ ---");
            HashSet<MusicLover> result = new HashSet<MusicLover>();
            while (true)
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    if (result.Count == 0)
                        continue;
                    else
                        break;
                }

                MusicLover lover = new MusicLover();
                foreach (string composition in line.Split(";; "))
                    if (composition.Length != 0)
                        lover.Add(composition);

                if (lover.Count != 0)
                    result.Add(lover);
            }

            Console.WriteLine($"Добавлено меломанов: {result.Count}");
            return result;
        }
    }
}
