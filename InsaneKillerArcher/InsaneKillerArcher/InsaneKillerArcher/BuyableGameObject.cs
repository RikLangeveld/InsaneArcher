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
        BoilingOil
    }

    class BuyableGameObject : SpriteGameObject
    {

        float price;
        UpgradeType type;
        bool isActive = false;

        public BuyableGameObject(float price, UpgradeType type) : base("spr_arrow@1x1")
        {
            this.price = price;
            this.type = type;
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
    }
}
