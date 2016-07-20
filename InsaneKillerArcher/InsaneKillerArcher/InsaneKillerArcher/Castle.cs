using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Castle : GameObjectList
    {


        private int castleLevel = 0;
        private Dictionary<Vector2, bool> archerPositions;
        private Dictionary<Vector2, bool> catapultPositions;
        private Dictionary<Vector2, SpriteGameObject> catapultObjects;
        private Dictionary<Vector2, Archer> archerObjects;
        private float health = 1000;

        private int askForArchers = 0;
        private int askForCatapults = 0;

        public SpriteGameObject mainCastle;
        private SpriteGameObject castlePart1;
        private SpriteGameObject castlePart2;
        private SpriteGameObject castlePart3;
        private SpriteGameObject castlePart4;
        private SpriteGameObject castlePart5;

        public float Health
        {
            get { return health; }
            set { health = value; }
        }

        public Castle() : base()
        {
            mainCastle = new SpriteGameObject("spr_castle");
            mainCastle.Position = new Vector2(0, InsaneKillerArcher.Screen.Y - mainCastle.Height - 18);

            castlePart1 = new SpriteGameObject("spr_castle_medium_tower");
            castlePart2 = new SpriteGameObject("spr_castle_tower");

            castlePart1.Position = new Vector2(mainCastle.Position.X + 40, mainCastle.Position.Y - 115);
            castlePart2.Position = new Vector2(mainCastle.Position.X, mainCastle.Position.Y - 40);

            castlePart1.Visible = false;
            castlePart2.Visible = false;


            archerPositions = new Dictionary<Vector2, bool>();
            archerObjects = new Dictionary<Vector2, Archer>();

            catapultPositions = new Dictionary<Vector2, bool>();
            catapultObjects = new Dictionary<Vector2 , SpriteGameObject>();

            Add(castlePart2);
            Add(MakeCatapult(new Vector2(142, 950)));
            Add(castlePart1);
            Add(MakeArcher(new Vector2(91, 917)));
            Add(mainCastle);
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

        public void makeCatapultVisible(Vector2 key)
        {
            catapultObjects[key].Visible = true;
        }

        public void makeArcherVisible(Vector2 key)
        {
            archerObjects[key].Visible = true;
            archerObjects[key].body.Visible = true;
            archerObjects[key].weapon.Visible = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (health <= 0)
            {
                InsaneKillerArcher.GameStateManager.SwitchTo("gameOver");
            }

        }

        public Catapult MakeCatapult(Vector2 position)
        {
            Catapult catapult = new Catapult(position);
            catapultPositions.Add(position, false);
            catapultObjects.Add(position, catapult);
            catapult.Visible = false;
            return catapult;
        }

        public Archer MakeArcher(Vector2 position)
        {
            Archer archer = new Archer(position);
            archerPositions.Add(position, false);
            archerObjects.Add(position, archer);
            archer.Visible = false;
            archer.body.Visible = false;
            archer.weapon.Visible = false;
            return archer;
        }

        public void CheckForUpgrades()
        {
            if (castleLevel == 1)
            {
                castlePart1.Visible = true;
            }
            else if (castleLevel == 2)
            {
                castlePart2.Visible = true;
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

        public Dictionary<Vector2, SpriteGameObject> CatapultObjects
        {
            get { return catapultObjects; }
        }
    }
}
