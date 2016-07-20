using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Castle : SpriteGameObject
    {
        private int castleLevel = 0;
        private Dictionary<Vector2, bool> archerPositions;
        private Dictionary<Vector2, bool> catapultPositions;
        private float health = 1000;

        private int askForArchers = 0;
        private int askForCatapults = 0;

        public float Health
        {
            get { return health; }
            set { health = value; }
        }

        public Castle() : base("spr_castle")
        {
            Position = new Vector2(0, InsaneKillerArcher.Screen.Y - Height - 18);

            archerPositions = new Dictionary<Vector2, bool>();
            catapultPositions = new Dictionary<Vector2, bool>();
        }

        public int CheckForArcherSpace()
        {
            int archerSpace = 0;
            foreach(var a in archerPositions)
            {
                if (!a.Value) archerSpace++;
            }
            return archerSpace;
        }

        public int CheckForCatapultSpace()
        {
            int catapultSpace = 0;
            foreach(var c in catapultPositions)
            {
                if (!c.Value) catapultSpace++;
            }
            return catapultSpace;
        }

        public Vector2 GetNewArcherPosition()
        {
            foreach(var a in archerPositions)
            {
                if (!a.Value)
                {
                    archerPositions[a.Key] = !a.Value;
                    return a.Key;
                }
            }

            return Vector2.Zero;
        }

        public Vector2 GetNewCatapultPosition()
        {
            foreach(var c in catapultPositions)
            {
                if (!c.Value)
                {
                    catapultPositions[c.Key] = !c.Value;
                    return c.Key;
                }
            }

            return Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (health <= 0)
            {
                InsaneKillerArcher.GameStateManager.SwitchTo("gameOver");
            }

        }

        public void CheckForUpgrades()
        {
            if (castleLevel == 1)
            {
                archerPositions.Add(new Vector2(125, InsaneKillerArcher.Screen.Y - Height + 85), false);

                catapultPositions.Add(new Vector2(200, InsaneKillerArcher.Screen.Y - Height + 85), false);
            }
        }

        public int CastleLevel
        {
            get { return castleLevel; }
            set { castleLevel++; }
        }

        public int AskForArchers
        {
            get { return askForArchers; }
            set { askForArchers = value; }
        }

        public int AskForCatapults
        {
            get { return askForCatapults; }
            set { askForCatapults = value; }
        }
    }
}
