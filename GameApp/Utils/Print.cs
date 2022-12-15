using GameLibrary.Objects;
using Spectre.Console;
using Spectre;

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
        var info = $"Atk: {card.AttackValue}\nHealth: {card.Health}\nSpecies: {card.Species.ToString()}";

        var panelCard = new Panel(info);

        panelCard.Header = new PanelHeader(card.Name);
        panelCard.Border = BoxBorder.Square;
        panelCard.Padding = new Padding(2, 1);
        panelCard.Collapse();

        return panelCard;
    }

    public static List<Panel> PlayerCards(List<Card> list)
    {
        var panelCards = new List<Panel>();
        foreach (var card in list)
        {
            panelCards.Add(Card(card));
        }

        return panelCards;
    }

    public static void Board(Dictionary<Player, List<Card>> board)
    {
        var table = new Table().Centered();
        table.Width(120);
        table.AddColumns("", "", "", "");
        table.HorizontalBorder();
        table.Expand();
        table.HideHeaders();

        foreach (var column in table.Columns)
        {
            column.Width(15);
        }
        foreach (var player in board.Keys)
        {
            table.AddRow(PlayerCards(board[player]));
        }

        AnsiConsole.Write(table);
    }

    public static void PlayerDeck(List<Card> playerDeck)
    {
        var playerCards = PlayerCards(playerDeck);
        var columns = new Columns(playerCards);

        AnsiConsole.Write(columns);
    }
}