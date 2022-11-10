using GameLibrary;
using GameLibrary.Objects;
using Utils;

class Program
{
    static void Main(string[] args)
    {
        var deck1 = GenerateDeck(8);
        var deck2 = GenerateDeck(8);

        var player1 = new Player("Kevin", deck1);
        var player2 = new Player("AI", deck2);

        var game = new Game(player1, player2);

        Play(game);
    }

    static void Play(Game game)
    {
        while (!game.IsEndOfGame())
        {
            bool endTurn = false;

            var currentPlayer = game.IsTurnOf();
            var currentOpponent = game.IsNotTurnOf();

            Console.WriteLine($"Is {currentPlayer.Name}'s turn:");

            while (!endTurn)
            {
                Print.Board(game.Board);
                Console.WriteLine("Choose an action:");
                Console.WriteLine("I: Invoke Card, A: Attack, <Enter>: End Turn");

                var action = Console.ReadKey();
                System.Console.WriteLine();
                switch (action.Key)
                {
                    case ConsoleKey.I:
                        if (currentPlayer.Hand.Count == 0)
                        {
                            System.Console.WriteLine("You don't have any cards in your hand. Choose another action.");
                            break;
                        }
                        InvokeCard(currentPlayer, game.Board);
                        break;

                    case ConsoleKey.A:
                        if (currentPlayer.Energy == 0)
                        {
                            System.Console.WriteLine("You don't have energy to attack. Choose another action.");
                            break;
                        }
                        if (game.Board[currentPlayer].Count == 0)
                        {
                            System.Console.WriteLine("You don't have cards in the board.");
                            break;
                        }
                        if (game.Board[currentOpponent].Count == 0)
                        {
                            System.Console.WriteLine("No Opponent card to attack.");
                            break;
                        }
                        AttackCard(currentPlayer, currentOpponent, game.Board);
                        game.UpdateBoard();
                        break;

                    case ConsoleKey.Enter:
                        endTurn = true;
                        break;

                    default:
                        System.Console.WriteLine("Wrong Key!");
                        break;
                }
            }

            game.EndTurn();
            
            if (game.TurnCounter % 2 == 0)
            {
                currentPlayer.Draw();
                currentPlayer.Energy++;
                currentOpponent.Draw();
                currentOpponent.Energy++;
            }
        }
    }

    static Card GenerateCard()
    {
        var species = Enum.GetValues(typeof(Species));
        var random = new Random();
        var cardSpecies = (Species)species.GetValue(random.Next(species.Length));
        var card = new Card(cardSpecies.ToString(), cardSpecies);
        return card;
    }

    static Queue<Card> GenerateDeck(int n)
    {
        var cards = new Queue<Card>();
        for (int i = 0; i < n; i++)
        {
            cards.Enqueue(GenerateCard());
        }
        return cards;
    }

    static Card PickCardFrom(List<Card> cards)
    {
        Print.PlayerCards(cards);

        var userInput = Console.ReadKey();
        System.Console.WriteLine();
        if (!InputValidation.IsDigit(userInput))  
            return null;

        var cardIndex = int.Parse(userInput.KeyChar.ToString());
        if (!InputValidation.IsIndexOutOfBounds(cardIndex, cards.Count)) 
            return null;

        return cards.ElementAt(cardIndex);
    }

    static void InvokeCard(Player currentPlayer, Dictionary<Player, List<Card>> board)
    {
        System.Console.WriteLine("Choose card to invoke:");
        var cardToInvoke = PickCardFrom(currentPlayer.Hand);
        if (cardToInvoke == null)
            return;
        
        currentPlayer.Invoke(cardToInvoke, board);
    }

    static void AttackCard(Player currentPlayer, Player currentOpponent, Dictionary<Player, List<Card>> board)
    {
        System.Console.WriteLine("Choose attacking card:");
        var attackingCard = PickCardFrom(board[currentPlayer]);
        if (attackingCard == null)
            return;

        System.Console.WriteLine("Choose card to attack:");
        var cardToAttack = PickCardFrom(board[currentOpponent]);
        if (cardToAttack == null)
            return;

        attackingCard.Attack(cardToAttack);
    }
}
