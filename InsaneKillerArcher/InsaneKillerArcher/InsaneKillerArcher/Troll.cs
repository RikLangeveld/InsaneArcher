using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class Troll : Enemy
    {
        public Troll(string moveAnim, string deadAnim, string attackAnim) : base(moveAnim, deadAnim, attackAnim, 1.5f)
        {
            movementSpeed = GameEnvironment.Random.Next(25, 50);
            health = 250f;
            attackDamage = 50f;
        }
    }
}
