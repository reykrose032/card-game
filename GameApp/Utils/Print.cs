using GameLibrary.Objects;

namespace Utils;
public static class Print
{
    public static void PlayerCards(List<Card> list)
    {
        System.Console.WriteLine();
        foreach (var card in list)
        {
            Console.WriteLine($"{list.IndexOf(card)} - {card.ToString()}, ");
        }
        System.Console.WriteLine();
    }

    public static void Board(Dictionary<Player, List<Card>> board)
    {
        foreach (var player in board.Keys)
        {
            PlayerCards(board[player]);
        }
    }
}