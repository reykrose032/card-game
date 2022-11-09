namespace GameLibrary.Objects;

public class Game
{
    public const int BoardSize = 4;
    public const int MaxNumberOfTurns = 20;
    public const int NumberOfCardsForInitialDraw = 3;
    public int TurnCounter { get; set; }
    public Player Player1;
    public Player Player2;
    public Dictionary<Player, List<Card>> Board;

    public Game(Player player1, Player player2)
    {
        TurnCounter = 0;

        Player1 = player1;
        Player2 = player2;

        Board = new Dictionary<Player, List<Card>>();
        Board.Add(Player1, new List<Card>());
        Board.Add(Player2, new List<Card>());

        InitialDraw();
    }
    private void InitialDraw()
    {
        Player1.Draw(NumberOfCardsForInitialDraw);
        Player2.Draw(NumberOfCardsForInitialDraw);
    }
    public void UpdateBoard()
    {
        foreach (var playerBoard in Board.Values)
        {
            foreach (var card in playerBoard.ToList())
            {
                if (card.Health <= 0)
                {
                    playerBoard.Remove(card);
                }
            }
        }
    }
    public Player IsTurnOf()
    {
        return (TurnCounter % 2 == 0) ? Player1 : Player2;
    }
    public Player IsNotTurnOf()
    {
        return (TurnCounter % 2 == 1) ? Player1 : Player2;
    }

    public void EndTurn()
    {
        TurnCounter++;
    }

    public bool IsEndOfGame()
    {
        if (TurnCounter >= MaxNumberOfTurns)
            return true;

        if (HasLost(Player1) || HasLost(Player2))
            return true;

        return false;
    }

    public bool HasLost(Player player)
        => player.Deck.Count == 0 && player.Hand.Count == 0 && Board[player].Count == 0;

}
