using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{

    class EnemySpawner : GameObjectList
    {
        private int minSpawnTime;
        private int maxSpawnTime;
        private float spawnTime;
        private float currentTime = 0f;

        private Random random = new Random(); // For all your random needs :)

        /// <summary>
        /// Constructor van de EnemySpawner class
        /// </summary>
        /// <param name="spawnTime">De tijd hoelang het duurt totdat er een nieuwe enemy spawnt</param>
        public EnemySpawner(int minSpawnTime, int maxSpawnTime)
        {
            spawnTime = maxSpawnTime;

            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
        }

        /// <summary>
        /// Updates Every Frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Spawn een enemy als de timer verloopt.
            if (spawnTime < currentTime)
            {
                int enemyDropMin = 5;
                int enemyDropMax = 15;

                int trollDropMin = 10;
                int trollDropMax = 25;

                int batDropMin = 5;
                int batDropMax = 10;

                int zeppelinDropMin = 20;
                int zeppelinDropMax = 40;


                int enemyType = GameEnvironment.Random.Next(7);

                switch (enemyType)
                {
                    case 0:
                        Add(new Enemy("spr_enemy_strip2@2x1", "spr_enemy_dead_strip5@5x1", "spr_enemy_fight_strip2@2x1", 0.5f, random.Next(enemyDropMin, enemyDropMax)));
                        break;
                    case 1:
                        Add(new Troll("spr_troll_walking_strip9@9x1", "spr_troll_attacking_strip5@5x1", "spr_troll_attacking_strip5@5x1", random.Next(trollDropMin, trollDropMax)));
                        break;
                    case 2:
                        Add(new Enemy("spr_enemy_strip2@2x1", "spr_enemy_dead_strip5@5x1", "spr_enemy_fight_strip2@2x1", 0.5f, random.Next(enemyDropMin, enemyDropMax)));
                        break;
                    case 3:
                        Add(new Bat("spr_bat_flying_strip12@12x1", "spr_bat_flying_strip12@12x1", "spr_bat_flying_strip12@12x1", random.Next(batDropMin, batDropMax)));
                        break;
                    case 4:
                        Add(new Enemy("spr_enemy_strip2@2x1", "spr_enemy_dead_strip5@5x1", "spr_enemy_fight_strip2@2x1", 0.5f, random.Next(enemyDropMin, enemyDropMax)));
                        break;
                    case 5:
                        Add(new Zeppelin("spr_enemy_strip2@2x1", "spr_enemy_strip2@2x1", "spr_enemy_strip2@2x1", random.Next(zeppelinDropMin, zeppelinDropMax)));
                        break;
                    case 6:
                        Add(new Enemy("spr_enemy_strip2@2x1", "spr_enemy_dead_strip5@5x1", "spr_enemy_fight_strip2@2x1", 0.5f, random.Next(enemyDropMin, enemyDropMax)));
                        break;
                }

                spawnTime = GameEnvironment.Random.Next(minSpawnTime, maxSpawnTime);
                currentTime = 0f;
            }
            
            //Check voor elke enemy in de wereld of deze onzichtbaar is, en als dat zo is wordt deze verwijderd uit de wereld.
            for (int i = gameObjects.Count-1; i > 0; i--)
                if (!gameObjects[i].Visible)
                    Remove(gameObjects[i]);

            base.Update(gameTime);
        }

        /// <summary>
        /// Property voor de SpawnTime voor een nieuwe enemy.
        /// </summary>
        public float SpawnTime
        {
            get { return spawnTime; }
            set { spawnTime = value; }
        }
    }
}
