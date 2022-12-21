using GameLibrary.Objects;
using GameApp;

public class ConsolePlayer : Player
{
    public ConsolePlayer(Player player) : base(player.Name, player.Deck, player.IsAI) { }

    public void PlayInvocationPhase(Player enemyPlayer, Game game)
    {
        if (Rules.CanInvoke(this))
        {
            Print.GameInformation(this, enemyPlayer, game);
            Console.WriteLine("Choose the Card to Invoke or Press <<Enter>> to Omit:");
            Console.WriteLine();
            Print.Hand(this);

            int cardCoordinates = -1;
            if (IsAI)
            {
                cardCoordinates = AI.GetCardToInvoke(this, enemyPlayer, game);
            }
            else
            {
                var userInput = Console.ReadKey(true);
                if (userInput.Key != ConsoleKey.Enter)
                {
                    cardCoordinates = int.Parse(userInput.KeyChar.ToString());
                }
            }
            Rules.InvokeCard(this, game.Board, cardCoordinates);
            System.Console.WriteLine($"{game.Board[this][cardCoordinates].Name} was invoked");
            Console.ReadKey();
            Console.Clear();
        }
    }
    public void Fight(Player enemyPlayer, Game game)
    {
        if (Rules.CanAttack(this, enemyPlayer, game))//no entiendo el error
        {
            Console.WriteLine("Choose the Attacking Card or Press <<Enter>> to Omit:");
            Print.GameInformation(this, enemyPlayer, game);

            int cardCoordinates = -1;
            int targetCardCoordinates = -1;
            if (IsAI)
            {
                cardCoordinates = AI.GetAttackingCard(this, enemyPlayer, game);
                targetCardCoordinates = AI.GetCardToAttack(this, enemyPlayer, game);
            }
            else
            {
                var userInput = Console.ReadKey(true);
                if (userInput.Key != ConsoleKey.Enter)
                {
                    cardCoordinates = int.Parse(userInput.KeyChar.ToString());
                    Console.WriteLine("Choose a Target:");
                    targetCardCoordinates = int.Parse(Console.ReadLine());
                }
            }

            Rules.AttackCard(this, enemyPlayer, game.Board, cardCoordinates, targetCardCoordinates);
            Console.WriteLine($"{game.Board[this][cardCoordinates].Name} attacked {game.Board[enemyPlayer][targetCardCoordinates].Name} ");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

