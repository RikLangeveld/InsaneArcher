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
        private int bounceNumber = 0;
        private int destroyAtbounces = 3;

        public CatapultBoulder(Vector2 position, Vector2 velocity) : base(0, "CatapultBoulder", new Dictionary<string, string>(), new Dictionary<string, Animation>(), position, velocity)
        {

            AddAnimation("spr_bolder_25p@4x4", "flying", true);


            PlayAnimation("flying");
            
            

            this.origin = Center;
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

        public void Bounce(float groundPositionY)
        {
            float overlapY = 0;

            InsaneKillerArcher.AssetManager.PlaySound("rock_hits_ground");

            //De bounce werkt nog niet helemaal goed.

            overlapY = position.Y + Height/2 - groundPositionY;
            Console.WriteLine(overlapY);

            Vector2 velocityNormal = Vector2.Normalize(velocity);
            Vector2 overlap = velocityNormal * velocityNormal;

            position.Y = groundPositionY;
            position.X -= overlap.X;

            velocity.Y = -velocity.Y * 0.4f;
            velocity.X = velocity.X * 0.4f;

            bounceNumber++;

            if (bounceNumber >= destroyAtbounces)
                visible = false;

            
        }
    }
}
