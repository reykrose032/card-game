namespace GameLibrary.Objects;

public class Game
{
    public const int BoardSize = 4;
    public const int MaxNumberOfTurns = 20;
    public int TurnCounter;
    private Player Player1;
    private Player Player2;
    public Dictionary<Player, Card[]> Board;

    public Game(Player player1, Player player2)
    {
        TurnCounter = 0;

        Player1 = player1;
        Player2 = player2;
        
        Board = new Dictionary<Player, Card[]>();
        Board.Add(Player1, new Card[BoardSize]);
        Board.Add(Player2, new Card[BoardSize]);
    }

    // public void Play()
    // {
    //     do
    //     {
    //         PlayTurn();
    //     } while (!IsEndOfGame());
    // }

    // public void PlayTurn()
    // {

    // }

    public bool IsEndOfGame()
    {
        if (TurnCounter >= MaxNumberOfTurns)
            return true;

        if (HasLost(Player1) || HasLost(Player2))
            return true;

        return false;
    }

    public bool HasLost(Player player)
        => Player1.Deck.Count == 0 && Player1.Hand.Count == 0 && Board[Player1].Count(elem => elem == null) == BoardSize;

}
