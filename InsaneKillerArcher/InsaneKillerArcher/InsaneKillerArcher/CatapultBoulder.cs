using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class CatapultBoulder : Projectile
    {
        private bool firstBounce = false;
        public bool FirstBounce { get { return firstBounce; } set { firstBounce = value; } }

        private float gravity = 3f;

        public CatapultBoulder(Vector2 position, Vector2 velocity) : base(0, "CatapultBoulder", new Dictionary<string, string>(), new Dictionary<string, Animation>(), position, velocity)
        {
            addAnimation("spr_bolder_25p@4x4", "flying", true);

            PlayAnimation("flying");
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Y += gravity;

            base.Update(gameTime);
        }

        public float Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }

        public void Bounce()
        {
            velocity.Y = -velocity.Y * 0.9f;
        }
    }
}
