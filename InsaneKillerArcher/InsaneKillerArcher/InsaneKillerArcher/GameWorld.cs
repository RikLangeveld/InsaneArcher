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
        private Archer archer;
        private GameObjectList arrows;
        private GameObjectList archerArrows;
        private GameObjectList animatedProjectiles;
        private AnimatedGameObject boulder;

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

            
            player = new Player();
            player.Position = new Vector2(50, InsaneKillerArcher.Screen.Y - castle.Height - player.Body.Height + 35);

            archer = new Archer();
            archer.Position = new Vector2(125, InsaneKillerArcher.Screen.Y - castle.Height + 85);

            Add(archer);

            enemySpawner = new EnemySpawner(2f, EnemySpawner.EnemyType.Enemy);
            zeppelinSpawner = new EnemySpawner(20f, EnemySpawner.EnemyType.Zeppelin);

            arrows = new GameObjectList();
            archerArrows = new GameObjectList();
            animatedProjectiles = new GameObjectList();

            Add(castle);
            Add(enemySpawner);
            Add(zeppelinSpawner);
            Add(player);
            Add(arrows);
            Add(archerArrows);
            Add(animatedProjectiles);
            Add(groundList);

        }

        public override void Update(GameTime gameTime)
        {

            foreach (Enemy enemy in enemySpawner.Objects)
            {

                float distanceToCastle = (enemy.Position - castle.Position).Length();
                if (distanceToCastle <= 300)
                    if (enemy.Health > 0)
                        enemy.EnemyIdle();

                if (enemy.Health <= 0)
                    enemy.EnemyDead();

                //Als de enemy verwijderd moet worden, wordt de sprite onzichtbaar gemaakt. Hierna wordt deze verwijderd in de EnemySpawner class.
                if (enemy.shouldDeleteEnemy())
                    enemy.Visible = false;

                for (int i = arrows.Objects.Count-1; i > 0; i--)
                {
                    if (enemy.CollidesWith(arrows.Objects[i] as Arrow))
                    {
                        arrows.Remove(arrows.Objects[i]);
                        enemy.Health -= player.Weapon.Damage;
                    }
                }

                for (int i = archerArrows.Objects.Count - 1; i > 0; i--)
                {
                    if (enemy.CollidesWith(archerArrows.Objects[i] as Arrow))
                    {
                        archerArrows.Remove(archerArrows.Objects[i]);
                        enemy.Health -= archer.Damage;
                    }
                }

                if (boulder != null && IsOutsideRoomRight(boulder.Position.X, boulder.Width))
                {
                    animatedProjectiles.Remove(boulder);
                }

                if (boulder != null && boulder.CollidesWith(enemy))
                {
                    enemy.Health = 0;
                }
            }

            foreach (BuyableGameObject upgrade in Store.upgrades)
            {
                if (upgrade.Type == UpgradeType.OverheadArrows && upgrade.IsActive)
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

                    for (int i = 100; i < InsaneKillerArcher.Screen.X - 100; i += interval)
                    {
                        Vector2 normalizedArrowDirection = new Vector2(random.Next(arrowDirectionXMin, arrowDirectionXMax), random.Next(arrowDirectionYMin, arrowDirectionYMax));
                        normalizedArrowDirection.Normalize();

                        arrows.Add(new Arrow(new Vector2(i, random.Next(arrowSpawnYMin, arrowSpawnYMax)), normalizedArrowDirection, 100));
                    }

                    upgrade.IsActive = false;
                }
                if (upgrade.Type == UpgradeType.RollingBoulder && upgrade.IsActive)
                {
                    int boulderStartPosX = 0;
                    int boulderStartPosY = InsaneKillerArcher.Screen.Y - 20;

                    int boulderVelX = 100;
                    int boulderVelY = 0;

                    boulder = new Boulder(new Vector2(boulderStartPosX, boulderStartPosY), new Vector2(boulderVelX, boulderVelY));

                    animatedProjectiles.Add(boulder);
                    upgrade.IsActive = false;
                }
                if (upgrade.Type == UpgradeType.BoilingOil && upgrade.IsActive)
                {
                    // do stuff if Boiling Oil is activated.
                }
            }

            foreach (Zeppelin zeppelin in zeppelinSpawner.Objects)
            {
                float distanceToCastle = (zeppelin.Position - castle.Position).Length();
                if (distanceToCastle <= 400 || zeppelin.Position.X < 200)
                    if (zeppelin.Health > 0)
                        zeppelin.Idle();

                if (zeppelin.Health <= 0)
                    zeppelin.Dead();

                for (int i = arrows.Objects.Count - 1; i > 0; i--)
                {
                    if (zeppelin.CollidesWith(arrows.Objects[i] as Arrow))
                    {
                        arrows.Remove(arrows.Objects[i]);
                        zeppelin.Health -= player.Weapon.Damage;
                    }
                }

                for (int i = archerArrows.Objects.Count - 1; i > 0; i--)
                {
                    if (zeppelin.CollidesWith(archerArrows.Objects[i] as Arrow))
                    {
                        archerArrows.Remove(archerArrows.Objects[i]);
                        zeppelin.Health -= archer.Damage;
                    }
                }
            }

            foreach (Arrow arrow in arrows.Objects)
            {
                if (IsOutsideRoomLeft(arrow.Position.X, arrow.Width) || IsOutsideRoomRight(arrow.Position.X, arrow.Width))
                {
                    arrow.Visible = false;
                }

                foreach (SpriteGameObject ground in groundList.Objects)
                {
                    if (arrow.CollidesWith(ground))
                    {
                        if (arrow.Active)
                        {

                            arrow.Velocity = Vector2.Zero;
                            arrow.Gravity = 0;

                            arrow.Active = false;
                        }
                        else if (arrow.deleteTimer > 120)
                        {
                            arrow.Visible = false;
                        }
                        else
                        {
                            arrow.deleteTimer ++;
                        }
                        
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

            Arrow arrow = new Arrow( player.Position, directionNormal, arrowSpeed, direction);

            arrows.Add(arrow);
        }

        public void ArcherShoot()
        {
            if (enemySpawner.Objects.Count > 0)
            {
                float adjacent = (enemySpawner.Objects[0].Position.X - archer.Position.X);
                float opposite = (enemySpawner.Objects[0].Position.Y - archer.Position.Y);

                Vector2 direction = new Vector2(adjacent, opposite);
                Vector2 directionNormal = Vector2.Normalize(direction);

                Arrow arrow = new Arrow("spr_arrow", archer.Position, directionNormal, arrowSpeed, direction);

                archerArrows.Add(arrow);
            }
        }

        /// <summary>
        /// A function to check if a spriteGameobject is Outside The room
        /// </summary>
        /// <param name="position">The position of the game object</param>
        /// <param name="width"> The width of the SpriteObject</param>
        /// <param name="height">The heigth of the SpriteObject</param>
        /// <returns></returns>
        public bool IsOutsideRoom(Vector2 position, int width, int height)
        {

            if (position.X > 0 && position.X + width < InsaneKillerArcher.Screen.X &&
                position.Y > 0 && position.Y + height < InsaneKillerArcher.Screen.Y)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Used to check if a object is out of the room on the right side
        /// </summary>
        /// <param name="position">position of the object</param>
        /// <param name="width">width of the object</param>
        /// <returns></returns>
        public bool IsOutsideRoomRight(float positionX, int width)
        {
            if (positionX + width / 2 > 1920)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Used to check if a object is out of the room on the left side
        /// </summary>
        /// <param name="position">position of the object</param>
        /// <param name="width">width of the object</param>
        /// <returns></returns>
        public bool IsOutsideRoomLeft(float positionX, int width)
        {
            if (positionX - width / 2 < 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Used to check if a object is below the view.
        /// </summary>
        /// <param name="position">position of the object</param>
        /// <param name="width">width of the object</param>
        /// <returns></returns>
        public bool IsOutsideRoomBelow(float positionY, int height)
        {
            if (positionY > 1080)
                return true;
            else
                return false;
        }

        public EnemySpawner EnemySpawner
        {
            get { return enemySpawner; }
        }
    }
}
