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

        var player1 = new Player("Franco", deck1);
        var player2 = new Player(deck2);

        bool wantIA = false;//preguntar si quiere jugar vs IA

        if (wantIA)
            player2 = new PlayerIA(deck2);
        else
            player2.Name = "Magela";

        var game = new Game(player1, player2);

        int drawEvery = 2;
        while (!Rules.IsEndOfGame(player1, player2, game))
        {
            bool endTurn = false;

            var currentPlayer = Game.GetPlayerTurnOrder(player1, player2, game.TurnCounter)[0];
            var enemyPlayer = Game.GetPlayerTurnOrder(player1, player2, game.TurnCounter)[1];

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

            do
            {
                Print.GameInformation(currentPlayer, enemyPlayer, game);
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("//// Invocation Phase ////");

                if (Rules.CanInvoke(currentPlayer))
                {
                    Print.GameInformation(currentPlayer, enemyPlayer, game);
                    Console.WriteLine("Choose the Card to Invoke or Press <<Enter>> to Omit:");
                    Console.WriteLine();
                    Print.Hand(currentPlayer);

                    int cardCoordinates = -1;
                    if (currentPlayer is PlayerIA)
                    {
                        PlayerIA tempPlayerIA = (PlayerIA)currentPlayer;
                        cardCoordinates = tempPlayerIA.GetCardToInvoke(currentPlayer, enemyPlayer, game);
                    }
                    else
                    {
                        var userInput = Console.ReadKey(true);
                        if (userInput.Key != ConsoleKey.Enter)
                        {
                            cardCoordinates = int.Parse(userInput.KeyChar.ToString());
                        }
                    }
                    Rules.InvokeCard(currentPlayer, game.Board, cardCoordinates);
                    System.Console.WriteLine($"{game.Board[currentPlayer][cardCoordinates].Name} was invoked");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.WriteLine("//// Fight Phase ////");
                if (Rules.CanAttack(currentPlayer, enemyPlayer, game))
                {
                    Console.WriteLine("Choose the Attacking Card or Press <<Enter>> to Omit:");
                    Print.GameInformation(currentPlayer, enemyPlayer, game);

                    int cardCoordinates = -1;
                    int targetCardCoordinates = -1;
                    if (currentPlayer is PlayerIA)
                    {
                        PlayerIA tempPlayerIA = (PlayerIA)currentPlayer;
                        cardCoordinates = tempPlayerIA.GetAttackingCard(currentPlayer, enemyPlayer, game);
                        targetCardCoordinates = tempPlayerIA.GetCardToAttack(currentPlayer, enemyPlayer, game);
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

                    Rules.AttackCard(currentPlayer, enemyPlayer, game.Board, cardCoordinates, targetCardCoordinates);
                    Console.WriteLine($"{game.Board[currentPlayer][cardCoordinates].Name} attacked {game.Board[enemyPlayer][targetCardCoordinates].Name} ");
                    Console.ReadKey();
                }

                endTurn = true;
            } while (!endTurn);

            Console.WriteLine();
            Console.WriteLine("////// End Turn //////");
            Console.WriteLine();
            game.EndTurn();
        }
    }


}
