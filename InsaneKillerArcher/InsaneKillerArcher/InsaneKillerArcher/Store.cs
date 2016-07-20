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

        private BuyableGameObject castleUpgrade;
        private BuyableGameObject archerUpgrade;
        private BuyableGameObject catapultUpgrade;


        public Store()
        {

            overheadArrows = new BuyableGameObject(100f, UpgradeType.OverheadArrows, "arrow_raining", new Vector2(30, 30));
            rollingBoulder = new BuyableGameObject(100f, UpgradeType.RollingBoulder, "bolder_powerupp", new Vector2(30, 235));
            boilingOil = new BuyableGameObject(100f, UpgradeType.BoilingOil, "spr_keuze_babarian", new Vector2(30, 440));

            castleUpgrade = new BuyableGameObject(100f, UpgradeType.CastleUpgrade, "spr_keuze_mage", new Vector2(235, 30));
            archerUpgrade = new BuyableGameObject(100f, UpgradeType.ArcherUpgrade, "spr_keuze_boog", new Vector2(440, 30));
            catapultUpgrade = new BuyableGameObject(100f, UpgradeType.CatapultUpgrade, "catepult@1x1", new Vector2(645, 30));

            upgrades.Add(overheadArrows);
            upgrades.Add(rollingBoulder);
            upgrades.Add(boilingOil);
            upgrades.Add(castleUpgrade);
            upgrades.Add(archerUpgrade);
            upgrades.Add(catapultUpgrade);

            Add(new SpriteGameObject("store_background"));
            Add(overheadArrows);
            Add(rollingBoulder);
            Add(boilingOil);
            Add(castleUpgrade);
            Add(archerUpgrade);
            Add(catapultUpgrade);
        }

        public override void HandleInput(InputHelper inputHelper)
        {

            if(inputHelper.KeyPressed(Keys.P))
            {
                InsaneKillerArcher.GameStateManager.SwitchTo("playingState");
            }
            
            if(mouseOver(inputHelper.MousePosition, overheadArrows) && inputHelper.MouseLeftButtonPressed()) {
                overheadArrows.IsActive = true;
            }

            if (mouseOver(inputHelper.MousePosition, rollingBoulder) && inputHelper.MouseLeftButtonPressed())
            {
                rollingBoulder.IsActive = true;
            }

            if (mouseOver(inputHelper.MousePosition, boilingOil) && inputHelper.MouseLeftButtonPressed())
            {
                boilingOil.IsActive = true;
            }

            if (mouseOver(inputHelper.MousePosition, castleUpgrade) && inputHelper.MouseLeftButtonPressed())
            {
                if (castleUpgrade.Level <= 1)
                {
                    castleUpgrade.upgradeLevel();
                }
            }

            if (mouseOver(inputHelper.MousePosition, archerUpgrade) && inputHelper.MouseLeftButtonPressed())
            {
                archerUpgrade.upgradeLevel();
            }

            if (mouseOver(inputHelper.MousePosition, catapultUpgrade) && inputHelper.MouseLeftButtonPressed())
            {
                catapultUpgrade.upgradeLevel();
            }

            base.HandleInput(inputHelper);
        }

        public bool mouseOver(Vector2 mousePosition, SpriteGameObject icon)
        {
            return mousePosition.X >= icon.Position.X
                && mousePosition.X <= icon.Position.X + icon.Width
                && mousePosition.Y >= icon.Position.Y
                && mousePosition.Y <= icon.Position.Y + icon.Height;
        }
    }
}
