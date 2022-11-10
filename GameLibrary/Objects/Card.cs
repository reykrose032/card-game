namespace GameLibrary.Objects;

public class Card
{
    public const int MinAttack = 0, MaxAttack = 50, MinHealth = 0, MaxHealth = 100, MinValue = 50, MaxValue = 100;
    Random random = new Random();
    public string Name { get; set; }
    public int AttackValue { get; set; }
    public int Health { get; set; }
    public Species Species { get; set; }

    public Card(string name, Species species)
    {
        Name = name;
        Species = species;
        AttackValue = ComputeAttack();
        Health = ComputeHealth();
    }

    public void Attack(Card card)
    {
        card.Health -= AttackValue;
    }

    public int ComputeAttack() => random.Next(MinAttack, MaxAttack);

    public int ComputeHealth() => random.Next(MinHealth, MaxHealth);

    public override string ToString()
    {
        return "{ " + $"name: {Name}, species: {Species}, attack: {AttackValue}, health: {Health}" + " }";
    }

}
