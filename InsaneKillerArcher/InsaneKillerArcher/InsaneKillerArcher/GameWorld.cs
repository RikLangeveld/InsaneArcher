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

        private EnemySpawner enemySpawner;
        private Castle castle;
        private GameObjectList groundList;
        private SpriteGameObject ground;
        private Player player;

        private Arrow arrow;


        public GameWorld()
        {
            //laat bovenaan staan, is aleen de achtergrond.
            Add(new SpriteGameObject("background"));
            Add(new SpriteGameObject("spr_bar"));

            castle = new Castle();
            groundList = new GameObjectList();
            

            for (int i = 0; i < InsaneKillerArcher.Screen.X/32; i++)
            {
                ground = new SpriteGameObject("gras");
                ground.Position = new Vector2(i * ground.Width, InsaneKillerArcher.Screen.Y - ground.Height);
                groundList.Add(ground);
            }


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
                //Als de enemy in aanraking komt met het kasteel, gaat deze dood -> alleen voor debugging.
                if (enemy.CollidesWith(castle))
                    enemy.EnemyDead();
                
                //Als de enemy verwijderd moet worden, wordt de sprite onzichtbaar gemaakt. Hierna wordt deze verwijderd in de EnemySpawner class.
                if (enemy.shouldDeleteEnemy())
                    enemy.Visible = false;

                foreach (Arrow arrow in gameObjects)
                {
                    if (enemy.CollidesWith(arrow))
                    {
                        enemy.Health -= 50;
                        arrow.Visible = false;
                    }
                }

                for (int i = gameObjects.Count-1; i > 0; i--)
                    if (typeof(Arrow).Equals(gameObjects[i]) && !gameObjects[i].Visible)
                        Remove(gameObjects[i]);
            }

            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            //Berekent de Angle van het wapen van de player met behulp van de positie van de muis.
            float opposite = inputHelper.MousePosition.Y - player.Position.Y;
            float adjacent = inputHelper.MousePosition.X - player.Position.X;

            //Heeft nog een restrictie nodig, max/min opposite en adjacent. Zodat de arm niet 360 graden kan draaien.
            player.Weapon.Angle = (float)Math.Atan2(opposite, adjacent);

            if (inputHelper.MouseLeftButtonPressed())
            {
                arrow = new Arrow("spr_arrow", player.Position);
                Add(arrow);
            }

            base.HandleInput(inputHelper);
        }

        public void Shoot()
        {
            Arrow arrow = new Arrow("arrow_projectile", player.Position);
        }
    }
}
