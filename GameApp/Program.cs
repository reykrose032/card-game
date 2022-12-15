using GameLibrary;
using GameLibrary.Objects;
using Utils;
using GameInterpreter;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        // System.Console.Write("> ");
        // var input = Console.ReadLine();
        // var input = "123 + 543";
        // if (!string.IsNullOrEmpty(input))
        // {
        //     var interpreter = new Interpreter(input);
        //     var result = interpreter.Expression();
        //     System.Console.WriteLine(result);
        // }

        var deck1 = GenerateDeck(3);
        var deck2 = GenerateDeck(3);

        var player1 = new Player("Kevin", deck1);
        var player2 = new Player("Franco", deck2);

        var game = new Game(player1, player2);

        Play(game);
    }

    static void Play(Game game)
    {
        Console.Clear();
        // AnsiConsole.Markup("[bold red]Battle Begins!!![/] \n");
        AnsiConsole.Write(new FigletText("Battle Begins!!!").Centered().Color(Color.Red));

        while (!game.IsEndOfGame())
        {
            bool endTurn = false;

            var currentPlayer = game.IsTurnOf();
            var currentOpponent = game.IsNotTurnOf();

            while (!endTurn)
            {
                Console.WriteLine($"Is {currentPlayer.Name}'s turn:");
                Print.Board(game.Board);
                AnsiConsole.Markup($"Choose an action [yellow](Energy: {currentPlayer.Energy})[/]:\n");
                AnsiConsole.Markup("[green]I: Invoke Card[/], [red]A: Attack[/], <Enter>: End Turn");

                var action = Console.ReadKey(true);
                System.Console.WriteLine();
                switch (action.Key)
                {
                    case ConsoleKey.I:
                        if (currentPlayer.Hand.Count == 0)
                        {
                            Print.PromptUserError("You don't have any cards in your hand");
                            break;
                        }
                        InvokeCard(currentPlayer, game.Board);
                        break;

                    case ConsoleKey.A:
                        if (currentPlayer.Energy == 0)
                        {
                            Print.PromptUserError("You don't have energy to attack :(");
                            break;
                        }
                        if (game.Board[currentPlayer].Count == 0)
                        {
                            Print.PromptUserError("You don't have cards in the board");
                            break;
                        }
                        if (game.Board[currentOpponent].Count == 0)
                        {
                            Print.PromptUserError("There's no opponent card to attack");
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

                Console.Clear();
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

    static Card GenerateCard(string name)
    {
        var species = Enum.GetValues(typeof(Species));
        var random = new Random();
        var cardSpecies = (Species)species.GetValue(random.Next(species.Length));
        var card = new Card(name, cardSpecies);
        return card;
    }

    static Queue<Card> GenerateDeck(int n)
    {
        var names = new string[]{"Peter", "Bullock", "Bender", "Bowser", "Lynch", "Flynn"}.ToList();
        var cards = new Queue<Card>();
        for (int i = 0; i < n; i++)
        {
            var randomIndex = new Random().Next(names.Count);
            var randomName = names.ElementAt(randomIndex);
            names.RemoveAt(randomIndex);
            cards.Enqueue(GenerateCard(randomName));
        }
        return cards;
    }

    static Card PickCardFrom(List<Card> cards)
    {
        Print.PlayerDeck(cards);

        var names = new List<string>();
        names.Add("...");
        foreach (var card in cards)
        {
            names.Add(card.Name);
        }

        var pageSize = cards.Count >= 2? cards.Count + 1 : 3;

        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .PageSize(pageSize)
                            .AddChoices(names));
        
        // BUG: If two or more cards have the same Name and any other than the first one is choosen,
        // the first one will always be choosen.
        var choosenCard = cards.Find((card) => card.Name == choice);
        
        return choosenCard;
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
        currentPlayer.Energy--;
    }
}
