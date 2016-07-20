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

        private TextGameObject upgradeText;

        private BuyableGameObject castleUpgrade;
        private BuyableGameObject archerUpgrade;
        private BuyableGameObject catapultUpgrade;


        public Store()
        {

            overheadArrows = new BuyableGameObject(100f, UpgradeType.OverheadArrows, "arrow_raining", new Vector2(75, 440));
            rollingBoulder = new BuyableGameObject(100f, UpgradeType.RollingBoulder, "bolder_powerupp", new Vector2(75, 645));
            boilingOil = new BuyableGameObject(100f, UpgradeType.BoilingOil, "spr_keuze_babarian", new Vector2(75, 850));

            castleUpgrade = new BuyableGameObject(100f, UpgradeType.CastleUpgrade, "spr_keuze_mage", new Vector2(1300, 440));
            archerUpgrade = new BuyableGameObject(100f, UpgradeType.ArcherUpgrade, "spr_keuze_boog", new Vector2(1300, 645));
            catapultUpgrade = new BuyableGameObject(100f, UpgradeType.CatapultUpgrade, "catepult@1x1", new Vector2(1300, 850));

            upgradeText = new TextGameObject("GameFont");
            upgradeText.Position = new Vector2(60, 80);
            upgradeText.Text = "Spawn Arrows to rain\n on your enemies!";

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

            Add(upgradeText);
        }

        public override void HandleInput(InputHelper inputHelper)
        {

            if(inputHelper.KeyPressed(Keys.P))
            {
                InsaneKillerArcher.GameStateManager.SwitchTo("playingState");
            }
            
            if(MouseOver(inputHelper.MousePosition, overheadArrows) && inputHelper.MouseLeftButtonPressed()) {
                InsaneKillerArcher.AssetManager.PlaySound("buy_shit");
                overheadArrows.IsActive = true;
            }

            if (MouseOver(inputHelper.MousePosition, rollingBoulder) && inputHelper.MouseLeftButtonPressed())
            {
                InsaneKillerArcher.AssetManager.PlaySound("buy_shit");
                rollingBoulder.IsActive = true;
            }

            if (MouseOver(inputHelper.MousePosition, boilingOil) && inputHelper.MouseLeftButtonPressed())
            {
                InsaneKillerArcher.AssetManager.PlaySound("buy_shit");
                boilingOil.IsActive = true;
            }

            if (MouseOver(inputHelper.MousePosition, castleUpgrade) && inputHelper.MouseLeftButtonPressed())
            {
                InsaneKillerArcher.AssetManager.PlaySound("buy_shit");
                castleUpgrade.UpgradeLevel();

            }

            if (MouseOver(inputHelper.MousePosition, archerUpgrade) && inputHelper.MouseLeftButtonPressed())
            {
                InsaneKillerArcher.AssetManager.PlaySound("buy_shit");
                archerUpgrade.UpgradeLevel();
            }

            if (MouseOver(inputHelper.MousePosition, catapultUpgrade) && inputHelper.MouseLeftButtonPressed())
            {
                InsaneKillerArcher.AssetManager.PlaySound("buy_shit");
                catapultUpgrade.UpgradeLevel();
            }

            base.HandleInput(inputHelper);
        }

        public bool MouseOver(Vector2 mousePosition, SpriteGameObject icon)
        {
            return mousePosition.X >= icon.Position.X
                && mousePosition.X <= icon.Position.X + icon.Width
                && mousePosition.Y >= icon.Position.Y
                && mousePosition.Y <= icon.Position.Y + icon.Height;
        }
    }
}
