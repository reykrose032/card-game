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
public class ClientEffect : Effect, IEffect
{
    Interpreter Interpreter;
    public ClientEffect(Card ownCard, Card enemyCard, Interpreter interpreter) : base(ownCard, enemyCard)
    {
        this.Interpreter = interpreter;
    }

    public void ActivateEffect()
    {

    }
}
public class Weaken : Effect, IEffect
{
    public Weaken(Card ownCard, Card enemyCard) : base(ownCard, enemyCard) { }
    public void ActivateEffect()
    {
        enemyCard.AttackValue = enemyCard.AttackValue * 20 / 100;
    }
}
public class Heal : Effect, IEffect
{
    public Heal(Card ownCard, Card enemyCard) : base(ownCard, enemyCard) { }
    public void ActivateEffect()
    {
        ownCard.Health = ownCard.Health + ownCard.Health * 3 / 100;
    }
}
