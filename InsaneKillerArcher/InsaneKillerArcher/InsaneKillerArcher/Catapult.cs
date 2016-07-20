using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Catapult : AnimatedGameObject
    {
        private float damage;
        private float shootDelay;
        private float timer;
        private float reloadTime;

        private bool canShoot;

        private Dictionary<string, Animation> currentAnimations = new Dictionary<string, Animation>();
        private Dictionary<string, string> spriteNames = new Dictionary<string, string>();

        public Catapult(Vector2 position) : base()
        {
            spriteNames.Add("idle", "catepult@1x1");
            spriteNames.Add("attack", "catepult_strip3@3x1");

            currentAnimations.Add("idle", new Animation("catepult@1x1", true));
            currentAnimations.Add("attack", new Animation("catepult_strip3@3x1", false));

            foreach (var a in currentAnimations)
            {
                this.LoadAnimation(spriteNames[a.Key], a.Key, currentAnimations[a.Key].IsLooping);
            }

            damage = 100.0f;
            shootDelay = 3.0f;
            timer = 0.0f;
            reloadTime = 5.0f;

            canShoot = true;

            PlayAnimation("idle");

            base.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > shootDelay)
            {
                PlayAnimation("attack");

                if (canShoot)
                {
                    Shoot();
                    canShoot = false;
                }

                if (animations["attack"].AnimationEnded)
                {
                    if (timer > reloadTime)
                    {
                        PlayAnimation("idle");
                        timer = 0.0f;
                        canShoot = true;
                    }
                }
            }
            
            base.Update(gameTime);
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
                        float length = (gameWorld.EnemySpawner.Objects[i].Position - Position).Length();
                        if (length < x)
                        {
                            x = length;
                            y = i;
                        }
                    }

                    Vector2 distanceVector = gameWorld.EnemySpawner.Objects[y].Position - position;

                    float adjacent = distanceVector.X * 0.3f;
                    float opposite = -distanceVector.Y * 1.5f;

                    CatapultBoulder boulder = new CatapultBoulder(new Vector2(position.X, position.Y - 32), new Vector2(adjacent, opposite));
                    gameWorld.CatapultBoulders.Add(boulder);
                }
            }
        }

        public float Damage
        {
            get { return damage; }
            set { damage = value; }
        }
    }
}
