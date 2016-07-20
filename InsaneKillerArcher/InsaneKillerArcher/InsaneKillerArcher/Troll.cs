using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class Troll : Enemy
    {
        public Troll(string moveAnim, string deadAnim, string attackAnim) : base(moveAnim, deadAnim, attackAnim)
        {
            movementSpeed = GameEnvironment.Random.Next(25, 50);
            health = 250f;
            attackDamage = 50f;
            attackTimer = 2f;
        }
    }
}
