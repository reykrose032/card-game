namespace GameLibrary.Objects;

public class Player
{
    public const int MinEnergy = 0;
    public const int InitialEnergy = 3;
    public string Name;
    public int Energy;
    public Queue<Card> Deck;
    public List<Card> Hand;

    public Player(string name, Queue<Card> deck)
    {
        Name = name;
        Energy = InitialEnergy;
        Deck = deck;
        Hand = new();
    }
}