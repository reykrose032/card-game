using Cards;

namespace CardsActions
{
    public static class AttackAction
    {
        public static void Attack(Card A, Card B)//A ataca a B
        {
            A.health -= B.ATK;
        }
    }
}