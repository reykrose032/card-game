using GameLibrary.Objects;
namespace GameApp;

public static class UtilsForConsole
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
    public static bool IsInRangeInput(int cardIndex, int validRange)
    {
        if (cardIndex >= validRange)
        {
            System.Console.WriteLine("Wrong key! Not a card in there.");
            return false;
        }
        else
            return true;
    }

    public static bool UserAnswer()
    {
        while (true)
        {
            string answer = Console.ReadLine();

            if (answer == "1")
                return true;
            else if (answer == "0")
                return false;
            else
                Console.WriteLine("please,select a valid option");
        }
    }
}
public static class Print
{
    public static void PressEnterToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadKey();
    }
    public static void ShowPlayerEnergy(Player currentPlayer)
    {
        Console.WriteLine($"{currentPlayer.Name} Energy: {currentPlayer.Energy}");
    }
    public static void ShowPlayerCards(List<Card> list)
    {
        foreach (var card in list)//posible abstraccion
        {
            Console.Write($"{list.IndexOf(card)} - {card.Name} // Health: {card.HealthValue}/{card.MaxHealthValue} // ATK: {card.AttackValue}/{card.MaxAttackValue} ||");
            foreach (IEffect effect in card.Effects)
            {
                Console.Write($" {effect.Name} Effect //");
            }
            Console.WriteLine();
        }
    }
    public static void ShowCardEffects(Card card)
    {
        foreach (IEffect effect in card.Effects)
        {
            Console.WriteLine($"{card.Effects.IndexOf(effect)} - {effect.Name} ");//anadir una descripcion de lo q hace el effecto o algo asi
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
        Console.WriteLine($"{enemyPlayer.Name}'s Cards:");
        Console.WriteLine();
        Print.ShowPlayerCards(game.Board[enemyPlayer]);
        Console.WriteLine("--------------------------");
        Print.ShowPlayerCards(game.Board[currentPlayer]);
        Console.WriteLine();
        Console.WriteLine($"{currentPlayer.Name}'s Cards:");
        Console.WriteLine();
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
        Print.ShowPlayerEnergy(currentPlayer);
        Console.WriteLine();
        Print.Hand(currentPlayer);
        Console.WriteLine();
        Print.Field(currentPlayer, enemyPlayer, Game);
        Console.WriteLine();
    }


}