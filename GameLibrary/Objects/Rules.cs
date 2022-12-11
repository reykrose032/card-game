using GameLibrary.Objects;
using GameApp;
namespace GameRules
{


    public static class Rules
    {
        public static List<string> GetRulesList(Player currentPlayer, Player enemyPlayer, Game game)
        {
            List<string> actions = new List<string>();
            Player playingPlayer = Game.GetPlayerTurnOrder(currentPlayer, enemyPlayer, game.TurnCounter)[0];
            if (playingPlayer.Hand.Count != 0)
                actions.Add("Invocation's Phase");
            if ((playingPlayer.Energy != 0) && (game.Board[playingPlayer].Count != 0))
                actions.Add("Fight's Phase");
            actions.Add("End Turn");
            return actions;
        }
        public static bool CanInvoke(Player currentPlayer) => currentPlayer.Hand.Count != 0;
        public static bool CanAttack(Player currentPlayer, Player enemyPlayer, Game game) => game.Board[currentPlayer].Count != 0 && game.Board[enemyPlayer].Count != 0;
        public static void InvokeCard(Player currentPlayer, Dictionary<Player, List<Card>> board, int userInput)
        {
            if (!Input.IsValidInput(userInput, currentPlayer.Hand.Count)) return;
            var cardToInvoke = currentPlayer.Hand.ElementAt(userInput);
            currentPlayer.Invoke(cardToInvoke, board);
        }
        public static void AttackCard(Player currentPlayer, Player currentOpponent, Dictionary<Player, List<Card>> board, int attackingCardCoordinates, int targetCardCoordinates)
        {
            var attackingCard = board[currentPlayer].ElementAt(targetCardCoordinates);
            var cardToAttack = board[currentOpponent].ElementAt(attackingCardCoordinates);
            attackingCard.Attack(cardToAttack);
        }

        public static bool IsEndOfGame(Player Player1, Player Player2, Game game)
        {
            if (game.TurnCounter >= 20)
                return true;
            if (HasLost(Player1, game) || HasLost(Player2, game))
                return true;

            return false;
        }
        public static bool HasLost(Player player, Game game)
        => player.Deck.Count == 0 && player.Hand.Count == 0 && game.Board[player].Count == 0;

    }
}
