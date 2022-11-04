using Cards;
using CardsActions;

Player playerA = new Player();
Player playerB = new Player();
//suponiendo q cada player tiene sus respectivas cartas en mano
List<Card> playerAfield = new List<Card>();
List<Card> playerBfield = new List<Card>();

for (int turn = 1; turn <= 20; turn++)
{
    Player player = new Player();
    Player enemyPlayer = new Player();
    List<Card> playerField = new List<Card>();
    List<Card> enemyField = new List<Card>();
    if (turn % 2 != 0)//turno par=playerA playing,turno impar=playerB playing
    {
        player = playerA;
        enemyPlayer = playerB;
        playerField = playerAfield;
        enemyField = playerBfield;
    }
    else
    {
        player = playerB;
        enemyPlayer = playerA;
        playerField = playerBfield;
        enemyField = playerAfield;
    }
    int playerCardSelection = int.Parse(Console.ReadLine());
    int targetCardSelection = int.Parse(Console.ReadLine());
    if (playerCardSelection == -1)
    {
        PlayerActions.IncreaseEnergy(player, 2);
        continue;
    }
    AttackAction.Attack(playerField[playerCardSelection], enemyField[targetCardSelection]);
    PlayerActions.DecreaseEnergy(player, 2);
    if (enemyField[targetCardSelection].health <= 0)
        enemyField.RemoveAt(targetCardSelection);
}
