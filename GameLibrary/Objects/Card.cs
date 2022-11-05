namespace GameLibrary.Objects;

public class Card
{
    public const int MinAttack = 0, MaxAttack = 100, MinHealth = 0, MaxHealth = 100;
    public string Name { get; set; }
    public int AttackValue { get; set; }
    public int Health { get; set; }
    public Species Species { get; set; }

    public Card(string name, int attack, int health, Species species)
    {
        Name = name;
        AttackValue = attack;
        Health = health;
        Species = species;
    }

    public void Attack(Card card)
    {
        card.Health -= AttackValue;
    }
}
