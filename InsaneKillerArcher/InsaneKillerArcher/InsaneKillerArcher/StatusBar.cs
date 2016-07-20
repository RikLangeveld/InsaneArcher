using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InsaneKillerArcher
{
    class StatusBar : GameObjectList
    {
        private SpriteGameObject background;


        public StatusBar() : base()
        {
            background = new SpriteGameObject("spr_bar");

            Add(background);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background.Sprite.Sprite, GlobalPosition, null, Color.White, 0f, background.Origin, new Vector2(1f, 2f), SpriteEffects.None, 0);
        }
    }
}
