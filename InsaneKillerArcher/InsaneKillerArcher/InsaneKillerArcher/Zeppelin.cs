using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Zeppelin : SpriteGameObject
    {
        private Vector2 startPosition = new Vector2(InsaneKillerArcher.Screen.X + 100, 
            GameEnvironment.Random.Next(InsaneKillerArcher.Screen.Y - 750, InsaneKillerArcher.Screen.Y - 300));
        private float movementSpeed = 50;
        private float health = 300f;

        public Zeppelin() : base("spr_zeppelin")
        {
            position = startPosition;

            Flying();
        }

        public void Idle()
        {
            velocity = Vector2.Zero;
        }

        public void Flying()
        {
            velocity = new Vector2(-movementSpeed, 0);
        }

        public void Dead()
        {
            velocity = Vector2.Zero;
            visible = false;
        }

        public float Health
        {
            get { return health; }
            set { health = value; }
        }
    }
}
