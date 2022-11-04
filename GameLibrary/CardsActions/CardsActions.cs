using Cards;


public static class CardsActions
{
    public static void Attack(Card cardA, Card cardB)//A ataca a B
    {
        cardA.health -= cardB.ATK;
    }
    public static void InvokeCard(Card card, List<Card> field)
    {
        field.Add(card);
    }
}

