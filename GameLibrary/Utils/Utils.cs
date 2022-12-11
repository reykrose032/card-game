using GameLibrary.Objects;
namespace GameApp;

public static class Input
{
    public static bool IsValidInput(ConsoleKeyInfo userInput)
    {
        if (!char.IsDigit(userInput.KeyChar))
        {
            Console.WriteLine("Key is not a digit.");
            return false;
        }
        else
            return true;
    }
    public static bool IsValidInput(int cardIndex, int validRange)
    {
        if (cardIndex >= validRange)
        {
            System.Console.WriteLine("Wrong key! Not a card in there.");
            return false;
        }
        else
            return true;
    }
}
public static class Print
{
    public static void PlayerChoices(List<Card> list)
    {
        foreach (var card in list)//posible abstraccion
        {
            Console.WriteLine($"{list.IndexOf(card)} - {card.Name} // Health: {card.Health} // ATK: {card.AttackValue} ");
        }
    }
    public static void ShowPlayerCards(List<Card> list)
    {
        foreach (var card in list)//posible abstraccion
        {
            Console.WriteLine($"{list.IndexOf(card)} - {card.Name} // Health: {card.Health} // ATK: {card.AttackValue} ");
        }
    }
    public static void PrintPossibleUserActions(List<string> actions)
    {
        foreach (var item in actions)
        {
            System.Console.WriteLine(item);
        }
    }
    public static void Field(Player currentPlayer, Player enemyPlayer, Game game)
    {
        Console.WriteLine("Field:");
        Console.WriteLine();
        Print.ShowPlayerCards(game.Board[currentPlayer]);
        Print.ShowPlayerCards(game.Board[enemyPlayer]);
    }
    public static void Hand(Player currentPlayer)
    {
        Console.Write($"Your Hand:");
        Console.WriteLine();
        Print.ShowPlayerCards(currentPlayer.Hand);
    }
    public static void GameInformation(Player currentPlayer, Player enemyPlayer, Game Game)
    {
        Console.WriteLine();
        Print.Hand(currentPlayer);
        Console.WriteLine();
        Print.Field(currentPlayer, enemyPlayer, Game);
        Console.WriteLine();
    }


}