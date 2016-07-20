using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Enemy : AnimatedGameObject
    {
        private Vector2 startPosition = new Vector2(InsaneKillerArcher.Screen.X + 100, InsaneKillerArcher.Screen.Y - 20);
        private bool attacking = false;
        private bool attack = false;

        protected float movementSpeed = 100;
        protected float health = 100f;
        protected float attackDamage = 35f;
        protected float attackTimer = 0.5f;
       
        private Dictionary<string, Animation> currentAnimations = new Dictionary<string, Animation>();
        private Dictionary<string, string> spriteNames = new Dictionary<string, string>();

        public Enemy(string moveAnim, string deadAnim, string attackAnim) : base(0, "Enemy")
        {
            spriteNames.Add("moving", moveAnim);
            spriteNames.Add("dead", deadAnim);
            spriteNames.Add("attacking", attackAnim);

            currentAnimations.Add("moving", new Animation(moveAnim, true));
            currentAnimations.Add("dead", new Animation(deadAnim, false));
            currentAnimations.Add("attacking", new Animation(attackAnim, true));

            foreach (var a in currentAnimations)
            {
                this.LoadAnimation(spriteNames[a.Key], a.Key, currentAnimations[a.Key].IsLooping);
            }

            position = startPosition;

            Moving();
        }

        public override void Update(GameTime gameTime)
        {
            if (attacking)
                attackTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (attackTimer <= 0)
            {
                attack = true;
                attackTimer = 0.5f;
            }

            base.Update(gameTime);
        }

        public void Idle()
        {
            velocity = Vector2.Zero;
            PlayAnimation("attacking");
            attacking = true;
        }

        public void Moving()
        {
            velocity = new Vector2(-movementSpeed, 0);
            PlayAnimation("moving");
            attacking = false;
        }

        public void Dead()
        {
            velocity = Vector2.Zero;
            PlayAnimation("dead");
            attacking = false;
        }

        public bool ShouldDeleteEnemy()
        {
            return animations["dead"].AnimationEnded;
        }

        public float Health
        {
            get { return health; }
            set { health = value; }
        }

        public float AttackDamage
        {
            get { return attackDamage; }
        }

        public bool Attack
        {
            get { return attack; }
            set { attack = value; }
        }
    }
}
