using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Arrow : SpriteGameObject
    { 

        private bool active = true;
        public bool Active { get { return active; } set {active = value;} }

        //tweak values
        private float gravity = 2.0f;
        private float speedVermindering = 900;
        public float deleteTimer = 0.0f;
        public float damage = 50f;
        
        public float Gravity { get { return gravity;} set { gravity = value; } }


        private float angle = 0.0f;

        public Arrow(Vector2 position, Vector2 directionNormal, float speed) : base("spr_arrow")
        {

            origin = Center;
            this.position = position;
            this.velocity = directionNormal * speed;

        }

        public Arrow(Vector2 position, Vector2 directionNormal, float speed, Vector2 richtingsVector) : base("spr_arrow")
        {

            this.position = position;
            richtingsVector = richtingsVector / speedVermindering;
            this.velocity = speed * richtingsVector;
            origin = Center;
        }

        public override void Update(GameTime gameTime)
        {
            if (active)
            {
                velocity.Y += gravity;
                angle = (float)Math.Atan2(velocity.Y, velocity.X);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Overrides de Draw methode uit SpriteGameObject, zodat de angle ook meegenomen wordt bij het tekenen van de sprite.
        /// </summary>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (deleteTimer <= 20)
            {
                spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
            }
        }

        public void arrowDeactive()
        {
            velocity = Vector2.Zero;
        }
    }
}
