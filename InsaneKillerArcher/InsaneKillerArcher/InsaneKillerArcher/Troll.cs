using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class Troll : Enemy
    {
        public Troll(string moveAnim, string deadAnim, string attackAnim) : base(moveAnim, deadAnim, attackAnim)
        {
            startPosition = new Vector2(InsaneKillerArcher.Screen.X + 100, InsaneKillerArcher.Screen.Y - 20);
            movementSpeed = GameEnvironment.Random.Next(25, 50);
            health = 250f;
            attackDamage = 50f;
            attackTimer = 2f;
        }
    }
}
