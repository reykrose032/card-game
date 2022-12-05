using GameLibrary.Objects;
namespace Utils
{
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


    }
}