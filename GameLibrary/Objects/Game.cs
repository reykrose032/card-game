namespace GameLibrary.Objects;

public class Game
{
    public const int BoardSize = 4;
    public const int MaxNumberOfTurns = 20;
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
    /* public Player IsTurnOf()
     {
         return (TurnCounter % 2 == 0) ? Player1 : Player2;
     }
     public Player IsNotTurnOf()
     {
         return (TurnCounter % 2 == 1) ? Player1 : Player2;
     }
     */
    public static Player[] GetPlayerTurnOrder(Player own, Player opponet, int TurnCounter)
    {
        bool[] mask = new bool[2];
        if (TurnCounter % 2 == 0) mask[0] = true;
        else mask[1] = true;
        Player[] players = new Player[2];
        if (mask[0])
        {
            players[0] = own;
            players[1] = opponet;
        }
        else
        {
            players[0] = opponet;
            players[1] = own;
        }
        return players;
    }

    public static bool Draw(int TurnCounter, int count, Player one, Player two)
    {
        if (TurnCounter % count == 0)
        {
            one.Draw();
            two.Draw();
            return true;
        }
        return false;
    }

    public void EndTurn()
    {
        TurnCounter++;
    }
}


