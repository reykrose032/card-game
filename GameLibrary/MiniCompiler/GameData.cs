using GameLibrary.Objects;

namespace MiniCompiler;
public static class GameData
{
    public static Dictionary<string, int> cardsStatsDic = new Dictionary<string, int>(); //relaciona cada nombre de variable entre ambas cartas con su valor
    public static Dictionary<string, Actions> gameActions = new Dictionary<string, Actions>();

    public static void PreparingGameActionsDic()
    {
        void Void() { }
        gameActions.Add("Draw()", Void);
        gameActions.Add("IncreaseEnergy()", Void);
        gameActions.Add("DecreaseEnergy()", Void);
        gameActions.Add("enemyDraw()", Void);
        gameActions.Add("enemyIncreaseEnergy()", Void);
        gameActions.Add("enemyDecreaseEnergy()", Void);
    }

    public static void PreparingGameActionsDic(Card ownCard, Card enemyCard)
    {
        gameActions["Draw()"] = ownCard.owner.Draw;
        gameActions["IncreaseEnergy()"] = ownCard.owner.IncreaseEnergy;
        gameActions["DecreaseEnergy()"] = ownCard.owner.DecreaseEnergy;
        gameActions["enemyDraw()"] = enemyCard.owner.Draw;
        gameActions["enemyIncreaseEnergy()"] = enemyCard.owner.IncreaseEnergy;
        gameActions["enemyDecreaseEnergy()"] = enemyCard.owner.DecreaseEnergy;
    }

    public static void FillGameStatsDic()
    {
        cardsStatsDic.Add("ownCard.Health", 0);
        cardsStatsDic.Add("ownCard.MaxHealth", 0);
        cardsStatsDic.Add("ownCard.AttackValue", 0);
        cardsStatsDic.Add("ownCard.MaxAttackValue", 0);

        cardsStatsDic.Add("enemyCard.Health", 0);
        cardsStatsDic.Add("enemyCard.MaxHealth", 0);
        cardsStatsDic.Add("enemyCard.AttackValue", 0);
        cardsStatsDic.Add("enemyCard.MaxAttackValue", 0);

        cardsStatsDic.Add("NOCInPlayerHand", 0);
        cardsStatsDic.Add("NOCInEnemyPlayerHand", 0);
        cardsStatsDic.Add("NOCInPlayerField", 0);
        cardsStatsDic.Add("NOCInEnemyPlayerField", 0);

    }


    public static void UpdateGameStatsDic(Card ownCard, Card enemyCard, Game gameState)
    {
        cardsStatsDic["ownCard.Health"] = ownCard.HealthValue;
        cardsStatsDic["ownCard.MaxHealthValue"] = ownCard.MaxHealthValue;
        cardsStatsDic["ownCard.AttackValue"] = ownCard.AttackValue;
        cardsStatsDic["ownCard.MaxAttackValue"] = ownCard.MaxAttackValue;

        cardsStatsDic["ownCard.Health"] = enemyCard.HealthValue;
        cardsStatsDic["ownCard.MaxHealthValue"] = enemyCard.MaxHealthValue;
        cardsStatsDic["ownCard.AttackValue"] = enemyCard.AttackValue;
        cardsStatsDic["ownCard.MaxAttackValue"] = enemyCard.MaxAttackValue;

        cardsStatsDic["NOCInPlayerHand"] = ownCard.owner.Hand.Count;
        cardsStatsDic["NOCInEnemyPlayerHand"] = enemyCard.owner.Hand.Count;
        cardsStatsDic["NOCInPlayerField"] = GetNOCInPlayerField(gameState, ownCard.owner);
        cardsStatsDic["NOCInEnemyPlayerField"] = GetNOCInPlayerField(gameState, enemyCard.owner);

        cardsStatsDic["PlayerEnergy"] = ownCard.owner.Energy;
        cardsStatsDic["EnemyPlayerEnergy"] = enemyCard.owner.Energy;

        cardsStatsDic["NOCInPlayerDeck"] = ownCard.owner.Deck.Count;
        cardsStatsDic["NOCInEnemyPlayerDeck"] = enemyCard.owner.Deck.Count;

    }


    static int GetNOCInPlayerField(Game gameState, Player player)
    {
        return gameState.Board[player].Count;
    }
    //esto es para modificar las estadisticas de las cartas 
    public static void UpdateCardStats(Card ownCard, Card enemyCard)
    {
        ownCard.HealthValue = cardsStatsDic["ownCard.Health"];
        ownCard.MaxHealthValue = cardsStatsDic["ownCard.MaxHealthValue"];
        ownCard.AttackValue = cardsStatsDic["ownCard.AttackValue"];
        ownCard.MaxAttackValue = cardsStatsDic["ownCard.MaxAttackValue"];

        enemyCard.HealthValue = cardsStatsDic["ownCard.Health"];
        enemyCard.MaxHealthValue = cardsStatsDic["ownCard.MaxHealthValue"];
        enemyCard.AttackValue = cardsStatsDic["ownCard.AttackValue"];
        enemyCard.MaxAttackValue = cardsStatsDic["ownCard.MaxAttackValue"];
    }

}