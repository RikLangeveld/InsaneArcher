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

        public BuyableGameObject(float price, UpgradeType type) : base("spr_arrow")
        {
            this.price = price;
            this.type = type;
        }
    }
}
