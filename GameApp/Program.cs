using GameLibrary;
using GameLibrary.Objects;
using MiniCompiler;
using GameRules;

namespace GameApp;

class Program
{
    static void Main(string[] args)
    {
        var deck1 = Start.GenerateDeck(4);
        var deck2 = Start.GenerateDeck(4);
        //testing the compiler by adding a new card to the deck1
        string userCodeInput = "Name: Franco Hernandez ; ATK: 10 ; Health: 20 ; Specie: Angel ; ownCard.Health = enemyCard.MaxHealth ;";
        Interpreter interpreter = new Interpreter(userCodeInput);//agregar TokenType.NONE para no tener angel por default

        Player player1 = new ConsolePlayer(new Player("Franco", deck1, false));
        Player player2 = new ConsolePlayer(new Player("Magela", deck2, false));

        bool wantIA = false;//preguntar si quiere jugar vs IA

        if (wantIA)
            player2 = new ConsolePlayer(new Player("IA", deck2, true));
        else
            player2.Name = "Magela";

        var game = new Game(player1, player2);

        int drawEvery = 2;
        while (!Rules.IsEndOfGame(player1, player2, game))
        {

            Player currentPlayer = Game.GetPlayerTurnOrder(player1, player2, game.TurnCounter)[0];
            Player enemyPlayer = Game.GetPlayerTurnOrder(player1, player2, game.TurnCounter)[1];

            if (Game.Draw(game.TurnCounter, drawEvery, player1, player2))
            {
                Console.WriteLine("//// Draw Phase ////");
                Console.WriteLine();
                Console.WriteLine("Player 1 has Draw.");
                System.Console.WriteLine("");
                Console.WriteLine("Player 2 has Draw.");
                Console.WriteLine();
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine($"Is {currentPlayer.Name}'s turn:");
            Console.WriteLine();

            ConsolePlayer playingPlayer = (ConsolePlayer)currentPlayer;

            Print.GameInformation(currentPlayer, enemyPlayer, game);
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("//// Invocation Phase ////"); playingPlayer.PlayInvocationPhase(enemyPlayer, game);
            Console.WriteLine("//// Fight Phase ////"); playingPlayer.Fight(enemyPlayer, game);
            Console.WriteLine();
            Console.WriteLine("////// End Turn //////");
            Console.WriteLine();
            game.EndTurn();
        }
    }


}
