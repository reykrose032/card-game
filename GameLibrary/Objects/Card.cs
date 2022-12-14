namespace GameLibrary.Objects;

public class Card
{
    public const int MinAttack = 0, MaxAttack = 50, MinHealth = 0, MaxHealth = 100;

    public string Name { get; set; }
    public int AttackValue { get; set; }
    public int MaxAttackValue { get; set; }
    public int Health { get; set; }
    public int MaxHealthValue { get; set; }

    public Species Specie { get; set; }

    public Player player;
    Random random = new Random();

    public Card(string name, int attackValue, int health, Species specie, Player player)
    {
        Name = name;
        AttackValue = attackValue;
        Health = health;
        Specie = specie;
        this.player = player;
    }
    public Card(string name, Species species)
    {
        Name = name;
        Specie = species;
        AttackValue = ComputeAttack();
        Health = ComputeHealth();
    }


    public void Attack(Card card)
    {
        card.Health -= AttackValue;
    }

    int ComputeAttack() => random.Next(MinAttack, MaxAttack);

    int ComputeHealth() => random.Next(MinHealth, MaxHealth);
}
