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

        public void EnemyIdle()
        {
            velocity = Vector2.Zero;
            this.PlayAnimation("fighting");
        }

        public void EnemyWalking()
        {
            velocity = new Vector2(-movementSpeed, 0);
            this.PlayAnimation("walkingLeft");
        }

        public void EnemyDead()
        {
            velocity = Vector2.Zero;
            this.PlayAnimation("dead");
            visible = false;
        }

        public bool shouldDeleteEnemy()
        {
            return animations["dead"].AnimationEnded;
        }
    }
}
