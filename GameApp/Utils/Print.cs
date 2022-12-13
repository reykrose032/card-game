using GameLibrary.Objects;
using Spectre.Console;

namespace Utils;
public static class Print
{
    public static void PromptUserError(string error)
    {
        AnsiConsole.Markup($"\n[red]{error}[/]\nPress any key to continue...");
        Console.ReadKey(true);
    }

    public static Panel Card(Card card)
    {
        var info = $"Atk: {card.AttackValue}\nHealth: {card.Health}";
        // var info = $"{Emoji.Known.Dagger}: {card.AttackValue}\n{Emoji.Known.RedHeart}: {card.Health}";
        // var info = Emoji.Known.Dagger + ": " + card.AttackValue + "\n" + Emoji.Known.RedHeart + ": " + card.Health;
        var panelCard = new Panel(info);
        panelCard.Header = new PanelHeader(card.Name);
        panelCard.Border = BoxBorder.Square;
        panelCard.Padding = new Padding(2, 1);

        return panelCard;
    }

    public static void PlayerCards(List<Card> list)
    {
        var panelCards = new List<Panel>();
        
        System.Console.WriteLine();
        foreach (var card in list)
        {
            panelCards.Add(Card(card));
            // Console.WriteLine($"{list.IndexOf(card)} - {card.ToString()}, ");
        }
        var columns = new Columns(panelCards);
        AnsiConsole.Write(columns);
        
        System.Console.WriteLine();
    }

    public static void Board(Dictionary<Player, List<Card>> board)
    {
        System.Console.WriteLine("Board: ");
        foreach (var player in board.Keys)
        {
            System.Console.WriteLine($"{player.Name}'s: ");
            PlayerCards(board[player]);
        }
    }
}