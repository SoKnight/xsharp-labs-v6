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

        public Toy() : this("", 0, null, null) { }

        public Toy(string name, int price) : this(name, price, null, null) { }

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
}