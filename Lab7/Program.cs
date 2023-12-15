using System.Diagnostics;

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
                    RunTask1();
                    break;
                case 2:
                    RunTask2();
                    break;
                case 3:
                    RunTask3();
                    break;
                case 4:
                    RunTask4();
                    break;
                case 5:
                    RunTask5();
                    break;
            }
        }

        // Решить задачу, используя класс List.
        // Составить программу, которая проверяет, есть ли в списке L хотя бы два одинаковых элемента.
        private static void RunTask1()
        {
            // --- ввод данных ---

            Console.WriteLine("Начинайте вводить строки для формирования списка ниже...");
            Console.WriteLine("Когда ввод нужно будет закончить - отправьте пустую строку.");
            Console.WriteLine();

            List<string> input = RequestStringsList("ИСХОДНЫЙ СПИСОК");
            Console.WriteLine();

            // --- решение задачи ---

            if (IsListContainsDuplicates(input))
            {
                Console.WriteLine("В заданном списке найден как минимум 1 повторяющийся элемент.");
            }
            else
            {
                Console.WriteLine("В заданном списке нет повторяющихся элементов.");
            }
        }

        // Решить задачу, используя класс LinkedList.
        // Удалить из списка L первое вхождение заданного элемента, если такой есть.
        private static void RunTask2()
        {
            // --- ввод данных ---

            Console.WriteLine("Начинайте вводить строки для формирования списка ниже...");
            Console.WriteLine("Когда ввод нужно будет закончить - отправьте пустую строку.");
            Console.WriteLine();

            LinkedList<string> input = RequestStringsLinkedList("ИСХОДНЫЙ СПИСОК");
            Console.WriteLine();

            string element = RequestString("Введите удаляемый элемент: ");
            Console.WriteLine();

            // --- решение задачи ---

            if (DeleteFirstOccurrence(input, element))
            {
                Console.WriteLine("--- РЕЗУЛЬТАТ ---");
                LinkedListNode<string>? node = input.First;
                while (node != null)
                {
                    Console.WriteLine(node.Value);
                    node = node.Next;
                }
            }
            else
            {
                Console.WriteLine($"Заданный список не содержит элемента '{element}'!");
            }
        }

        // Решить задачу, используя класс HashSet.
        // Есть перечень музыкальных произведений.
        // Определить для каждого произведения, какие из них нравятся всем n меломанам, какие — некоторым из меломанов, и какие — никому из меломанов.
        private static void RunTask3()
        {
            // --- ввод данных ---

            Console.WriteLine("Начинайте вводить названия музыкальных произведений ниже...");
            Console.WriteLine("Когда ввод нужно будет закончить - отправьте пустую строку.");
            Console.WriteLine();

            HashSet<string> compositions = RequestStringsHashSet("МУЗЫКАЛЬНЫЕ ПРОИЗВЕДЕНИЯ");

            Console.WriteLine("Начинайте вводить сведения о меломанах ниже...");
            Console.WriteLine("Формат ввода: <произведение1>; <произведение2>; ...; <произведениеN>");
            Console.WriteLine("Когда ввод нужно будет закончить - отправьте пустую строку.");
            Console.WriteLine();

            HashSet<HashSet<string>> musicLovers = RequestStringsHashSet2D("СВЕДЕНИЯ О МЕЛОМАНАХ", "; ");

            // --- решение задачи ---

            Console.WriteLine("--- РЕЗУЛЬТАТ ---");

            HashSet<string> lovedByAll = new HashSet<string>(compositions);
            HashSet<string> unitedMusicLovers = new HashSet<string>();

            foreach (HashSet<string> musicLover in musicLovers)
            {
                lovedByAll.IntersectWith(musicLover);
                unitedMusicLovers.UnionWith(musicLover);
            }

            // --- нравятся всем

            if (lovedByAll.Count != 0)
            {
                PrintCompositionsGroup(lovedByAll, "Нравятся всем");
                compositions.ExceptWith(lovedByAll);
            }

            unitedMusicLovers.IntersectWith(compositions);

            // --- нравятся некоторым

            if (unitedMusicLovers.Count != 0)
            {
                PrintCompositionsGroup(unitedMusicLovers, "Нравятся некоторым");
                compositions.ExceptWith(unitedMusicLovers);
            }

            // --- не нравятся никому

            if (compositions.Count != 0)
                PrintCompositionsGroup(compositions, "Не нравятся никому");
        }

        // Решить задачу, используя класс HashSet.
        // Файл содержит текст на русском языке.
        // Напечатать в алфавитном порядке все гласные буквы, которые не входят более чем в одно слово.
        private static void RunTask4()
        {
            // --- ввод данных ---

            string fileName = GetFileName("text-on-russian", 4);
            if (!AwaitManualInput(fileName))
                return;

            // --- решение задачи ---

            HashSet<char> vowels = new HashSet<char>("аяоёуюэеыи");
            HashSet<char>? result = FilterWordCharsInTextFile(vowels, fileName);

            if (result == null || result.Count == 0)
            {
                Console.WriteLine("Не удалось найти гласные буквы, удовлетворяющие условию задачи.");
                return;
            }

            // 'ё' > 'я'
            char[] alphabetical = "аеёиоуыэюя".ToCharArray();
            IOrderedEnumerable<char> ordered = result.OrderBy(x => Array.IndexOf(alphabetical, x));
            Console.WriteLine($"Результат: [{string.Join(", ", ordered).ToUpper()}]");
        }

        // класс, описывающий участника олимпиады для задания #5
        public class Participant
        {
            public string FirstName { init; get; }
            public string LastName { init; get; }
            public int Score { init; get; }

            public Participant(string firstName, string lastName, int score)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.Score = score;
            }
        }

        // Решить задачу, используя класс Dictionary (или класс SortedList).
        // Входные данные читать из текстового файла.
        // На вход программе сначала подается число участников олимпиады N.
        // В каждой из следующих N строк находится результат одного из участников олимпиады в следующем формате:
        //   < Фамилия > < Имя > < класс > < баллы >
        // где:
        //   < Фамилия > – символьная строка(не более 20 символов),
        //   < Имя > – символьная строка(не более 15 символов),
        //   < класс > – число от 7 до 11,
        //   < баллы > – целое число набранных участником баллов.
        // <Фамилия> и <Имя>, <Имя> и <класс>, а также <класс> и <баллы> разделены одним пробелом.
        // Пример входной строки:
        //   Семенов Егор 11 225
        //
        // Победителем олимпиады становится участник, набравший наибольшее количество баллов, при условии, что он набрал более 200 баллов.
        // Если такое количество баллов набрали несколько участников, то все они признаются победителями при выполнении условия,
        // что их доля не превышает 20% от общего числа участников.
        // Победителем олимпиады не признается никто, если нет участников, набравших больше 200 баллов,
        // или больше 20% от общего числа участников набрали одинаковый наибольший балл.
        //
        // Напишите эффективную по времени работы и по используемой памяти программу, которая будет определять фамилию и имя лучшего участника, не ставшего победителем олимпиады.
        // Если таких участников несколько, т.е. если следующий за баллом победителей один и тот же балл набрали несколько человек, или, если победителей нет,
        // а лучших участников несколько (в этом случае именно они являются искомыми), то выдается только количество искомых участников.
        // Гарантируется, что искомые участники (участник) имеются.
        //
        // Программа должна выводить через пробел фамилию и имя искомого участника или их количество.
        // Пример выходных данных (один искомый участник):
        //   Семенов Егор
        // Второй вариант выходных данных (несколько искомых участников):
        //   12
        private static void RunTask5()
        {
            // --- ввод данных ---

            Console.WriteLine("Формат данных: <фамилия> <имя> <класс> <баллы>");
            Console.WriteLine("Накладываемые ограничения:");
            Console.WriteLine("- <фамилия> - строка до 20 символов");
            Console.WriteLine("- <имя> - строка до 15 символов");
            Console.WriteLine("- <класс> - число от 7 до 11");
            Console.WriteLine("- <баллы> - целое неотрицательное число");
            Console.WriteLine();

            string fileName = GetFileName("participants", 5);
            if (!AwaitManualInput(fileName))
                return;

            // --- решение задачи ---

            SortedList<int, object>? participants;
            int totalCount;
            if (!ReadParticipantsFromFile(fileName, out participants, out totalCount) || participants == null || participants.Count == 0)
                return;

            // --- вывод результата ---

            bool winnerFound = false;
            foreach (KeyValuePair<int, object> pair in participants)
            {
                if (!winnerFound && IsWinner(pair.Key, pair.Value, totalCount))
                {
                    winnerFound = true;
                    continue;
                }

                PrintResult(pair.Value);
                return;
            }

            PrintError("Вероятно, что-то было введено не по условию задачи :(");
        }

        // печать результата работы алгоритма решения задачи #5
        private static void PrintResult(object result)
        {
            if (result is Participant)
            {
                Participant participant = (Participant)result;
                Console.WriteLine($"Результат: {participant.LastName} {participant.FirstName}");
            }
            else if (result is int)
            {
                Console.WriteLine($"Результат: {result}");
            }
        }

        // реализация проверки условия победителя для задания #5
        private static bool IsWinner(int score, object value, int totalCount)
        {
            if (score > 200)
                return true;

            if (value is int && ((double)value) / totalCount <= 0.2)
                return true;

            return false;
        }

        // компаратор, используемый в задании #5
        public class ReversedIntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y.CompareTo(x);
            }
        }

        // чтение сведений об участниках из файла (задание #5)
        private static bool ReadParticipantsFromFile(string fileName, out SortedList<int, object>? list, out int totalCount)
        {
            list = null;
            totalCount = 0;

            try
            {
                StreamReader reader = new StreamReader(File.Open(fileName, FileMode.Open));

                int currentCount = 0;
                bool first = true;

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    // --- чтение количества участников с первой строки ---

                    if (first)
                    {
                        if (!int.TryParse(line, out totalCount) || totalCount <= 0)
                        {
                            PrintError("Ошибка при чтении исходного файла!\nКоличество участников должно быть явно указано в первой строке.");
                            reader.Close();
                            return false;
                        }

                        if (totalCount == 0)
                        {
                            PrintError("Ошибка при чтении исходного файла!\nКоличество участников в первой строке не может быть равно 0.");
                            reader.Close();
                            return false;
                        }

                        list = new SortedList<int, object>(new ReversedIntComparer());
                        first = false;
                        continue;
                    }

                    // --- чтение данных об участнике ---

                    if (++currentCount > totalCount)
                    {
                        PrintError("Ошибка при чтении исходного файла!\nРеальное количество участников больше указанного в первой строке.");
                        reader.Close();
                        return false;
                    }

                    Participant? participant = ParseParticipant(line);
                    if (participant == null)
                    {
                        PrintError($"Строка '{line}' была пропущена: не соответствует формату или не подходит под заданные ограничения.");
                        currentCount--;
                        continue;
                    }

                    object? value;

                    if (!list.TryGetValue(participant.Score, out value))
                    {
                        list.Add(participant.Score, participant);
                    }
                    else
                    {
                        list.Remove(participant.Score);

                        if (value is Participant)
                            list.Add(participant.Score, 2);
                        else if (value is int)
                            list.Add(participant.Score, ((int)value) + 1);
                    }
                }

                reader.Close();

                if (first)
                    PrintError("Ошибка при чтении исходного файла!\nКоличество участников должно быть явно указано в первой строке.");
                else if (totalCount > currentCount)
                    PrintError("Ошибка при чтении исходного файла!\nНе удалось считать сведения об участниках в указанном количестве.");
                else
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при чтении исходного файла!\n{ex.Message}");
                return false;
            }
        }

        // разбивка строки на части и построение из них объекта типа Participant
        private static Participant? ParseParticipant(string line)
        {
            while (line.Contains("  "))
                line = line.Replace("  ", " ");

            string[] parts = line.Split(' ');
            if (parts.Length != 4)
                return null;

            string firstName = parts[1];
            if (firstName.Length > 15)
                return null;

            string lastName = parts[0];
            if (lastName.Length > 20)
                return null;

            byte grade;
            if (!byte.TryParse(parts[2], out grade) || grade < 7 || grade > 11)
                return null;

            int score;
            if (!int.TryParse(parts[3], out score) || score < 0)
                return null;

            return new Participant(firstName, lastName, score);
        }

        // печать группы музыкальных композиций под заданным заголовком
        private static void PrintCompositionsGroup(HashSet<string> compositions, string header)
        {
            Console.WriteLine($"{header}:");
            foreach (string composition in compositions)
                Console.WriteLine($"- '{composition}'");
        }

        // удаление первого вхождения данного элемента из данного связного списка
        private static bool DeleteFirstOccurrence<T>(LinkedList<T> list, T element)
        {
            LinkedListNode<T>? node = list.First;

            while (node != null)
            {
                LinkedListNode<T>? next = node.Next;

                if (element != null && element.Equals(node.Value))
                {
                    list.Remove(node);
                    return true;
                }

                node = next;
            }

            return false;
        }

        // проверка на наличие дубликатов в данном списке
        private static bool IsListContainsDuplicates<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T element = list[i];
                if (list.LastIndexOf(element) != i)
                {
                    Console.WriteLine($"Элемент '{element}' встречается в списке более 1 раза.");
                    return true;
                }
            }

            return false;
        }

        // фильтрация символов в текстовом файле по условию в задании #4
        private static HashSet<char>? FilterWordCharsInTextFile(HashSet<char> chars, string fileName)
        {
            try
            {
                StreamReader reader = new StreamReader(File.Open(fileName, FileMode.Open));

                HashSet<char> expectedChars = new HashSet<char>(chars);
                HashSet<char> meetOnceChars = new HashSet<char>();

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length == 0)
                        continue;

                    foreach (string word in line.ToLower().Split(' '))
                    {
                        if (word.Length == 0)
                            continue;

                        // получим набор гласных букв в слове
                        HashSet<char> set = new HashSet<char>(word);
                        set.IntersectWith(chars);
                        if (set.Count == 0)
                            continue;

                        // найдём теперь уже дважды встречающиеся буквы
                        var meetTwiceChars = set.Intersect(meetOnceChars);
                        meetOnceChars.ExceptWith(meetTwiceChars);
                        set.ExceptWith(meetTwiceChars);
                        if (set.Count == 0)
                            continue;

                        // зафиксируем факт того, что мы встретили новые буквы
                        var meetNew = expectedChars.Intersect(set);
                        meetOnceChars.UnionWith(meetNew);
                        expectedChars.ExceptWith(meetNew);
                    }
                }

                reader.Close();

                expectedChars.UnionWith(meetOnceChars);
                return expectedChars;
            }
            catch (Exception ex)
            {
                PrintError($"Ошибка при чтении исходного файла!\n{ex.Message}");
                return null;
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

        // запрос ввода перечисления строк для формирования хэш-сета из хэш-сетов со словами
        private static HashSet<HashSet<string>> RequestStringsHashSet2D(string header, string separator)
        {
            HashSet<HashSet<string>> set = new HashSet<HashSet<string>>();
            Console.WriteLine($"--- {header} ---");

            while (true)
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    if (set.Count == 0)
                        continue;
                    else
                        break;
                }

                HashSet<string> childSet = new HashSet<string>();
                foreach (string childItem in line.Split(separator))
                    if (!string.IsNullOrWhiteSpace(childItem))
                        childSet.Add(childItem);

                if (childSet.Count == 0)
                    continue;

                set.Add(childSet);
            }

            return set;
        }

        // запрос ввода перечисления строк для формирования хэш-сета
        private static HashSet<string> RequestStringsHashSet(string header)
        {
            HashSet<string> set = new HashSet<string>();
            Console.WriteLine($"--- {header} ---");

            while (true)
            {
                string? line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    if (set.Count == 0)
                        continue;
                    else
                        break;
                }

                set.Add(line);
            }

            return set;
        }

        // запрос ввода перечисления строк для формирования связного списка
        private static LinkedList<string> RequestStringsLinkedList(string header)
        {
            LinkedList<string> list = new LinkedList<string>();
            Console.WriteLine($"--- {header} ---");

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

            return list;
        }

        // запрос ввода перечисления строк для формирования списка
        private static List<string> RequestStringsList(string header)
        {
            List<string> list = new List<string>();
            Console.WriteLine($"--- {header} ---");

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

            return list;
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

        // запрос ввода строки
        private static string RequestString(string prompt)
        {
            bool success;
            string? value;

            do
            {
                Console.Write(prompt);
                value = Console.ReadLine();
                success = !string.IsNullOrEmpty(value);
                if (!success)
                    PrintError($"Введенные данные имеют неверный формат!\nУкажите непустую строку.");
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

        // форматирование имени файла
        private static string GetFileName(string baseName, int taskNumber)
        {
            return $"task{taskNumber}-{baseName}.txt";
        }
    }
}