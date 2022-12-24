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
        if (DefaultEffects.Count == 0)
            FillDefaultEffectList();
        for (int i = random.Next(DefaultEffects.Count); i < DefaultEffects.Count; i++)
        {
            IEffect temp = DefaultEffects[random.Next(DefaultEffects.Count)];
            if (!card.Effects.Contains(temp))
                card.Effects.Add(temp);
        }

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

    private static List<IEffect> DefaultEffects = new List<IEffect>();
    private static void FillDefaultEffectList()
    {
        DefaultEffects.Add(new Weaken());
        DefaultEffects.Add(new Heal());
    }

}