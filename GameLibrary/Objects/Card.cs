namespace GameLibrary.Objects;

public class Card
{
    public const int MinAttack = 0, MaxAttack = 100, MinHealth = 0, MaxHealth = 100;
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
