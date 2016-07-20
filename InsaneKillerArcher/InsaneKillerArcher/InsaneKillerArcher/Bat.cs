using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class Bat : Enemy
    {
        public Bat(string moveAnim, string deadAnim, string attackAnim) : base (moveAnim, deadAnim, attackAnim, 0.1f)
        {
            startPosition = new Vector2(InsaneKillerArcher.Screen.X + 100, 
                GameEnvironment.Random.Next(InsaneKillerArcher.Screen.Y - 200, InsaneKillerArcher.Screen.Y - 100));

            position = startPosition;

            movementSpeed = GameEnvironment.Random.Next(100, 125);
            health = 50f;
            attackDamage = 50f;
        }
    }
}
