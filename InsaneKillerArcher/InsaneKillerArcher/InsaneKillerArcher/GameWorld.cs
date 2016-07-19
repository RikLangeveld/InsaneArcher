using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsaneKillerArcher;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InsaneKillerArcher
{
    class GameWorld : GameObjectList
    {

        private Boolean canShoot = true; // checks if the player can shoot
        private float shootCooldown = 500f; // cooldown time for shooting
        private float shootCooldownTimer = 0f; // checks if cooldown is already done.

        private float arrowSpeed = 300;

        private EnemySpawner enemySpawner;
        private EnemySpawner zeppelinSpawner;
        private Castle castle;
        private GameObjectList groundList;
        private SpriteGameObject ground;
        private Player player;
        private GameObjectList arrows;


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

            enemySpawner = new EnemySpawner(2f, EnemySpawner.EnemyType.Enemy);
            zeppelinSpawner = new EnemySpawner(12f, EnemySpawner.EnemyType.Zeppelin);

            arrows = new GameObjectList();

            Add(castle);
            Add(enemySpawner);
            Add(zeppelinSpawner);
            Add(player);
            Add(arrows);
        }

        public override void Update(GameTime gameTime)
        {

            foreach (Enemy enemy in enemySpawner.Objects)
            {
                //Als de enemy in aanraking komt met het kasteel, gaat deze dood -> alleen voor debugging.
                if (enemy.CollidesWith(castle))
                    enemy.EnemyDead();

                if (enemy.Health == 0)
                    enemy.EnemyDead();

                //Als de enemy verwijderd moet worden, wordt de sprite onzichtbaar gemaakt. Hierna wordt deze verwijderd in de EnemySpawner class.
                if (enemy.shouldDeleteEnemy())
                    enemy.Visible = false;

                for (int i = arrows.Objects.Count-1; i > 0; i--)
                {
                    if (enemy.CollidesWith(arrows.Objects[i] as Arrow))
                    {
                        arrows.Remove(arrows.Objects[i]);
                        enemy.Health -= 50;
                    }
                }

                foreach(BuyableGameObject upgrade in Store.upgrades)
                {
                    if(upgrade.Type == UpgradeType.OverheadArrows && upgrade.IsActive)
                    {
                        int interval = 100;

                        for (int i=100; i<InsaneKillerArcher.Screen.X - 100; i += interval)
                        {
                            arrows.Add(new Arrow("spr_arrow", new Vector2(i, 0)));
                        }

                        upgrade.IsActive = false;
                    }
                }
            }

            foreach (Zeppelin zeppelin in zeppelinSpawner.Objects)
            {
                //Als de enemy in aanraking komt met het kasteel, gaat deze dood -> alleen voor debugging.
                if (zeppelin.CollidesWith(castle))
                    zeppelin.Dead();

                if (zeppelin.Health == 0)
                    zeppelin.Dead();

                for (int i = arrows.Objects.Count - 1; i > 0; i--)
                {
                    if (zeppelin.CollidesWith(arrows.Objects[i] as Arrow))
                    {
                        arrows.Remove(arrows.Objects[i]);
                        zeppelin.Health -= 50;
                    }
                }
            }

            // cooldown voor het schieten staat hier.
            if (!canShoot)
            {
                shootCooldownTimer += (float)gameTime.ElapsedGameTime.Milliseconds;

                if (shootCooldownTimer >= shootCooldown)
                {
                    shootCooldownTimer = 0;
                    canShoot = true;

                }
            }

            for (int i = gameObjects.Count - 1; i > 0; i--)
                if (!gameObjects[i].Visible)
                    Remove(gameObjects[i]);


            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            //Berekent de Angle van het wapen van de player met behulp van de positie van de muis.
            float adjacent = inputHelper.MousePosition.X - player.Position.X;
            float opposite = inputHelper.MousePosition.Y - player.Position.Y;

            //Heeft nog een restrictie nodig, max/min opposite en adjacent. Zodat de arm niet 360 graden kan draaien.
            player.Weapon.Angle = (float)Math.Atan2(opposite, adjacent);

            if (inputHelper.MouseLeftButtonDown() && canShoot)
            {
                Shoot(adjacent, opposite);
            }

            if (inputHelper.KeyPressed(Keys.P))
            {
                InsaneKillerArcher.GameStateManager.SwitchTo("store");
            }

            base.HandleInput(inputHelper);
        }


        public void Shoot(float playerPositionX, float playerPositionY)
        {
            canShoot = false;

            Vector2 direction = new Vector2(playerPositionX, playerPositionY);
            Vector2 directionNormal = Vector2.Normalize(direction);

            Arrow arrow = new Arrow("spr_arrow", player.Position, directionNormal, arrowSpeed);

            arrows.Add(arrow);
        }
    }
}
