public static class PlayerActions
{
    public static void IncreaseEnergy(Player a, int amount)
    {
        a.energy += amount;
    }
    public static void DecreaseEnergy(Player a, int amount)
    {
        a.energy -= amount;
    }
}