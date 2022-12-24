namespace GameLibrary.Objects;

public class Card
{
    public const int MinAttack = 0, MaxAttack = 50, MinHealth = 0, MaxHealth = 100;

    public string Name { get; set; }
    public int AttackValue { get; set; }
    public int MaxAttackValue { get; set; }
    public int HealthValue { get; set; }
    public int MaxHealthValue { get; set; }
    public Species Specie { get; set; }
    public List<IEffect> Effects = new List<IEffect>();

    public Card(string name, int attackValue, int health, Species specie)
    {
        Name = name;
        AttackValue = attackValue <= MaxAttack && attackValue >= MinAttack ? attackValue : throw new ArgumentException();
        MaxAttackValue = attackValue;
        HealthValue = health;
        MaxHealthValue = HealthValue <= MaxHealth && HealthValue >= MinHealth ? HealthValue : throw new ArgumentException();
        Specie = specie;
    }
    public Card(string name, Species species)
    {
        Name = name;
        Specie = species;
        AttackValue = RandomStuff.ComputeAttack();
        HealthValue = RandomStuff.ComputeHealth();
        MaxAttackValue = AttackValue;
        MaxHealthValue = HealthValue;
    }

    public void Attack(Card card) => card.HealthValue -= AttackValue;

}
