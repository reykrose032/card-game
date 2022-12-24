namespace GameLibrary.Objects;

public class RandomStuff
{
    static Random random = new Random();
    public static int ComputeAttack()
    {

        return random.Next(Card.MinAttack, Card.MaxAttack);
    }

    public static int ComputeHealth()
    {

        return random.Next(Card.MinHealth, Card.MaxHealth);
    }
    public static int GetRandomNumberUpTo(int n)
    {

        return random.Next(n);
    }
}