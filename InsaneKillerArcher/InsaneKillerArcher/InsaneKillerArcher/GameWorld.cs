﻿using System;
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
        private SpriteGameObject background;
        private SpriteGameObject castle;
        private EnemySpawner enemySpawner;

        private GameObjectList groundList;
        private SpriteGameObject ground;
        private Player player;


        public GameWorld()
        {
            castle = new SpriteGameObject("spr_castle");
            groundList = new GameObjectList();
            Add (new SpriteGameObject("background"));

            for (int i = 0; i < InsaneKillerArcher.Screen.X/32; i++)
            {
                ground = new SpriteGameObject("gras");
                ground.Position = new Vector2(i * ground.Width, InsaneKillerArcher.Screen.Y - ground.Height);
                groundList.Add(ground);
            }

            castle.Position = new Vector2(0, InsaneKillerArcher.Screen.Y - castle.Height - 18);

            Add(groundList);

            player = new Player();
            player.Position = new Vector2(50, InsaneKillerArcher.Screen.Y - castle.Height - player.Body.Height + 35);

            enemySpawner = new EnemySpawner(2f);

            Add(castle);
            Add(enemySpawner);
            Add(player);
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

        public override void HandleInput(InputHelper inputHelper)
        {
            float opposite = inputHelper.MousePosition.Y - player.Position.Y;
            float adjacent = inputHelper.MousePosition.X - player.Position.X;
            player.Weapon.Angle = (float)Math.Atan2(opposite, adjacent);

            base.HandleInput(inputHelper);
        }
    }
}
