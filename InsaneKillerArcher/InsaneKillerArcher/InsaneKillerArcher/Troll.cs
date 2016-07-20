using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class Troll : Enemy
    {
        public Troll(string moveAnim, string deadAnim, string attackAnim, int moneyDrop) : base(moveAnim, deadAnim, attackAnim, moneyDrop)
        {
            movementSpeed = GameEnvironment.Random.Next(25, 50);
            health = 250f;
            attackDamage = 50f;
            attackTimer = 2f;
        }
    }
}
