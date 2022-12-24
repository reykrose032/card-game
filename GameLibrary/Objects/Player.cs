namespace GameLibrary.Objects;

public class Player
{
    public const int MinEnergy = 0;
    public const int InitialEnergy = 3;
    public string Name { get; }
    public int Number { get; }
    public int Energy { get; private set; }
    public Queue<Card> Deck { get; set; }
    public List<Card> Hand { get; set; }
    public bool IsAI;
    public Player(string name, Queue<Card> deck, int playerNumber, bool IsAI, int energy = InitialEnergy)
    {
        Name = name;
        Energy = energy;
        Deck = deck;
        Hand = new();
        Number = playerNumber;
        this.IsAI = IsAI;
    }

    public void Invoke(Card card, Dictionary<Player, List<Card>> board)
    {
        if (Energy > 0 && Hand.Contains(card) && board[this].Count < Game.BoardSize)
        {
            board[this].Add(card);
            Hand.Remove(card);
        }
    }

    public void Draw()
    {
        if (Deck.Count > 0) Hand.Add(Deck.Dequeue());
    }

    public void IncreaseEnergy() => Energy++;

    public void DecreaseEnergy() => Energy--;
    public void IncreaseEnergy(int x = 1) => Energy += x;

    public void DecreaseEnergy(int x = 1) => Energy -= x;

}