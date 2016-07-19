using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Enemy : AnimatedGameObject
    {

        private Vector2 startPosition = new Vector2(InsaneKillerArcher.Screen.X + 20, InsaneKillerArcher.Screen.Y - 40);
        private float movementSpeed = 200;

        public Enemy() : base(0, "Enemy")
        {
            this.LoadAnimation("spr_enemy_strip2@2x1", "walkingLeft", true);
            this.LoadAnimation("spr_enemy_dead_strip5@5x1", "dead", false);
            this.LoadAnimation("spr_enemy_fight_strip2@2x1", "fighting", true);

            this.PlayAnimation("walkingLeft");

            base.position = startPosition;
            velocity = new Vector2(-movementSpeed, 0);

        }

        public override void Update(GameTime gameTime)
        {



            base.Update(gameTime);
        }
    }
}
