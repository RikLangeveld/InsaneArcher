using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Castle : SpriteGameObject
    {
        private int health = 100;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Castle() : base("spr_castle")
        {
            Position = new Vector2(0, InsaneKillerArcher.Screen.Y - Height - 18);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (health <= 0)
            {
                
            }

        }
    }
}
