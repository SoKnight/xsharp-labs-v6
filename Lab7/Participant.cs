namespace Lab7
{
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
}
