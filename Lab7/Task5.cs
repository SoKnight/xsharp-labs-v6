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

using System.Reflection;

namespace Lab7
{
    internal class Task5
    {
        private static readonly string FILE_NAME = "task5_participants.txt";

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача:");
            Console.WriteLine("Решить задачу, используя класс Dictionary (или класс SortedList).");
            Console.WriteLine("Входные данные читать из текстового файла.");
            Console.WriteLine();

            // --- заполнение исходного файла вручную ---

            Console.WriteLine("Формат данных: <фамилия> <имя> <класс> <баллы>");
            Console.WriteLine("Накладываемые ограничения:");
            Console.WriteLine("- <фамилия> - строка до 20 символов");
            Console.WriteLine("- <имя> - строка до 15 символов");
            Console.WriteLine("- <класс> - число от 7 до 11");
            Console.WriteLine("- <баллы> - целое неотрицательное число");
            Console.WriteLine();

            Console.WriteLine("Заполните файл входными данными...");
            Console.WriteLine();

            if (!UserInput.AwaitManualInput(FILE_NAME))
                return;

            // --- решение задачи ---

            SortedList<int, object>? participants;
            int totalCount;
            if (!ReadParticipants(out participants, out totalCount) || participants == null || participants.Count == 0)
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

            WriteError("Вероятно, что-то было введено не по условию задачи :(");
        }

        private static void PrintResult(object result)
        {
            if (result is Participant)
            {
                Participant participant = (Participant) result;
                Console.WriteLine($"Результат: {participant.LastName} {participant.FirstName}");
            }
            else if (result is int)
            {
                Console.WriteLine($"Результат: {result}");
            }
        }

        private static bool IsWinner(int score, object value, int totalCount)
        {
            if (score > 200)
                return true;

            if (value is int && ((double)value) / totalCount <= 0.2)
                return true;

            return false;
        }

        private static bool ReadParticipants(out SortedList<int, object>? list, out int totalCount)
        {
            list = null;
            totalCount = 0;

            try
            {
                StreamReader reader = new StreamReader(File.Open(FILE_NAME, FileMode.Open));

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
                            WriteError("Ошибка при чтении исходного файла!\nКоличество участников должно быть явно указано в первой строке.");
                            reader.Close();
                            return false;
                        }

                        if (totalCount == 0)
                        {
                            WriteError("Ошибка при чтении исходного файла!\nКоличество участников в первой строке не может быть равно 0.");
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
                        WriteError("Ошибка при чтении исходного файла!\nРеальное количество участников больше указанного в первой строке.");
                        reader.Close();
                        return false;
                    }

                    Participant? participant = ParseParticipant(line);
                    if (participant == null)
                    {
                        WriteError($"Строка '{line}' была пропущена: не соответствует формату или не подходит под заданные ограничения.");
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
                    WriteError("Ошибка при чтении исходного файла!\nКоличество участников должно быть явно указано в первой строке.");
                else if (totalCount > currentCount)
                    WriteError("Ошибка при чтении исходного файла!\nНе удалось считать сведения об участниках в указанном количестве.");
                else
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                WriteError($"Ошибка при чтении исходного файла!\n{ex.Message}");
                return false;
            }
        }

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

        private static void WriteError(string message)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = tmp;
        }
    }
}
