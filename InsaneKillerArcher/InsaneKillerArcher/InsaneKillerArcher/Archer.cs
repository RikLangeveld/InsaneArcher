using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace InsaneKillerArcher
{
    class Archer : GameObjectList
    {
        public SpriteGameObject body;
        public SpriteGameObject weapon;

        private float angle;
        private float damage;

        private float shootDelay;
        private float timer;

        public Archer(Vector2 newPos)
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

            shootDelay = 2.0f;
            timer = 0.0f;

            position = newPos;
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
                Shoot();

                timer = 0f;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible)
            {
                body.Draw(gameTime, spriteBatch);
                spriteBatch.Draw(weapon.Sprite.Sprite, weapon.GlobalPosition, null, Color.White, angle, weapon.Origin, 1.0f, SpriteEffects.None, 0);
            }

        }

        public void Shoot()
        {
            var gameWorld = GameWorld as GameWorld;
            if (gameWorld.EnemySpawner.Objects.Count > 0)
            {
                //Shortest length
                float x = 99999;
                //index of object with shortest length
                int y = 0;

                if (Visible)
                {
                    for (int i = 0; i < gameWorld.EnemySpawner.Objects.Count; i++)
                    {
                        float length = (gameWorld.EnemySpawner.Objects[i].Position - position).Length();
                        if (length < x)
                        {
                            x = length;
                            y = i;
                        }
                    }

                    float adjacent = (gameWorld.EnemySpawner.Objects[y].Position.X - position.X) * 1.2f;
                    float opposite = -(gameWorld.EnemySpawner.Objects[y].Position.Y - position.Y);

                    float newAdjacent = GameEnvironment.Random.Next((int)(adjacent - 50), (int)(adjacent + 50));
                    float newOpposite = GameEnvironment.Random.Next((int)(opposite - 50), (int)(opposite + 50));

                    Vector2 direction = new Vector2(newAdjacent, newOpposite);
                    Vector2 directionNormal = Vector2.Normalize(direction);

                    Arrow arrow = new Arrow(position, directionNormal, 300, direction);
                    gameWorld.ArcherArrows.Add(arrow);
                }
            }
        }

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public float Damage
        {
            get { return damage; }
            set { damage = value; }
        }
    }
}
