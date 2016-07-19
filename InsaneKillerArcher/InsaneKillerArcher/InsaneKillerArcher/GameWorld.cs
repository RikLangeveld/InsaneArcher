using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsaneKillerArcher;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace InsaneKillerArcher
{
    class GameWorld : GameObjectList
    {
        private SpriteGameObject castle;
        private EnemySpawner enemySpawner;

        public GameWorld()
        {
            castle = new SpriteGameObject("spr_castle");
            castle.Position = new Vector2( 0 , InsaneKillerArcher.Screen.Y - castle.Height);

            enemySpawner = new EnemySpawner(2f);

            Add(castle);
            Add(enemySpawner);
        }

        public override void Update(GameTime gameTime)
        {

            foreach(Enemy enemy in enemySpawner.Objects)
            {
                if (enemy.CollidesWith(castle))
                {
                    enemy.EnemyDead();
                }

                enemy.shouldDeleteEnemy();
            }

            base.Update(gameTime);
        }

    }
}
