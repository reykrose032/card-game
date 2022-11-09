using GameLibrary.Objects;

namespace Utils;
public static class Print
{
    public static void PlayerCards(List<Card> list)
    {
        foreach (var card in list)
        {
            Console.Write($"{list.IndexOf(card)} - {card.Name}, ");
        }

        System.Console.WriteLine();
    }

}