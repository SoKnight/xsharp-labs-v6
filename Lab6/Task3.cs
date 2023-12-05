// Бинарные файлы, содержащие величины типа struct.
// Заполнение исходного файла организовать отдельным методом.

// Файл содержит сведения об игрушках: название игрушки, ее стоимость в рублях и возрастные границы.
// Например, игрушка может предназначаться для детей от двух до пяти лет.
// Определить стоимость самого дорогого конструктора (игрушки).

using System.Runtime.Serialization.Formatters.Binary;

namespace Lab6
{
    [Serializable]
    public struct Toy
    {
        private string name;
        private int price;
        private int? minAgeRestriction;
        private int? maxAgeRestriction;

        public string Name { get { return name; } }
        public int Price { get { return price; } }

        public Toy() : this("", 0, null, null) {}

        public Toy(string name, int price) : this(name, price, null, null) {}

        public Toy(string name, int price, int? minAgeRestriction, int? maxAgeRestriction)
        {
            this.name = name;
            this.price = price;
            this.minAgeRestriction = minAgeRestriction;
            this.maxAgeRestriction = maxAgeRestriction;
        }

        public override string ToString()
        {
            return "{name='" + name + '\''
                + ", price=" + price.ToString()
                + (minAgeRestriction != null ? ", minAgeRestriction=" + minAgeRestriction : "")
                + (maxAgeRestriction != null ? ", maxAgeRestriction=" + maxAgeRestriction : "")
                + '}';
        }
    }

    internal class Task3
    {
        private static readonly string FILE_NAME = "toyz.bin";

        public static void Run()
        {
            // --- ввод данных ---

            Console.WriteLine("Вариант 6.");
            Console.WriteLine("Задача:");
            Console.WriteLine("Файл содержит сведения об игрушках: название игрушки, ее стоимость в рублях и возрастные границы.");
            Console.WriteLine("Например, игрушка может предназначаться для детей от двух до пяти лет.");
            Console.WriteLine("Определить стоимость самого дорогого конструктора (игрушки).");
            Console.WriteLine();

            // --- создание исходного файла ---

            Toy[] toyz = new Toy[]
            {
                new Toy("test", 100),
                new Toy("test2", 50, 2, 5)
            };

            Console.WriteLine("Список игрушек:");
            foreach (Toy toy in toyz) Console.WriteLine($"- {toy}");
            Console.WriteLine();

            if (!SaveToyzToFile(toyz))
                return;

            // --- чтение файла со сведениями ---

            Toy[]? loadedToyz = LoadToyzFromFile();
            if (loadedToyz == null)
                return;

            // --- решение задачи ---

            Toy? mostExpensive = FindMostExpensiveToy(loadedToyz);
            if (!mostExpensive.HasValue)
                return;

            Console.WriteLine($"Самая дорогая игрушка: '{mostExpensive.Value.Name}' ({mostExpensive.Value.Price} Р)");
        }

        private static Toy? FindMostExpensiveToy(Toy[] toyz)
        {
            Toy? current = null;

            foreach (Toy toy in toyz)
                if (!current.HasValue || current.Value.Price <= toy.Price)
                    current = toy;

            return current;
        }

        private static Toy[]? LoadToyzFromFile()
        {
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(FILE_NAME, FileMode.Open));
                BinaryFormatter formatter = new BinaryFormatter();
                Toy[]? toyz = formatter.Deserialize(reader.BaseStream) as Toy[];
                reader.Close();
                return toyz;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при чтении файла со сведениями об игрушках!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return null;
            }
        }

        private static bool SaveToyzToFile(Toy[] toyz)
        {
            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(FILE_NAME, FileMode.Create));
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(writer.BaseStream, toyz);
                writer.Flush();
                writer.Close();

                Console.WriteLine("Файл со сведениями об игрушках сохранён по пути:\n" + Path.GetFullPath(FILE_NAME));
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleColor tmp = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при создании файла со сведениями об игрушках!");
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = tmp;
                return false;
            }
        }
    }
}
