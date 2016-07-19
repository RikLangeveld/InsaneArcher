﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace InsaneKillerArcher
{
    class Archer : GameObjectList
    {
        //locks onto enemy
        //10 px offset from enemy

        SpriteGameObject body;
        SpriteGameObject weapon;

        private float angle;
        private float damage;

        private float shootDelay;
        private float timer;

        public Archer()
        {
            body = new SpriteGameObject("spr_archer");
            body.Position = position;

            Add(body);

            weapon = new SpriteGameObject("spr_boog");
            weapon.Origin = weapon.Center;
            weapon.Position = new Vector2(position.X + 15, position.Y + 7);

            Add(weapon);

            angle = 0.0f;
            damage = 25.0f;

            shootDelay = 1.0f;
            timer = 0.0f;
        }

        /// <summary>
        /// Updates Every Frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (timer > shootDelay)
            {
                (GameWorld as GameWorld).ArcherShoot();

                timer = 0f;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            body.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(weapon.Sprite.Sprite, weapon.GlobalPosition, null, Color.White, angle, weapon.Origin, 1.0f, SpriteEffects.None, 0);
        }
        
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }
    }
}