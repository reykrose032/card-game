using GameLibrary;
using GameLibrary.Objects;

class Program
{
    static void Main(string[] args)
    {
        var card1 = new Card("Angel", 80, 80, Species.Angel);
        var card2 = new Card("Pancho", 80, 80, Species.Cthulhu);
        var card3 = new Card("Rudy", 80, 80, Species.Dragon);

        var deck1 = new Queue<Card>();
        deck1.Enqueue(card1);
        deck1.Enqueue(card2);
        deck1.Enqueue(card3);
        var player1 = new Player("Kevin", deck1);
        var player2 = new Player("AI", deck1);
        var game = new Game(player1, player2);
        
        Console.WriteLine(game.IsEndOfGame());
    }
}
