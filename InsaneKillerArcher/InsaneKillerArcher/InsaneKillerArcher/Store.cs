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

            overheadArrows = new BuyableGameObject(100f, UpgradeType.OverheadArrows, "arrow_raining", new Vector2(30, 30));
            rollingBoulder = new BuyableGameObject(100f, UpgradeType.RollingBoulder, "spr_keuze_mage", new Vector2(30, 235));
            boilingOil = new BuyableGameObject(100f, UpgradeType.BoilingOil, "spr_keuze_babarian", new Vector2(30, 440));

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
            /*
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
            */

            foreach(var u in upgrades)
            {
                if(mouseOver(inputHelper.MousePosition, u) && inputHelper.MouseLeftButtonPressed())
                {
                    u.IsActive = true;
                }
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
