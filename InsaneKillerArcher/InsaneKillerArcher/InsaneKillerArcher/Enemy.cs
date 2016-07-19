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
        private float movementSpeed = 100;
        private float health = 100f;
        private float attackDamage = 35f;
        private float attackTimer = 0.5f;
        private bool attacking = false;
        private bool attack = false;

        private Dictionary<string, Animation> currentAnimations = new Dictionary<string, Animation>();
        private Dictionary<string, string> spriteNames = new Dictionary<string, string>();

        public Enemy() : base(0, "Enemy")
        {
            spriteNames.Add("walkingLeft", "spr_enemy_strip2@2x1");
            spriteNames.Add("dead", "spr_enemy_dead_strip5@5x1");
            spriteNames.Add("fighting", "spr_enemy_fight_strip2@2x1");

            currentAnimations.Add("walkingLeft", new Animation("spr_enemy_strip2@2x1", true));
            currentAnimations.Add("dead", new Animation("spr_enemy_dead_strip5@5x1", false));
            currentAnimations.Add("fighting", new Animation("spr_enemy_fight_strip2@2x1", true));

            foreach (var a in currentAnimations)
            {
                this.LoadAnimation(spriteNames[a.Key], a.Key, currentAnimations[a.Key].IsLooping);
            }

            position = startPosition;

            EnemyWalking();
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

        public void killEnemy()
        {
            health = 0;
        }

        public void EnemyIdle()
        {
            velocity = Vector2.Zero;
            this.PlayAnimation("fighting");
            attacking = true;
        }

        public void EnemyWalking()
        {
            velocity = new Vector2(-movementSpeed, 0);
            this.PlayAnimation("walkingLeft");
            attacking = false;
        }

        public void EnemyDead()
        {
            velocity = Vector2.Zero;
            this.PlayAnimation("dead");
            attacking = false;
        }

        public bool shouldDeleteEnemy()
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
