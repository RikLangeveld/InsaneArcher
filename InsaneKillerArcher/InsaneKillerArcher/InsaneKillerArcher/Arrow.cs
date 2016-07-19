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
        private float gravity = 0.5f;
        private float angle = 0.0f;

        public Arrow(string assetname, Vector2 position, Vector2 directionNormal, float speed) : base(assetname)
        {
            this.position = position;
            this.velocity = directionNormal * speed;
            origin = Center;
        }

        public override void Update(GameTime gameTime)
        {
            velocity.Y += gravity;

            angle = (float)Math.Atan2(velocity.Y, velocity.X);
            base.Update(gameTime);
        }

        /// <summary>
        /// Overrides de Draw methode uit SpriteGameObject, zodat de angle ook meegenomen wordt bij het tekenen van de sprite.
        /// </summary>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
