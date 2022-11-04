using Cards;
using CardsActions;

Player playerA = new Player();
Player playerB = new Player();
//suponiendo q cada player tiene sus respectivas cartas en mano
List<Card> playerAfield = new List<Card>();
List<Card> playerBfield = new List<Card>();

for (int turn = 1; turn <= 20; turn++)
{
    int playerCardSelection = int.Parse(Console.ReadLine());
    int oponentCardSelection = int.Parse(Console.ReadLine());
    if (playerCardSelection == -1)
    {
        playerA.energy += 2;
        continue;
    }
    AttackAction.Attack(playerAfield[playerCardSelection], playerBfield[oponentCardSelection]);
    playerA.energy -= 2;
    if (playerBfield[oponentCardSelection].health <= 0)
        playerBfield.RemoveAt(oponentCardSelection);

}
