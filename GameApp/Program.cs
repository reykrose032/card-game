using GameLibrary;
using GameLibrary.Objects;
using MiniCompiler;
using Spectre.Console;


namespace GameApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("//////Welcome//////");
        Print.PressEnterToContinue();

        Console.WriteLine("Want to play versus AI ?(press: 1 = true,0 = false)");
        bool wantIA = UtilsForConsole.UserAnswer() ? true : false;

        Console.WriteLine("Creating Decks...");
        var deck1 = Start.GenerateDeck(4);
        var deck2 = Start.GenerateDeck(4);

        Console.WriteLine("Want to create your own Card/Cards?(press: 1 = true ,0 = false");
        bool wantCreateCard = UtilsForConsole.UserAnswer() ? true : false;

        Player player1 = new ConsolePlayer(new Player("Franco", deck1, 1, false));
        Player player2 = new ConsolePlayer(new Player("Magela", deck2, 2, false));

        //entradas de usuario de prueba 
        string CardAndEffect = "Name: Franco Hernandez ; InitialATK: 10 ; InitialHealth: 20 ; InitialSpecie: Angel ; ";
        string Card = "Name: Magela Cornelio ; InitialATK: 2 ; InitialHealth: 50 ; InitialSpecie: Angel ;";
        string Effect = "EffectName: Personalized Effect ; IF: ownCard.Health ownCard.MaxHealth < ; ownCard.MaxHealth = 5 4 + 1 - ; ownCard.AttackValue = ownCard.MaxAttackValue ;";

        // adding cards to a list to test the interpreter
        List<Card> listOfCardExperiment = new List<Card>();
        if (wantCreateCard)
        {
            Interpreter interpreter = new Interpreter();

            Console.WriteLine("Select the amount of Cards to create");
            int amountOfCards = 1;
            for (int i = 0; i < amountOfCards; i++)
            {
                Console.WriteLine($"Write the code of the Card Number {i + 1} :");
                interpreter.EatUserCode(Card);
                listOfCardExperiment.Add(interpreter.BuildCard());
                interpreter.BuildEffect(listOfCardExperiment[i]);
                Console.WriteLine("Card Created !!");
                Console.WriteLine();
                Console.WriteLine("Want to add more effects to this Card? (press 1 = true, 0 = false)");
                if (UtilsForConsole.UserAnswer())
                {
                    Console.WriteLine("Write the number of Effets to be added");
                    int amountOfAditionalEffects = 1;
                    for (int j = 1; j <= amountOfAditionalEffects; j++)
                    {
                        Console.WriteLine($"Write the code of the Effect number {j + 1} :");
                        interpreter.EatUserCode(Effect);
                        interpreter.BuildEffect(listOfCardExperiment[i]);
                        Console.WriteLine("Effect Added !!");
                    }
                }
            }
        }

        if (wantIA)
            player2 = new ConsolePlayer(new Player("IA", deck2, 2, true));
        else
            player2.Name = "Magela";

        var game = new Game(player1, player2);

        Console.WriteLine("//////////START////////////");
        int drawEvery = 2;
        while (!Rules.IsEndOfGame(player1, player2, game))
        {
            Player currentPlayer = Game.GetPlayerTurnOrder(player1, player2, game.TurnCounter)[0];
            Player enemyPlayer = Game.GetPlayerTurnOrder(player1, player2, game.TurnCounter)[1];

            if (Game.Draw(game.TurnCounter, drawEvery, player1, player2))
            {
                System.Console.WriteLine("//// Draw Phase ////");
                System.Console.WriteLine();
                System.Console.WriteLine("Player 1 has Draw.");
                System.Console.WriteLine("");
                System.Console.WriteLine("Player 2 has Draw.");
                System.Console.WriteLine();
                System.Console.ReadKey();
                System.Console.Clear();
            }

            System.Console.WriteLine($"Is {currentPlayer.Name}'s turn:");
            System.Console.WriteLine();

            ConsolePlayer playingPlayer = (ConsolePlayer)currentPlayer;

            Print.GameInformation(currentPlayer, enemyPlayer, game);
            System.Console.ReadKey();
            System.Console.Clear();

            System.Console.WriteLine("//// Invocation Phase ////"); playingPlayer.PlayInvocationPhase(enemyPlayer, game);
            System.Console.WriteLine("//// Fight Phase ////"); playingPlayer.Fight(enemyPlayer, game);
            System.Console.WriteLine();
            System.Console.WriteLine("////// End Turn //////");
            System.Console.WriteLine();

            Console.Clear();
            game.EndTurn();
        }
    }


}
