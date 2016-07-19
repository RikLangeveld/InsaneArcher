﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InsaneKillerArcher
{
    class PlayerWeapon : SpriteGameObject
    {
        private float angle = 0.0f;

        public PlayerWeapon(string assetname) : base(assetname)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, Color.White, angle, origin, 1.0f, SpriteEffects.None, 0);
        }

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }
    }
}
