// Решить задачу с использованием структуры «текстовый файл».
// В файле хранится текст.

// Переписать в другой файл строки, в которых нет русских букв.

using System.Diagnostics;

namespace Lab6
{
    internal class Task6
    {
        private static readonly string INPUT_FILE_NAME = "task6_input.txt";
        private static readonly string OUTPUT_FILE_NAME = "task6_output.txt";

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача:");
            Console.WriteLine("Переписать в другой файл строки, в которых нет русских букв.");
            Console.WriteLine();

            // --- заполнение исходного файла вручную ---

            Console.WriteLine("Введите входные данные в исходном файле...");
            Console.WriteLine();

            if (!UserInput.AwaitManualInput(INPUT_FILE_NAME))
                return;

            // --- решение задачи ---

            if (!SolveTask())
                return;

            try
            {
                Process process = Process.Start("notepad.exe", OUTPUT_FILE_NAME);
                Console.WriteLine("Готово!");
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при попытке открыть файл с результатами!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
            }
        }

        private static bool SolveTask()
        {
            try
            {
                StreamReader reader = new StreamReader(File.Open(INPUT_FILE_NAME, FileMode.Open));
                StreamWriter writer = new StreamWriter(File.Open(OUTPUT_FILE_NAME, FileMode.Create));

                string? line;
                bool linePassed;

                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;

                    linePassed = true;
                    foreach (char ch in line)
                    {
                        if (ch >= 'А' && ch <= 'Я' || ch >= 'а' && ch <= 'я')
                        {
                            linePassed = false;
                        }
                    }
                    
                    if (linePassed)
                    {
                        Console.WriteLine($"Строка '{line}', не содержит русских букв...");
                        writer.WriteLine(line);
                    }
                    else
                    {
                        Console.WriteLine($"Строка '{line}' пропущена...");
                    }
                }

                reader.Close();
                writer.Close();

                Console.WriteLine();
                Console.WriteLine("Файл с результатами сохранён по пути:\n" + Path.GetFullPath(OUTPUT_FILE_NAME));
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при перезаписи строк между файлами!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return false;
            }
        }

    }
}

