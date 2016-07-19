using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class EnemySpawner : GameObjectList
    {
        private float spawnTime;
        private float currentTime = 0f;

        /// <summary>
        /// Constructor van de EnemySpawner class
        /// </summary>
        /// <param name="spawnTime">De tijd hoelang het duurt totdat er een nieuwe enemy spawnt</param>
        public EnemySpawner(float spawnTime)
        {
            this.spawnTime = spawnTime;
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
                Add(new Enemy());

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
