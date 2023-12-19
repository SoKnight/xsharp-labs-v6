namespace Lab7
{
    // компаратор, используемый в задании #5
    public class ReversedIntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y.CompareTo(x);
        }
    }
}
