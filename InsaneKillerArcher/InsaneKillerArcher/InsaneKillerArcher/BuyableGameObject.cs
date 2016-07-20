using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{

    enum UpgradeType
    {
        OverheadArrows,
        RollingBoulder,
        BoilingOil,
        CastleUpgrade,
        ArcherUpgrade,
        CatapultUpgrade
    }

    class BuyableGameObject : SpriteGameObject
    {

        float price;
        UpgradeType type;
        bool isActive = false;
        int level = 0;

        public BuyableGameObject(float price, UpgradeType type, string spriteName, Vector2 position) : base(spriteName)
        {
            this.price = price;
            this.type = type;
            base.position = position;
        }

        public void upgradeLevel()
        {
            level++;
            isActive = true;
        }

        public float Price
        {
            get { return price; }
        }

        public UpgradeType Type
        {
            get { return type; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public int Level
        {
            get { return level; }
        }
    }
}
