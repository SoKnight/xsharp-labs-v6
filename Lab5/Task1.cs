// Разработать класс с тремя целочисленными полями.
// Создать конструктор копирования.
// Разработать метод, вычисляющий минимальную из последних цифр полей.
// Перегрузить метод ToString() для формирования строки из полей класса.
// Реализовать дочерний класс (его содержание предложить самостоятельно и описать в решении: какой содержательный смысл имеют поля; реализовать конструкторы; предложить и реализовать 2-3 метода).
// Протестировать все конструкторы и другие методы базового и дочернего классов.

namespace Lab5
{
    // класс, описывающий общую игровую статистику
    public class Statistics
    {
        // три целочисленных поля
        protected int playTime;
        protected int kills;
        protected int deaths;

        // общий конструктор
        public Statistics(int playTime, int kills, int deaths)
        {
            this.playTime = playTime;
            this.kills = kills;
            this.deaths = deaths;
        }

        // конструктор копирования
        public Statistics(Statistics instance)
        {
            this.playTime = instance.playTime;
            this.kills = instance.kills;
            this.deaths = instance.deaths;
        }

        // метод, вычисляющий минимальную из последних цифр
        public int ComputeMinLastDigit()
        {
            int playTimeLastDigit = playTime % 10;
            int killsLastDigit = kills % 10;
            int deathsLastDigit = deaths % 10;

            if (playTimeLastDigit <= killsLastDigit && playTimeLastDigit <= deathsLastDigit)
                return playTimeLastDigit;
            else if (killsLastDigit <= playTimeLastDigit && killsLastDigit <= deathsLastDigit)
                return killsLastDigit;
            else
                return deathsLastDigit;
        }

        // перегрузка #ToString()
        public override string ToString()
        {
            return "{playTime=" + playTime
                + ", kills=" + kills
                + ", deaths=" + deaths
                + "}";
        }
    }

    // дочерний класс, описывающий игровую статистику конкретной сессии
    public class SessionStatistics : Statistics
    {
        private DateTime sessionStartTime;
        private DateTime sessionEndTime;

        // общий конструктор, перегружающий родительский и устанавливающий время начала игровой сессии
        public SessionStatistics() : base(0, 0, 0)
        {
            this.sessionStartTime = DateTime.Now;
            this.sessionEndTime = DateTime.MinValue;
        }

        // метод для проверки, закрыта ли сессия
        public bool IsSessionClosed()
        {
            return sessionEndTime != DateTime.MinValue;
        }

        // метод для закрытия игровой сессии
        public void CloseSession()
        {
            this.sessionEndTime = DateTime.Now;
            base.playTime = (int) (sessionEndTime - sessionStartTime).TotalSeconds;
        }

        // метод для увеличения счётчика убийств
        public void IncreaseKillsCounter()
        {
            base.kills++;
        }

        // метод для увеличения счётчика смертей
        public void IncreaseDeathsCounter()
        {
            base.deaths++;
        }

        // перегрузка #ToString()
        public override string ToString()
        {
            return "{playTime=" + playTime
                + ", kills=" + kills
                + ", deaths=" + deaths
                + ", sessionStartTime=" + sessionStartTime
                + (IsSessionClosed() ? (", sessionEndTime=" + sessionEndTime) : "")
                + "}";
        }
    }

    internal class Task1
    {
        public static void Run()
        {
            // --- создание объектов статистики двумя способами

            Statistics stats1 = new Statistics(0, 0, 0);
            Console.WriteLine("Пустая статистика: " + stats1);

            Statistics stats2 = new Statistics(1, 2, 3);
            Console.WriteLine("Непустая статистика: " + stats2);

            Statistics copyOfStats2 = new Statistics(stats2);
            Console.WriteLine("Копия непустой статистики: " + copyOfStats2);

            Console.WriteLine();

            // --- минимальная из последних цифр

            Console.WriteLine("МПЦ для статистики '" + stats1 + "': " + stats1.ComputeMinLastDigit());
            Console.WriteLine("МПЦ для статистики '" + stats2 + "': " + stats2.ComputeMinLastDigit());

            Statistics stats3 = new Statistics(22938, 34837, 1093);
            Console.WriteLine("МПЦ для статистики '" + stats3 + "': " + stats3.ComputeMinLastDigit());

            Statistics stats4 = new Statistics(2323, 12, 23);
            Console.WriteLine("МПЦ для статистики '" + stats4 + "': " + stats4.ComputeMinLastDigit());

            Statistics stats5 = new Statistics(87362, 32, 196);
            Console.WriteLine("МПЦ для статистики '" + stats5 + "': " + stats5.ComputeMinLastDigit());

            Console.WriteLine();

            // --- создание объектов статистики сессии

            SessionStatistics sessionStats = new SessionStatistics();
            Console.WriteLine("Статистика сессии: " + sessionStats);
            Console.WriteLine("Закрыта ли сессия: " + (sessionStats.IsSessionClosed() ? "Да" : "Нет"));

            Console.WriteLine();

            // --- проверка методов работы со статистикой сессии

            sessionStats.IncreaseKillsCounter();
            Console.WriteLine("После увеличения счётчика убийств: " + sessionStats);

            sessionStats.IncreaseDeathsCounter();
            Console.WriteLine("После увеличения счётчика смертей: " + sessionStats);

            sessionStats.CloseSession();
            Console.WriteLine("После закрытия: " + sessionStats);
            Console.WriteLine("Закрыта ли сессия: " + (sessionStats.IsSessionClosed() ? "Да" : "Нет"));
        }
    }
}