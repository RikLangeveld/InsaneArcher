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
        private GameObjectList animatedProjectiles;

        private Random random = new Random(); // for all your Random needs!. :)


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
            zeppelinSpawner = new EnemySpawner(20f, EnemySpawner.EnemyType.Zeppelin);

            arrows = new GameObjectList();
            animatedProjectiles = new GameObjectList();

            Add(castle);
            Add(enemySpawner);
            Add(zeppelinSpawner);
            Add(player);
            Add(arrows);
            Add(animatedProjectiles);
        }

        public override void Update(GameTime gameTime)
        {

            foreach (Enemy enemy in enemySpawner.Objects)
            {
                float distanceToCastle = (enemy.Position - castle.Position).Length();
                if (distanceToCastle <= 300)
                    enemy.EnemyIdle();

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
                        // Tweak values;
                        int intervalXMin = 50;
                        int intervalXMax = 75;

                        int arrowDirectionXMin = 1;
                        int arrowDirectionXMax = 2;

                        int arrowDirectionYMin = 8;
                        int arrowDirectionYMax = 12;

                        int arrowSpawnYMin = -25;
                        int arrowSpawnYMax = -5;


                        int interval = random.Next(intervalXMin, intervalXMax);

                        for (int i=100; i<InsaneKillerArcher.Screen.X - 100; i += interval)
                        {
                            Vector2 normalizedArrowDirection = new Vector2(random.Next(arrowDirectionXMin, arrowDirectionXMax), random.Next(arrowDirectionYMin, arrowDirectionYMax));
                            normalizedArrowDirection.Normalize();

                            arrows.Add(new Arrow("spr_arrow", new Vector2(i, random.Next(arrowSpawnYMin, arrowSpawnYMax )), normalizedArrowDirection, 100));
                        }

                        upgrade.IsActive = false;
                    }
                    if(upgrade.Type == UpgradeType.RollingBoulder && upgrade.IsActive)
                    {
                        int boulderStartPosX = 0;
                        int boulderStartPosY = InsaneKillerArcher.Screen.Y - 20;

                        int boulderVelX = 30;
                        int boulderVelY = 0;

                        animatedProjectiles.Add(new Boulder(new Vector2(boulderStartPosX, boulderStartPosY), new Vector2(boulderVelX, boulderVelY)));
                        upgrade.IsActive = false;

                        
                    }
                    if(upgrade.Type == UpgradeType.BoilingOil && upgrade.IsActive)
                    {
                        // do stuff if Boiling Oil is activated.
                    }
                }
            }

            foreach (Zeppelin zeppelin in zeppelinSpawner.Objects)
            {
                float distanceToCastle = (zeppelin.Position - castle.Position).Length();
                if (distanceToCastle <= 400 || zeppelin.Position.X < 200)
                    zeppelin.Idle();

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
