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
                    (GameWorld as GameWorld).CatapultShoot();
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

        public float Damage
        {
            get { return damage; }
            set { damage = value; }
        }
    }
}
