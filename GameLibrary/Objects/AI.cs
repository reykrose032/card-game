namespace GameLibrary.Objects;

public static class AI
{
    public static int GetCardToInvoke(Player currentPlayer, Player enemyPlayer, Game game)
    {
        int cardWithMaxAtk = SelectingCardWithLargestATK(currentPlayer, currentPlayer.Hand);
        int cardWithLargestHealth = SelectingOwnCardWithLargestHealth(currentPlayer, currentPlayer.Hand);
        return (cardWithMaxAtk + cardWithLargestHealth) % 2 == 0 ? cardWithMaxAtk : cardWithLargestHealth;
    }

    public static int GetAttackingCard(Player currentPlayer, Player enemyPlayer, Game game) => SelectingCardWithLargestATK(currentPlayer, game.Board[currentPlayer]);

    public static int GetCardToAttack(Player currentPlayer, Player enemyPlayer, Game game) => SelectingCardWithMinorHealth(currentPlayer, enemyPlayer, game.Board[enemyPlayer]);

    public static int SelectingCardWithLargestATK(Player currentPlayer, List<Card> cardList)
    {
        int result = 0;
        int largestAtk = 0;
        for (int cardNumber = 0; cardNumber < cardList.Count; cardNumber++)
        {
            if (cardList[cardNumber].AttackValue > largestAtk)
            {
                largestAtk = cardList[cardNumber].AttackValue;
                result = cardNumber;
            }
        }
        return result;
    }

    public static int SelectingCardWithMinorHealth(Player currentPlayer, Player enemyPlayer, List<Card> cardList)
    {
        int result = 0;
        int minorHealth = 0;
        for (int cardNumber = 0; cardNumber < cardList.Count; cardNumber++)
        {
            if (cardList[cardNumber].HealthValue < minorHealth)
            {
                minorHealth = cardList[cardNumber].HealthValue;
                result = cardNumber;
            }
        }
        return result;
    }
    public static int SelectingOwnCardWithLargestHealth(Player currentPlayer, List<Card> cardList)
    {
        int result = 0;
        int largestHealth = 0;
        for (int cardNumber = 0; cardNumber < cardList.Count; cardNumber++)
        {
            if (cardList[cardNumber].HealthValue > largestHealth)
            {
                largestHealth = cardList[cardNumber].HealthValue;
                result = cardNumber;
            }
        }
        return result;
    }

    public static int GetEffectToCast(Card ownCard, Card targetCard, Game game)
    {
        //mejorar mas adelante
        Random random = new Random();
        return random.Next(ownCard.Effects.Count);
    }
    public static bool WantCastEffect(Player currentPlayer, Player enemyPlayer, Game game)
    {//mejorar mas adelante
        Random random = new Random();
        return random.Next(2) == 1;
    }

}
