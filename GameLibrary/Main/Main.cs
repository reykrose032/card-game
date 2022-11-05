using Cards;


Player playerA = new Player();
Player playerB = new Player();

List<Card> handA = new List<Card>();
List<Card> handB = new List<Card>();

List<Card> fieldA = new List<Card>();
List<Card> fieldB = new List<Card>();

for (int turn = 1; turn <= 20; turn++)
{
    //fijando situacion del juego(player al q le toca jugar ,etc)

    Player player = new Player();
    Player enemyPlayer = new Player();
    List<Card> playerField = new List<Card>();
    List<Card> enemyField = new List<Card>();
    List<Card> playerHand = new List<Card>();
    List<Card> enemyHand = new List<Card>();
    if (turn % 2 != 0)//turno par=playerA playing,turno impar=playerB playing
    {
        player = playerA;
        enemyPlayer = playerB;
        playerField = fieldA;
        enemyField = fieldB;
        playerHand = handA;
        enemyHand = handB;
    }
    else
    {
        player = playerB;
        enemyPlayer = playerA;
        playerField = fieldB;
        enemyField = fieldA;
        playerHand = handB;
        enemyHand = handA;
    }
    while (player.energy > 0)//invocando cartas
    {
        if (Console.ReadLine().ToLower() == "i")
        {
            int cardToInvoke = int.Parse(Console.ReadLine());
            CardsActions.InvokeCard(playerHand[cardToInvoke], playerField);
            PlayerActions.DecreaseEnergy(player, 2);
        }
        else
            break;
    }

    for (; player.energy > 0;)
    {
        int playerCardSelection = int.Parse(Console.ReadLine());//seleccionando carta propia
        int targetCardSelection = int.Parse(Console.ReadLine());//seleccionando carta contraria
        if (playerCardSelection == -1)//-1 significaria q decidio pasar turno
        {
            PlayerActions.IncreaseEnergy(player, 2);
            break;
        }
        else
        {
            CardsActions.Attack(playerField[playerCardSelection], enemyField[targetCardSelection]);
            PlayerActions.DecreaseEnergy(player, 2);
            if (enemyField[targetCardSelection].health <= 0)//destruyendo carta con vida negativa
                enemyField.RemoveAt(targetCardSelection);
        }
    }
}
