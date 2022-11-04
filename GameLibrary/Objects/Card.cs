namespace GameLibrary.Objects;

public class Card
{
    public const int MaxAttack = 100;
    public const int MinAttack = 0;
    public const int MaxHealth = 100;
    public const int MinHealth = 0;
    public string Name;
    public int Attack;
    public int Health;
    public Species Species;

    public Card(string name, int attack, int health, Species species)
    {
        Name = name;
        Attack = attack;
        Health = health;
        Species = species;
    }
}
