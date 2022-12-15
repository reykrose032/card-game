namespace GameLibrary.Objects;

public static class AI
{
    public static int GetCardToInvoke(Player currentPlayer, Player enemyPlayer, Game game)
    {
        int cardWithMaxAtk = SelectingCardWithLargestATK(currentPlayer, enemyPlayer, game);
        int cardWithLargestHealth = SelectingCardWithLargestHealth(currentPlayer, enemyPlayer, game);
        return (cardWithMaxAtk + cardWithLargestHealth) % 2 == 0 ? cardWithMaxAtk : cardWithLargestHealth;
    }

    public static int GetAttackingCard(Player currentPlayer, Player enemyPlayer, Game game) => SelectingCardWithLargestATK(currentPlayer, enemyPlayer, game);

    public static int GetCardToAttack(Player currentPlayer, Player enemyPlayer, Game game) => SelectingCardWithMinorHealth(currentPlayer, enemyPlayer, game);

    public static int SelectingCardWithLargestATK(Player currentPlayer, Player enemyPlayer, Game game)
    {
        int result = 0;
        int largestAtk = 0;
        for (int cardNumber = 0; cardNumber < game.Board[currentPlayer].Count; cardNumber++)
        {
            if (game.Board[currentPlayer][cardNumber].AttackValue > largestAtk)
            {
                largestAtk = game.Board[currentPlayer][cardNumber].AttackValue;
                result = cardNumber;
            }
        }
        return result;
    }

    public static int SelectingCardWithMinorHealth(Player currentPlayer, Player enemyPlayer, Game game)
    {
        int result = 0;
        int minorHealth = 0;
        for (int cardNumber = 0; cardNumber < game.Board[enemyPlayer].Count; cardNumber++)
        {
            if (game.Board[enemyPlayer][cardNumber].HealthValue < minorHealth)
            {
                minorHealth = game.Board[enemyPlayer][cardNumber].HealthValue;
                result = cardNumber;
            }
        }
        return result;
    }
    public static int SelectingCardWithLargestHealth(Player currentPlayer, Player enemyPlayer, Game game)
    {
        int result = 0;
        int largestHealth = 0;
        for (int cardNumber = 0; cardNumber < game.Board[currentPlayer].Count; cardNumber++)
        {
            if (game.Board[currentPlayer][cardNumber].HealthValue > largestHealth)
            {
                largestHealth = game.Board[currentPlayer][cardNumber].HealthValue;
                result = cardNumber;
            }
        }
        return result;
    }
}