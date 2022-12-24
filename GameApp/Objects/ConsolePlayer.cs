using GameLibrary.Objects;
using GameApp;

public class ConsolePlayer : Player
{
    public ConsolePlayer(Player player) : base(player.Name, player.Deck, player.Number, player.IsAI) { }

    public void PlayInvocationPhase(Player enemyPlayer, Game game)
    {
        while (this.Energy > 0 && Rules.CanInvoke(this))
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
            System.Console.WriteLine($"{game.Board[this].Last().Name} was invoked");
            Console.ReadKey();
            Console.Clear();
        }
    }
    public void Fight(Player enemyPlayer, Game game)
    {
        while (this.Energy > 0 && Rules.CanFight(this, enemyPlayer, game))
        {

            Console.WriteLine("Choose the Card to Fight or Press <<Enter>> to Omit:");
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
                    Console.WriteLine("Choose your Victim:");
                    targetCardCoordinates = int.Parse(Console.ReadLine());
                }
                else if (userInput.Key == ConsoleKey.Enter)
                {
                    return;
                }
            }
            Console.WriteLine(" Press 0 to Attack || Press 1 to Cast an Effect ");
            if (UtilsForConsole.UserAnswer())// devuelve true si el usuario pulso 1,si toco 0 lo contrario
            {
                Card ownCard = game.Board[this][cardCoordinates];
                ownCard.Effects.Add(new Weaken());
                ownCard.Effects.Add(new Heal());
                Card enemyCard = game.Board[enemyPlayer][targetCardCoordinates];
                Console.WriteLine("You have selected to Cast an Effect,now select the Effect to Cast");
                Print.ShowCardEffects(ownCard);
                var userInput = Console.ReadKey(true);
                int effectCoordinates = int.Parse(userInput.KeyChar.ToString());
                Rules.CastEffect(this, enemyPlayer, cardCoordinates, targetCardCoordinates, game, ownCard.Effects[effectCoordinates]);
                Console.WriteLine($"{ownCard.Name} casted {ownCard.Effects[effectCoordinates].Name} Effect on {enemyCard.Name}");
            }
            else
            {
                Rules.AttackCard(this, enemyPlayer, cardCoordinates, targetCardCoordinates, game);
                Console.WriteLine($"{game.Board[this][cardCoordinates].Name} attacked {game.Board[enemyPlayer][targetCardCoordinates].Name} ");
            }
            Console.ReadKey();
            game.UpdateBoard();
            Console.Clear();

        }
    }
}

