namespace Cards
{
    public struct Card
    {
        public string name;
        public int health { get; set; }
        public string type;
        public int ATK;
        public Card(string name, int health, string type, int ATK)
        {
            this.name = name;
            this.health = health;
            this.type = type;
            this.ATK = ATK;
        }
    }
}