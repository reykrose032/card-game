using GameLibrary.Objects;
using MiniCompiler;
interface IEffect
{
    void ActivateEffect();
}
public abstract class Effect
{
    public Card ownCard;
    public Card enemyCard;
    public Effect(Card ownCard, Card enemyCard)
    {
        this.ownCard = ownCard;
        this.enemyCard = enemyCard;
    }
}

public class Weaken : Effect, IEffect//baja el damage del oponente a la mitad
{
    public Weaken(Card ownCard, Card enemyCard) : base(ownCard, enemyCard) { }
    public void ActivateEffect()
    {
        enemyCard.AttackValue = enemyCard.AttackValue / 2;
    }
}
public class Heal : Effect, IEffect//cura un 20% de la vida max
{
    public Heal(Card ownCard, Card enemyCard) : base(ownCard, enemyCard) { }
    public void ActivateEffect()
    {
        ownCard.Health += ownCard.MaxAttackValue * 20 / 100;
    }
}
