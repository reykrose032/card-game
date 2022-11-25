using GameLibrary;
using GameLibrary.Objects;
using Utils;
using MiniCompiler;

class Program
{
    static void Main(string[] args)
    {
        var deck1 = GenerateDeck(4);
        var deck2 = GenerateDeck(4);
        //testing the compiler by adding a new card to the deck1
        string userCodeInput = "Name: Franco Hernandez ; ATK: 10 ; Health: 20 ; Specie: Angel ; EffectAction: card.Health = card.MaxHealth ;";
        Interpreter interpreter = new Interpreter(userCodeInput);//agregar TokenType.NONE para no tener angel por default
        Card ClientCard = new Card(interpreter.newInitialCardName, interpreter.newInitialCardATK, interpreter.newInitialCardHealth, interpreter.newInitialCardSpecie);
        ClientCard.effect = new ClientEffect(ClientCard, deck1.ElementAt(0), interpreter);
        deck1.Enqueue(ClientCard);

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
                        if (game.Board[currentOpponent].Count == 0)//hay q pensar sobre esto ,pq se supoo=ne qq nunca te quedes sin cartas
                        //ya q los player no tienen vida,es decir si te quedas sin cartas en el campo,entonces eres invulnerable,yo creo
                        //q deberia ser mejor poner de regla q si te quedas sin cartas en campo,pierdes.Tampoco es literal asi,por ejemp
                        //si tengo una carta restante y me la destruyen entonces pierdo?no lo veo justo,tenemos q pensar sobre esto...
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
        Print.PlayerChoices(currentPlayer.Hand);
        System.Console.WriteLine();

        var userInput = Console.ReadKey();
        System.Console.WriteLine();
        if (!Input.IsValidInput(userInput)) return;

        var cardIndex = int.Parse(userInput.KeyChar.ToString());
        if (!Input.IsValidInput(cardIndex, currentPlayer.Hand.Count)) return;

        var cardToInvoke = currentPlayer.Hand.ElementAt(cardIndex);
        currentPlayer.Invoke(cardToInvoke, board);
    }

    static void AttackCard(Player currentPlayer, Player currentOpponent, Dictionary<Player, List<Card>> board)
    {
        System.Console.WriteLine("Choose attacking card:");
        Print.PlayerChoices(board[currentPlayer]);
        System.Console.WriteLine();

        var userInput = Console.ReadKey();
        System.Console.WriteLine();
        if (!Input.IsValidInput(userInput)) return;

        var cardIndex = int.Parse(userInput.KeyChar.ToString());
        if (!Input.IsValidInput(cardIndex, board[currentPlayer].Count)) return;

        var attackingCard = board[currentPlayer].ElementAt(cardIndex);

        System.Console.WriteLine("Choose card to attack:");
        Print.PlayerChoices(board[currentOpponent]);
        System.Console.WriteLine();

        userInput = Console.ReadKey();
        if (!Input.IsValidInput(userInput)) return;

        cardIndex = int.Parse(userInput.KeyChar.ToString());
        if (!Input.IsValidInput(cardIndex, board[currentOpponent].Count)) return;

        var cardToAttack = board[currentOpponent].ElementAt(cardIndex);

        attackingCard.Attack(cardToAttack);
    }
}
