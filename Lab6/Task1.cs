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
            Console.WriteLine("������� 6.");
            Console.WriteLine("������: ���������� � ������ ���� ����������������� ������� �� ��������, ������� ������ k.");
            Console.WriteLine();

            int count = InputData.RequestPositiveInteger("������� ���������� �������� �����: ");
            int k = InputData.RequestNonZeroInteger("������� �������� 'k': ");

            if (!GenerateRandomNumbersFile(count, 1000))
                return;

            if (!SolveTask(k))
                return;

            Console.WriteLine();
            Console.WriteLine("�������!");
        }

        private static bool SolveTask(int k)
        {
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(INPUT_FILE_NAME, FileMode.Open));
                BinaryWriter writer = new BinaryWriter(File.Open(OUTPUT_FILE_NAME, FileMode.Create));

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();

                    if (number % k == 0)
                    {
                        Console.WriteLine(number);
                        writer.Write(number);
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

        private static bool GenerateRandomNumbersFile(int count, int maxBound)
        {
            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(INPUT_FILE_NAME, FileMode.Create));
                Random random = new Random();

                for (int i = 0; i < count; i++)
                    writer.Write(random.Next(maxBound));

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