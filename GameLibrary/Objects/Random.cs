namespace GameLibrary.Objects;

public class RandomStuff
{

    public static int ComputeAttack()
    {
        Random random = new Random();
        return random.Next(Card.MinAttack, Card.MaxAttack);
    }

    public static int ComputeHealth()
    {
        Random random = new Random();
        return random.Next(Card.MinHealth, Card.MaxHealth);
    }
}