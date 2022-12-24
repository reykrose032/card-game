using GameLibrary.Objects;
using MiniCompiler;

public interface IEffect
{
    string Name { get; set; }
    void ActivateEffect(Card ownCard, Card enemyCard, Game game, int PlayerID, int EnemyPlayerID);
}

public class Weaken : IEffect//baja el damage del oponente a la mitad
{
    public string Name { get; set; }
    public Weaken()
    { Name = "Weaken"; }
    public void ActivateEffect(Card ownCard, Card enemyCard, Game game, int PlayerID, int EnemyPlayerID)
    {//como tengo el id del player jugando,puedo acceder a sus cosas tambien,y posiblemente tamb a las del rival
        enemyCard.AttackValue = enemyCard.AttackValue / 2;
    }
}
public class Heal : IEffect//cura un 20% de la vida max
{
    public string Name { get; set; }
    public Heal()
    { Name = "Heal"; }
    public void ActivateEffect(Card ownCard, Card enemyCard, Game game, int PlayerID, int EnemyPlayerID)
    {
        ownCard.HealthValue += ownCard.MaxAttackValue * 20 / 100;
    }
}


public class ClientEffect : IEffect
{
    public string Name { get; set; }

    Iinstruction instructions;
    public ClientEffect(string EffectName, Iinstruction instructions)
    { this.instructions = instructions; Name = "ClientEffect"; Name = EffectName; }

    public void ActivateEffect(Card ownCard, Card enemyCard, Game game, int PlayerID, int EnemyPlayerID)
    {
        if (GameData.GameActions.Count != 0) GameData.PreparingGameActionsDic();

        if (GameData.CardStats.Count != 0) GameData.PreparingGameStatsDic();

        GameData.UpdatingGameActionsDic(ownCard, enemyCard, game);
        GameData.UpdatingGameStatsDic(ownCard, enemyCard, game);
        instructions.Execute();
        GameData.UpdateCardStats(ownCard, enemyCard);
    }
}

