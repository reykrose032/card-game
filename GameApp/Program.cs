using GameLibrary;
using GameLibrary.Objects;

class Program
{
    static void Main(string[] args)
    {
        var deck1 = GenerateDeck(4);
        var deck2 = GenerateDeck(4);

        var player1 = new Player("Kevin", deck1);
        var player2 = new Player("AI", deck2);

        var game = new Game(player1, player2);

        while (!game.IsEndOfGame())
        {
            bool endTurn = false;

            var currentPlayer = game.IsTurnOf();
            var currentOpponent = game.IsNotTurnOf();

            if (game.TurnCounter % 2 == 0)
            {
                currentPlayer.Draw();
                currentOpponent.Draw();
            }

            Console.WriteLine($"Is {currentPlayer.Name}'s turn:");

            while (!endTurn)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("I: Invoke Card, A: Attack, <Enter>: End Turn");

                var action = Console.ReadKey();
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

    static void InvokeCard(Player currentPlayer, Dictionary<Player, List<Card>> board)
    {
        System.Console.WriteLine("Choose card to invoke:");
        foreach (var card in currentPlayer.Hand)
        {
            Console.Write($"{currentPlayer.Hand.IndexOf(card)} - {card.Name}, ");
        }
        System.Console.WriteLine();

        var userInput = Console.ReadKey();
        System.Console.WriteLine();
        if (!char.IsDigit(userInput.KeyChar))
        {
            System.Console.WriteLine("Key is not a digit.");
            return;
        }

        var cardIndex = int.Parse(userInput.KeyChar.ToString());
        if (cardIndex >= currentPlayer.Hand.Count)
        {
            System.Console.WriteLine("Wrong key! Not a card in there.");
            return;
        }

        var cardToInvoke = currentPlayer.Hand.ElementAt(cardIndex);
        currentPlayer.Invoke(cardToInvoke, board);
    }

    static void AttackCard(Player currentPlayer, Player currentOpponent, Dictionary<Player, List<Card>> board)
    {
        System.Console.WriteLine("Choose attacking card:");
        foreach (var card in board[currentPlayer])
        {
            Console.Write($"{board[currentPlayer].IndexOf(card)} - {card.Name}, ");
        }
        System.Console.WriteLine();

        var userInput = Console.ReadKey();
        System.Console.WriteLine();
        if (!char.IsDigit(userInput.KeyChar))
        {
            System.Console.WriteLine("Key is not a digit.");
            return;
        }

        var cardIndex = int.Parse(userInput.KeyChar.ToString());
        if (cardIndex >= board[currentPlayer].Count)
        {
            System.Console.WriteLine("Wrong key! Not a card in there.");
            return;
        }

        var attackingCard = board[currentPlayer].ElementAt(cardIndex);

        System.Console.WriteLine("Choose card to attack:");
        foreach (var card in board[currentOpponent])
        {
            Console.Write($"{board[currentOpponent].IndexOf(card)} - {card.Name}, ");
        }
        System.Console.WriteLine();

        userInput = Console.ReadKey();
        if (!char.IsDigit(userInput.KeyChar))
        {
            System.Console.WriteLine("Key is not a digit.");
            return;
        }

        cardIndex = int.Parse(userInput.KeyChar.ToString());
        if (cardIndex >= board[currentOpponent].Count)
        {
            System.Console.WriteLine("Wrong key! Not a card in there.");
            return;
        }

        var cardToAttack = board[currentOpponent].ElementAt(cardIndex);

        attackingCard.Attack(cardToAttack);
    }
}
