using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Zeppelin : Enemy
    {

        public Zeppelin(string moveAnim, string deadAnim, string attackAnim, int moneyDrop) : base(moveAnim, deadAnim, attackAnim, 3f, moneyDrop)
        {
            startPosition = new Vector2(InsaneKillerArcher.Screen.X + 100,
                GameEnvironment.Random.Next(InsaneKillerArcher.Screen.Y - 750, InsaneKillerArcher.Screen.Y - 300));

            position = startPosition;

            movementSpeed = 50;
            health = 300f;
            attackDamage = 75f;
        }
    }
}
