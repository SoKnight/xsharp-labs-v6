// �������� �����, ���������� �������� ������.
// �������� ���� ��������� ���������� �������.
// ���������� ������������ ��������� �������.

// ���������� � ������ ���� ����������������� ������� �� ��������, ������� ������ k.

namespace Lab6
{
    internal class Task1
    {
        private static readonly string INPUT_FILE_NAME = "input.bin";
        private static readonly string OUTPUT_FILE_NAME = "output.bin";

        public static void Run()
        {
            // --- ���� ������ ---

            Console.WriteLine("������� 6.");
            Console.WriteLine("������: ���������� � ������ ���� ����������������� ������� �� ��������, ������� ������ k.");
            Console.WriteLine();

            int count = UserInput.RequestPositiveInteger("������� ���������� �������� �����: ");
            int k = UserInput.RequestNonZeroInteger("������� �������� 'k': ");
            Console.WriteLine();

            // --- ��������� ��������� ����� �� ���������� ������� ---

            int[] writtenNumbers = new int[count];
            if (!GenerateRandomNumbersFile(count, 1000, writtenNumbers))
                return;

            Console.WriteLine($"��������������� �����: [{String.Join(", ", writtenNumbers)}]");
            Console.WriteLine();

            // --- ������� ������ ---

            int countOfWritten;
            if (!SolveTask(k, writtenNumbers, out countOfWritten))
                return;

            if (countOfWritten == 0)
            {
                Console.WriteLine($"�����, ������� {k}, �����������.");
            }
            else
            {
                Array.Resize(ref writtenNumbers, countOfWritten);
                Console.WriteLine($"�����, ������� {k}: [{String.Join(", ", writtenNumbers)}]");
            }
        }

        private static bool SolveTask(int k, int[] writtenNumbers, out int countOfWritten)
        {
            countOfWritten = 0;

            try
            {
                BinaryReader reader = new BinaryReader(File.Open(INPUT_FILE_NAME, FileMode.Open));
                BinaryWriter writer = new BinaryWriter(File.Open(OUTPUT_FILE_NAME, FileMode.Create));

                int writtenNumbersIndex = 0;
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();

                    if (number % k == 0)
                    {
                        writer.Write(number);
                        writtenNumbers[writtenNumbersIndex++] = number;
                        countOfWritten++;
                    }
                }

                reader.Close();
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("������ �������� ������� ��� ������� ������ ������!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return false;
            }
        }

        private static bool GenerateRandomNumbersFile(int count, int maxBound, int[] writtenNumbers)
        {
            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(INPUT_FILE_NAME, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                {
                    int number = random.Next(maxBound);
                    writer.Write(number);
                    writtenNumbers[i] = number;
                }

                writer.Flush();
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("������ ��� �������� ��������� �����!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return false;
            }
        }
    }
}