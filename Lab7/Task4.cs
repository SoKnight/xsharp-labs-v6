// Решить задачу, используя класс HashSet.

// Файл содержит текст на русском языке.
// Напечатать в алфавитном порядке все гласные буквы, которые не входят более чем в одно слово.

namespace Lab7
{
    internal class Task4
    {
        private static readonly string FILE_NAME = "task4_text_on_russian.txt";
        private static readonly HashSet<char> VOWELS = new HashSet<char>("аяоёуюэеыи".ToCharArray());

        private class WordVowels : HashSet<char> { }

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача:");
            Console.WriteLine("Файл содержит текст на русском языке.");
            Console.WriteLine("Напечатать в алфавитном порядке все гласные буквы, которые не входят более чем в одно слово.");
            Console.WriteLine();

            // --- заполнение исходного файла вручную ---

            Console.WriteLine("Введите текст на русском языке в исходном файле...");
            Console.WriteLine();

            if (!UserInput.AwaitManualInput(FILE_NAME))
                return;

            // --- решение задачи ---

            HashSet<WordVowels> words = new HashSet<WordVowels>();
            if (!ReadAllWords(words))
                return;

            HashSet<char> result = new HashSet<char>();
            foreach (char vowel in VOWELS)
            {
                int count = 0;

                foreach (WordVowels word in words)
                {
                    if (word.Contains(vowel))
                        count++;

                    if (count > 1)
                        break;
                }

                if (count <= 1)
                    result.Add(vowel);
            }

            // --- вывод результата ---

            if (result.Count == 0)
            {
                Console.WriteLine("Подходящих по условию гласных букв не найдено!");
            }
            else
            {
                // 'ё' > 'я'
                char[] alphabetical = "аеёиоуыэюя".ToCharArray();
                IOrderedEnumerable<char> ordered = result.OrderBy(x => Array.IndexOf(alphabetical, x));
                Console.WriteLine($"Результат: [{string.Join(", ", ordered).ToUpper()}]");
            }
        }

        private static bool ReadAllWords(HashSet<WordVowels> words)
        {
            try
            {
                StreamReader reader = new StreamReader(File.Open(FILE_NAME, FileMode.Open));

                string? line;
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;

                    if (line.Length == 0)
                        continue;

                    foreach (string word in line.Split(' '))
                    {
                        if (word.Length != 0)
                        {
                            WordVowels instance = new WordVowels();

                            foreach (char ch in word.ToLower())
                                if (VOWELS.Contains(ch))
                                    instance.Add(ch);

                            if (instance.Count != 0)
                                words.Add(instance);
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
    }
}
