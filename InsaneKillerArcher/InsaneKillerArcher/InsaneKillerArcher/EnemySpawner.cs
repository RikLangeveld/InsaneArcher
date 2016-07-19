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

        public EnemySpawner(float spawnTime)
        {
            this.spawnTime = spawnTime;
        }

        public override void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Console.WriteLine(currentTime);
            Console.WriteLine(spawnTime);

            if (spawnTime < currentTime)
            {
                Add(new Enemy());

                currentTime = 0f;
            }

            base.Update(gameTime);
        }

        public float SpawnTime
        {
            get { return spawnTime; }
            set { spawnTime = value; }
        }
    }
}
