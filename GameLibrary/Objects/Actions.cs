using GameLibrary.Objects;
namespace GameActions
{
    public static class Actions
    {
        public static void Draw(Player player, Queue<Card> deck, List<Card> hand)
        {
            if (deck.Count > 0)
            {
                hand.Add(deck.Dequeue());
            }
        }
    }
}