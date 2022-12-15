using GameLibrary.Objects;
using MiniCompiler;

public interface IEffect
{
    void ActivateEffect(Card ownCard, Card enemyCard, Game game);
}

public class Weaken : IEffect//baja el damage del oponente a la mitad
{
    public void ActivateEffect(Card ownCard, Card enemyCard, Game game)
    {
        enemyCard.AttackValue = enemyCard.AttackValue / 2;
    }
}
public class Heal : IEffect//cura un 20% de la vida max
{
    public void ActivateEffect(Card ownCard, Card enemyCard, Game game)
    {
        ownCard.HealthValue += ownCard.MaxAttackValue * 20 / 100;
    }
}


public class ClientEffect : IEffect
{
    Iinstruction instructions;
    public ClientEffect(Iinstruction instructions) => this.instructions = instructions;

    public void ActivateEffect(Card ownCard, Card enemyCard, Game game)
    {
        if (GameData.gameActions.Count != 0)
            GameData.PreparingGameActionsDic();
        if (GameData.cardsStatsDic.Count != 0)
            GameData.FillGameStatsDic();
        GameData.PreparingGameActionsDic(ownCard, enemyCard);
        GameData.UpdateGameStatsDic(ownCard, enemyCard, game);
        instructions.Execute();
        GameData.UpdateCardStats(ownCard, enemyCard);
    }
}
