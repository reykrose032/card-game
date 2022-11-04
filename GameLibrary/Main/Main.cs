using Cards;


Player playerA = new Player();
Player playerB = new Player();

List<Card> Ahand = new List<Card>();
List<Card> Bhand = new List<Card>();

List<Card> Afield = new List<Card>();
List<Card> Bfield = new List<Card>();

for (int turn = 1; turn <= 20; turn++)
{
    //fijando situacion del juego(player al q le toca jugar ,etc)
    //mejorar clase player para evitar todo el reguero q hay en el if y else alla abajo feisimo
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
        playerField = Afield;
        enemyField = Bfield;
        playerHand = Ahand;
        enemyHand = Bhand;
    }
    else
    {
        player = playerB;
        enemyPlayer = playerA;
        playerField = Bfield;
        enemyField = Afield;
        playerHand = Bhand;
        enemyHand = Ahand;
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

    int playerCardSelection = int.Parse(Console.ReadLine());
    int targetCardSelection = int.Parse(Console.ReadLine());
    if (playerCardSelection == -1)//-1 significaria q decidio pasar turno
        PlayerActions.IncreaseEnergy(player, 2);
    else
    {
        CardsActions.Attack(playerField[playerCardSelection], enemyField[targetCardSelection]);
        PlayerActions.DecreaseEnergy(player, 2);
        if (enemyField[targetCardSelection].health <= 0)
            enemyField.RemoveAt(targetCardSelection);
    }
}
