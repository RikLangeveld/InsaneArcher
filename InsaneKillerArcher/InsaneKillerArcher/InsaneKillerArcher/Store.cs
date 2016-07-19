using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Store : GameObjectList
    {
        public static List<BuyableGameObject> upgrades = new List<BuyableGameObject>();

        private BuyableGameObject overheadArrows;
        private BuyableGameObject rollingBoulder;
        private BuyableGameObject boilingOil;


        public Store()
        {

            overheadArrows = new BuyableGameObject(100f, UpgradeType.OverheadArrows);
            rollingBoulder = new BuyableGameObject(100f, UpgradeType.RollingBoulder);
            boilingOil = new BuyableGameObject(100f, UpgradeType.BoilingOil);

            overheadArrows.Position = new Vector2(30, 30);
            rollingBoulder.Position = new Vector2(30, 60);
            boilingOil.Position = new Vector2(30, 90);

            upgrades.Add(overheadArrows);
            upgrades.Add(rollingBoulder);
            upgrades.Add(boilingOil);

            Add(overheadArrows);
            Add(rollingBoulder);
            Add(boilingOil);
        }

        public override void HandleInput(InputHelper inputHelper)
        {

            if(inputHelper.KeyPressed(Keys.P))
            {
                InsaneKillerArcher.GameStateManager.SwitchTo("playingState");
            }

            if(inputHelper.KeyPressed(Keys.Z))
            {
                overheadArrows.IsActive = true;
            }

            base.HandleInput(inputHelper);
        }
    }
}
