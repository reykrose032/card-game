namespace GameLibrary.Objects;

public class PlayerIA : Player
{
    public PlayerIA(Queue<Card> deck) : base("IA", deck) { }

    public int GetCardToInvoke(Player currentPlayer, Player enemyPlayer, Game game)
    {
        int cardWithMaxAtk = SelectingCardWithLargestATK(currentPlayer, enemyPlayer, game);
        int cardWithLargestHealth = SelectingCardWithLargestHealth(currentPlayer, enemyPlayer, game);
        return (cardWithMaxAtk + cardWithLargestHealth) % 2 == 0 ? cardWithMaxAtk : cardWithLargestHealth;
    }

    public int GetAttackingCard(Player currentPlayer, Player enemyPlayer, Game game) => SelectingCardWithLargestATK(currentPlayer, enemyPlayer, game);

    public int GetCardToAttack(Player currentPlayer, Player enemyPlayer, Game game) => SelectingCardWithMinorHealth(currentPlayer, enemyPlayer, game);

    public int SelectingCardWithLargestATK(Player currentPlayer, Player enemyPlayer, Game game)
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

    public int SelectingCardWithMinorHealth(Player currentPlayer, Player enemyPlayer, Game game)
    {
        int result = 0;
        int minorHealth = 0;
        for (int cardNumber = 0; cardNumber < game.Board[enemyPlayer].Count; cardNumber++)
        {
            if (game.Board[enemyPlayer][cardNumber].Health < minorHealth)
            {
                minorHealth = game.Board[enemyPlayer][cardNumber].Health;
                result = cardNumber;
            }
        }
        return result;
    }
    public int SelectingCardWithLargestHealth(Player currentPlayer, Player enemyPlayer, Game game)
    {
        int result = 0;
        int largestHealth = 0;
        for (int cardNumber = 0; cardNumber < game.Board[currentPlayer].Count; cardNumber++)
        {
            if (game.Board[currentPlayer][cardNumber].Health > largestHealth)
            {
                largestHealth = game.Board[currentPlayer][cardNumber].Health;
                result = cardNumber;
            }
        }
        return result;
    }
}


