using GameLibrary.Objects;
using MiniCompiler;


public interface IEffect
{
    string Name { get; set; }
    void ActivateEffect(Card ownCard, Card targetCard, Game game, int PlayerID, int EnemyPlayerID);
}

public class Weaken : IEffect//baja el damage del oponente a la mitad
{
    public string Name { get; set; }
    public Weaken()
    { Name = "Weaken"; }
    public void ActivateEffect(Card ownCard, Card targetCard, Game game, int PlayerID, int EnemyPlayerID)
    {//como tengo el id del player jugando,puedo acceder a sus cosas tambien,y posiblemente tamb a las del rival,lo q equivale a mayor cantidad de efectos posibles
        targetCard.AttackValue = targetCard.AttackValue / 2;
    }
}
public class Heal : IEffect//cura un 20% del maximo ataque de la carta objetivo
{
    public string Name { get; set; }
    public Heal()
    { Name = "Heal"; }
    public void ActivateEffect(Card ownCard, Card targetCard, Game game, int PlayerID, int EnemyPlayerID)
    {
        ownCard.HealthValue += targetCard.MaxAttackValue * 20 / 100;
    }
}

//efecto creado por el cliente/usuario
public class UserEffect : IEffect
{
    public string Name { get; set; }

    Iinstruction instructions; //abol de instrucciones del efecto
    public UserEffect(string EffectName, Iinstruction instructions)
    { this.instructions = instructions; Name = "ClientEffect"; Name = EffectName; }

    public void ActivateEffect(Card ownCard, Card targetCard, Game game, int PlayerID, int EnemyPlayerID)
    {
        if (GameData.GameActions.Count != 0) GameData.PreparingGameActionsDic();

        if (GameData.CardStats.Count != 0) GameData.PreparingGameStatsDic();

        GameData.UpdatingGameActionsDic(ownCard, targetCard, game);
        GameData.UpdatingGameStatsDic(ownCard, targetCard, game);
        instructions.Execute(); //ejecuta el arbol de instrucciones ,el cual usa ambos diccionarios para alterar el juego modificandolos
        GameData.UpdateCardStats(ownCard, targetCard);//actualiza el estado del juego mediante los diccionarios modificados por el arbol de instrucciones,lo q hace q los efectos hagan efecto XD
    }
}

