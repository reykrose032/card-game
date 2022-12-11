using GameLibrary;
namespace GameLibrary.Objects;
public class Start
{
    static Card GenerateCard()
    {
        var species = Enum.GetValues(typeof(Species));
        var random = new Random();
        var cardSpecies = (Species)species.GetValue(random.Next(species.Length));
        var card = new Card(cardSpecies.ToString(), cardSpecies);
        return card;
    }

    public static Queue<Card> GenerateDeck(int n)
    {
        var cards = new Queue<Card>();
        for (int i = 0; i < n; i++)
        {
            cards.Enqueue(GenerateCard());
        }
        return cards;
    }
}