﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace InsaneKillerArcher
{
    class Enemy : AnimatedGameObject
    {
        private float baseAttackTimer;
        private float attackTimer;
        private bool attacking = false;
        private bool attack = false;
        private int moneyDrop;
        private bool isDead = false;

        protected Vector2 startPosition = new Vector2(InsaneKillerArcher.Screen.X + 100, InsaneKillerArcher.Screen.Y - 20);
        protected float movementSpeed = 100;
        protected float health = 100f;
        protected float attackDamage = 35f;
 
        private Dictionary<string, Animation> currentAnimations = new Dictionary<string, Animation>();
        private Dictionary<string, string> spriteNames = new Dictionary<string, string>();

        public Enemy(string moveAnim, string deadAnim, string attackAnim, float baseAttackTimer, int moneyDrop) : base(0, "Enemy")
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

            this.moneyDrop = moneyDrop;
            this.baseAttackTimer = baseAttackTimer;
            attackTimer = baseAttackTimer;
        }

        public override void Update(GameTime gameTime)
        {
            if (attacking)
                attackTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (attackTimer <= 0)
            {
                attack = true;
                attackTimer = baseAttackTimer;
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
            isDead = true;
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

        public float MoneyDrop
        {
            get { return moneyDrop; }
        }

        public bool IsDead
        {
            get { return isDead; }
        }
    }
}
