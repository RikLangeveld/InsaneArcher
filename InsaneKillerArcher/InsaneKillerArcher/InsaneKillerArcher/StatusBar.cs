using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InsaneKillerArcher
{
    class StatusBar : SpriteGameObject
    {



        public StatusBar() : base("spr_bar")
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, Color.White, 0f, origin, new Vector2(1f, 2f), SpriteEffects.None, 0);
        }
    }
}
