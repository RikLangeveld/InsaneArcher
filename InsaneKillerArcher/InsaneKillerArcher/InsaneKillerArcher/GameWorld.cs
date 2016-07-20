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

        private StatusBar statusBar;
        private EnemySpawner enemySpawner;
        private EnemySpawner zeppelinSpawner;
        private Castle castle;
        private GameObjectList groundList;
        private SpriteGameObject ground;
        private Player player;
        private GameObjectList catapults;
        private GameObjectList archers;
        private GameObjectList arrows;
        private GameObjectList archerArrows;
        private GameObjectList animatedProjectiles;
        private GameObjectList catapultBoulders;
        private AnimatedGameObject boulder;

        private Random random = new Random(); // for all your Random needs!. :)

        public GameWorld()
        {
            //laat bovenaan staan, is aleen de achtergrond.
            Add(new SpriteGameObject("background"));

            statusBar = new StatusBar();
            Add(statusBar);

            castle = new Castle();
            groundList = new GameObjectList();
            

            for (int i = 0; i < InsaneKillerArcher.Screen.X/32; i++)
            {
                ground = new SpriteGameObject("gras");
                ground.Position = new Vector2(i * ground.Width, InsaneKillerArcher.Screen.Y - ground.Height);
                groundList.Add(ground);
            }

            
            player = new Player();

            player.Position = new Vector2(50, InsaneKillerArcher.Screen.Y - castle.mainCastle.Height - player.Body.Height + 35);

            catapultBoulders = new GameObjectList();

            archers = new GameObjectList();
            catapults = new GameObjectList();

            Add(archers);
            Add(catapults);

            enemySpawner = new EnemySpawner(2, 5, EnemyType.Enemy);
            zeppelinSpawner = new EnemySpawner(10, 20, EnemyType.Zeppelin);

            arrows = new GameObjectList();
            archerArrows = new GameObjectList();
            animatedProjectiles = new GameObjectList();

            Add(castle);
            Add(enemySpawner);
            Add(zeppelinSpawner);
            Add(player);
            Add(catapultBoulders);
            Add(arrows);
            Add(archerArrows);
            Add(animatedProjectiles);
            Add(groundList);

        }

        public override void Update(GameTime gameTime)
        {

            foreach (Enemy enemy in enemySpawner.Objects)

            {

                if(enemy.CollidesWith(castle.mainCastle))
                {
                    if (enemy.Health > 0)
                    {
                        enemy.Idle();

                        if (enemy.GetType().Equals(typeof(Bat)))
                        {
                            enemy.Health = 0;
                        }
                    }
                }

                if (enemy.Health <= 0)
                    enemy.Dead();

                //Als de enemy verwijderd moet worden, wordt de sprite onzichtbaar gemaakt. Hierna wordt deze verwijderd in de EnemySpawner class.
                if (enemy.ShouldDeleteEnemy())
                    enemy.Visible = false;

                if (enemy.Attack)
                {
                    castle.Health -= enemy.AttackDamage;
                    enemy.Attack = false;
                }

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
                        enemy.Health -= (archerArrows.Objects[i] as Arrow).damage;
                        archerArrows.Remove(archerArrows.Objects[i]);
                    }
                }

                if (boulder != null && IsOutsideRoomRight(boulder.Position.X, -30))
                {
                    animatedProjectiles.Remove(boulder);
                    boulder.Visible = false;
                }

                if (boulder != null && boulder.CollidesWith(enemy))
                {
                    enemy.Health = 0;
                }
            }

            foreach (ClickableSpriteGameObject icon in statusBar.ClickableObjects.Objects)
            {
                if (icon.Type == IconType.OverheadArrowsIcon && icon.Clickable && icon.Clicked)
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

                    icon.SetInactive();
                }
                if (icon.Type == IconType.RollingBoulderIcon && icon.Clickable && icon.Clicked)
                {
                    int boulderStartPosX = 0;
                    int boulderStartPosY = InsaneKillerArcher.Screen.Y - 15;

                    int boulderVelX = 100;
                    int boulderVelY = 0;

                    boulder = new Boulder(new Vector2(boulderStartPosX, boulderStartPosY), new Vector2(boulderVelX, boulderVelY));

                    animatedProjectiles.Add(boulder);
                    icon.SetInactive();
                }
                if (icon.Type == IconType.BoilingOilIcon && icon.Clickable && icon.Clicked)
                {
                    // do stuff if Boiling Oil is activated.
                }

            }

            foreach (BuyableGameObject upgrade in Store.upgrades)
            {
                if (upgrade.Type == UpgradeType.CastleUpgrade && upgrade.IsActive)
                {
                    castle.CastleLevel++;

                    castle.CheckForUpgrades();

                    upgrade.IsActive = false;
                }

                if (upgrade.Type == UpgradeType.ArcherUpgrade && upgrade.IsActive)
                {
                    int archerSpace = castle.CheckForArcherSpace();
                    castle.AskForArchers += upgrade.Level;
                    if (archerSpace != 0 && castle.AskForArchers != 0)
                    {
                        for (int i = 0; i < castle.AskForArchers; i++)
                        {
                            archerSpace--;
                            Vector2 newArcherPosition = castle.GetNewArcherPosition();
                            Console.WriteLine(newArcherPosition);
                            if (newArcherPosition != Vector2.Zero)
                            {
                                castle.makeArcherVisible(newArcherPosition);
                            }
                        }
                        upgrade.IsActive = false;
                    }
                }

                if (upgrade.Type == UpgradeType.CatapultUpgrade && upgrade.IsActive)
                {
                    int catapultSpace = castle.CheckForCatapultSpace();
                    castle.AskForCatapults += upgrade.Level;
                    if (catapultSpace != 0 && castle.AskForCatapults != 0)
                    {
                        for (int i = 0; i < castle.AskForCatapults; i++)
                        {
                            catapultSpace--;
                            Vector2 newCatapultPosition = castle.GetNewCatapultPosition();
                            if (newCatapultPosition != Vector2.Zero)
                            {
                                castle.makeCatapultVisible(newCatapultPosition);
                            }
                        }
                        upgrade.IsActive = false;
                    }
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
                        zeppelin.Health -= (archerArrows.Objects[i] as Arrow).damage;
                        archerArrows.Remove(archerArrows.Objects[i]);
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

            foreach (CatapultBoulder boulder in catapultBoulders.Objects)
            {
                foreach (SpriteGameObject ground in groundList.Objects)
                {

                    if (ground.CollidesWith(boulder))
                    {
                        boulder.Bounce(ground.Position.Y);
                    }
                }

                foreach (Zeppelin zeppelin in zeppelinSpawner.Objects)
                {
                    if (boulder.CollidesWith(zeppelin))
                    {
                        zeppelin.Health -= 100;
                        boulder.Visible = false;
                    }
                }

                foreach (Enemy enemy in enemySpawner.Objects)
                {
                    if (boulder.CollidesWith(enemy))
                    {
                        enemy.Health -= 100;
                        boulder.Visible = false;
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
                //Shortest length
                float x = 99999;
                //index of object with shortest length
                int y = 0;

                Vector2 archerPosition = Vector2.Zero;

                for (int i = 0; i < enemySpawner.Objects.Count; i++)
                {
                    foreach (var archer in castle.ArcherObjects)
                    {
                        float length = (enemySpawner.Objects[i].Position - castle.ArcherObjects[archer.Key].Position).Length();
                        if (length < x)
                        {
                            x = length;
                            y = i;
                            archerPosition = castle.ArcherObjects[archer.Key].Position;
                        }
                    }
                }

                if (archerPosition != Vector2.Zero)
                {
                    float adjacent = (enemySpawner.Objects[y].Position.X - archerPosition.X) * 1.2f;
                    float opposite = -(enemySpawner.Objects[y].Position.Y - archerPosition.Y);

                    float newAdjacent = random.Next((int)(adjacent - 50), (int)(adjacent + 50));
                    float newOpposite = random.Next((int)(opposite - 50), (int)(opposite + 50));

                    Vector2 direction = new Vector2(newAdjacent, newOpposite);
                    Vector2 directionNormal = Vector2.Normalize(direction);

                    Arrow arrow = new Arrow(archerPosition, directionNormal, arrowSpeed, direction);
                    archerArrows.Add(arrow);
                }
            }
        }


        public void CatapultShoot()
        {
            if (enemySpawner.Objects.Count > 0)
            {
                //Shortest length
                float x = 99999;

                //index of object with shortest length
                int y = 0;

                Vector2 catapultPosition = Vector2.Zero;

                for (int i = 0; i < enemySpawner.Objects.Count; i++)
                {
                    foreach (var catapult in castle.CatapultObjects) { 
                        
                        float length = (enemySpawner.Objects[i].Position - castle.CatapultObjects[catapult.Key].Position).Length();
                        if (length < x)
                        {
                            x = length;
                            y = i;

                            catapultPosition = castle.CatapultObjects[catapult.Key].Position;
                        }
                    }

                    if (catapultPosition != Vector2.Zero)
                    {
                        Vector2 distanceVector = enemySpawner.Objects[y].Position - catapultPosition;

                        float adjacent = distanceVector.X * 0.3f;
                        float opposite = -distanceVector.Y * 1.5f;

                        CatapultBoulder boulder = new CatapultBoulder(new Vector2(catapultPosition.X, catapultPosition.Y - 32), new Vector2(adjacent, opposite));
                        catapultBoulders.Add(boulder);
                    }
                }
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
