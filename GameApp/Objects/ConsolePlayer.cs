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


            int cardCoordinates = -1;
            if (IsAI)
            {
                cardCoordinates = AI.GetCardToInvoke(this, enemyPlayer, game);
            }
            else
            {
                Console.WriteLine("Choose the Card to Invoke or Press <<Enter>> to Omit:");
                Console.WriteLine();
                Print.Hand(this);
                var userInput = Console.ReadKey();
                if (userInput.Key != ConsoleKey.Enter)
                {
                    cardCoordinates = int.Parse(userInput.KeyChar.ToString());
                }
                else if (userInput.Key == ConsoleKey.Enter)
                {
                    return;
                }
                else
                { Console.WriteLine("please,select a valid input"); continue; }
            }
            Rules.InvokeCard(this, game.Board, cardCoordinates);
            System.Console.WriteLine($"{game.Board[this].Last().Name} was invoked");
            Print.PressEnterToContinue();
            Console.Clear();
        }
    }
    public void Fight(Player enemyPlayer, Game game)
    {
        if (!(this.Energy > 0 && Rules.CanFight(this, enemyPlayer, game)))
        { Console.WriteLine("This Player can't fight right now ."); return; }
        while (this.Energy > 0 && Rules.CanFight(this, enemyPlayer, game))
        {
            Print.GameInformation(this, enemyPlayer, game);

            int cardCoordinates = -1;
            int targetCardCoordinates = -1;
            int effectCoordinates = -1;
            if (IsAI)
            {
                cardCoordinates = AI.GetAttackingCard(this, enemyPlayer, game);
                targetCardCoordinates = AI.GetCardToAttack(this, enemyPlayer, game);
                if (!AI.WantCastEffect(this, enemyPlayer, game))
                {
                    Rules.AttackCard(this, enemyPlayer, cardCoordinates, targetCardCoordinates, game);
                    Console.WriteLine($"{game.Board[this][cardCoordinates].Name} attacked {game.Board[enemyPlayer][targetCardCoordinates].Name} ");

                    Print.PressEnterToContinue();
                    Console.Clear();
                    return;
                }
                else
                {
                    Card ownCard = game.Board[this][cardCoordinates];
                    Card targetCard = game.Board[enemyPlayer][targetCardCoordinates];

                    effectCoordinates = AI.GetEffectToCast(ownCard, targetCard, game);
                    Rules.CastEffect(this, enemyPlayer, cardCoordinates, targetCardCoordinates, game, ownCard.Effects[effectCoordinates]);
                    Console.WriteLine($"{ownCard.Name} casted {ownCard.Effects[effectCoordinates].Name} Effect on {targetCard.Name}");
                    Print.PressEnterToContinue();
                    Console.Clear();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Choose the Card to Fight or Press <<Enter>> to Omit:");
                var userInput = Console.ReadKey(true);
                if (userInput.Key != ConsoleKey.Enter)
                {
                    cardCoordinates = int.Parse(userInput.KeyChar.ToString());
                    Console.WriteLine("Choose your Target:");
                    targetCardCoordinates = int.Parse(Console.ReadLine());
                }
                else if (userInput.Key == ConsoleKey.Enter)
                {
                    return;
                }
            }
            Console.WriteLine(" Press 0 to Attack || Press 1 to Cast an Effect ");
            if (UtilsForConsole.UserAnswer())// devuelve true si el usuario pulso 1,si toco 0 lo contrario
            {//mejorar legibilidad y construccion de RulesCastEffect
                Card ownCard = game.Board[this][cardCoordinates];
                Card targetCard = game.Board[enemyPlayer][targetCardCoordinates];

                Console.WriteLine("You have selected to Cast an Effect,now select the Effect to Cast");
                Print.ShowCardEffects(ownCard);
                var userInput = Console.ReadKey(true);
                effectCoordinates = int.Parse(userInput.KeyChar.ToString());
                Rules.CastEffect(this, enemyPlayer, cardCoordinates, targetCardCoordinates, game, ownCard.Effects[effectCoordinates]);
                Console.WriteLine($"{ownCard.Name} casted {ownCard.Effects[effectCoordinates].Name} Effect on {targetCard.Name}");
            }
            else
            {
                Rules.AttackCard(this, enemyPlayer, cardCoordinates, targetCardCoordinates, game);
                Console.WriteLine($"{game.Board[this][cardCoordinates].Name} attacked {game.Board[enemyPlayer][targetCardCoordinates].Name} ");
            }
            Print.PressEnterToContinue();
            Console.Clear();
        }
    }
}

