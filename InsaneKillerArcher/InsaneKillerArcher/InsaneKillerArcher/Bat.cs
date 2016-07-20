using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class Bat : Enemy
    {
        public Bat(string moveAnim, string deadAnim, string attackAnim, int moneyDrop) : base (moveAnim, deadAnim, attackAnim, moneyDrop)
        {
            startPosition = new Vector2(InsaneKillerArcher.Screen.X + 100, 
                GameEnvironment.Random.Next(InsaneKillerArcher.Screen.Y - 200, InsaneKillerArcher.Screen.Y - 100));

            position = startPosition;

            movementSpeed = GameEnvironment.Random.Next(100, 125);
            health = 50f;
            attackDamage = 50f;
            attackTimer = 0f;
        }
    }
}
