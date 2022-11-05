namespace GameLibrary.Objects;

public class Game
{
    public const int BoardSize = 4;
    public const int MaxNumberOfTurns = 20;
    public int TurnCounter { get; set; }
    private Player Player1;
    private Player Player2;
    public Dictionary<Player, List<Card>> Board;

    public Game(Player player1, Player player2)
    {
        TurnCounter = 0;

        Player1 = player1;
        Player2 = player2;

        Board = new Dictionary<Player, List<Card>>();
        Board.Add(Player1, new List<Card>());
        Board.Add(Player2, new List<Card>());
    }

    public bool IsTurnOf(Player player) => (TurnCounter % 2 == 1) ? player == Player2 : player == Player1;
    public void PlayTurn(Player player)
    {
        if (IsTurnOf(player))
        {
            while (true)
            {

            }
        }
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
        => player.Deck.Count == 0 && player.Hand.Count == 0 && Board[player].Count() == 0;

}
