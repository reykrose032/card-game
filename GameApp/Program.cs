using GameLibrary;
using GameLibrary.Objects;
using MiniCompiler;


namespace GameApp;

class Program
{
    static void Main(string[] args)
    {
        var deck1 = Start.GenerateDeck(4);
        var deck2 = Start.GenerateDeck(4);
        //testing the compiler by adding a new card to the deck1
        string userCodeInput = "Name: Franco Hernandez ; InitialATK: 10 ; InitialHealth: 20 ; InitialSpecie: Angel ; ownCard.Health = enemyCard.MaxHealth ;";

        Interpreter interpreter = new Interpreter();//agregar TokenType.NONE para no tener angel por default

        Player player1 = new ConsolePlayer(new Player("Franco", deck1, false));
        Player player2 = new ConsolePlayer(new Player("Magela", deck2, false));

        //ve entendiendo la implementacion,abstraete de como funciona,por ahora,poco a poco voy haciendolo mas legible
        interpreter.EatUserCode(userCodeInput);//cada nuevo codigo es introducido en este metodo
        deck1.Enqueue(interpreter.BuildCard(player1));//este metodo crea una nueva carta segun el codigo metido en el metodo anterior
        IEffect clientEffect = interpreter.BuildEffect();//este metodo crea un nuevo efecto segun el codigo metido dos lineas mas arriba
        //cada vez q quieras crear una nueva carta o efecto,hay q usar el metodo EatCode q interpreta el codigo para poder crear cartas y efectos en base a el ultimo codigo "comido


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
            game.EndTurn();
        }
    }


}
