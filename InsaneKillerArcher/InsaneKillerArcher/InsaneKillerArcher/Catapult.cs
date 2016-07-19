using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Catapult : SpriteGameObject
    {
        private float damage;
        private float shootDelay;
        private float timer;

        public Catapult() : base("spr_archer")
        {
            damage = 100.0f;
            shootDelay = 3.0f;
            timer = 0.0f;
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > shootDelay)
            {
                (GameWorld as GameWorld).CatapultShoot();

                timer = 0f;
            }

            base.Update(gameTime);
        }

        public float Damage
        {
            get { return damage; }
            set { damage = value; }
        }
    }
}
