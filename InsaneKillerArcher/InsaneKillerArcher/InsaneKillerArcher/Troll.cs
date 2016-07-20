using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class Troll : Enemy
    {
        public Troll(string moveAnim, string deadAnim, string attackAnim, int moneyDrop) : base(moveAnim, deadAnim, attackAnim, 1.5f, moneyDrop)
        {
            movementSpeed = GameEnvironment.Random.Next(25, 50);
            health = 250f;
            attackDamage = 50f;
        }
    }
}
