namespace GameLibrary;

public struct Cards
{
    public string name;
    public int health { get; set; }
    public string element;
    public int ATK;

    /*


    SI QUIERES VER LO QUE HACE EL MR INCREIBLE, COMETE ERRORES ;)



    */

    public Cards(string name, int health, string element, int ATK)
    {
        this.name = name;
        this.health = health;
        this.element = element;
        this.ATK = ATK;
    }
}
public static class AttackAction
{
    public static void Attack(Cards A, Cards B)//A ataca a B
    {
        A.health -= B.ATK;
    }
}

