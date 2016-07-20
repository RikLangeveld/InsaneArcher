using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InsaneKillerArcher
{
    class StatusBar : GameObjectList
    {
        private SpriteGameObject background;

        private ClickableSpriteGameObject overHeadArrowsIcon;
        private ClickableSpriteGameObject rollingBoulderIcon;
        private ClickableSpriteGameObject boilingOilIcon;

        private TextGameObject moneyCount;

        private GameObjectList clickableObjects;


        public StatusBar() : base()
        {
            clickableObjects = new GameObjectList();

            background = new SpriteGameObject("spr_bar");

            overHeadArrowsIcon = new ClickableSpriteGameObject("arrow_raining", IconType.OverheadArrowsIcon);
            rollingBoulderIcon = new ClickableSpriteGameObject("bolder_powerupp", IconType.RollingBoulderIcon);
            boilingOilIcon = new ClickableSpriteGameObject("spr_keuze_mage", IconType.BoilingOilIcon);

            moneyCount = new TextGameObject("GameFont");

            overHeadArrowsIcon.Position = new Vector2(975, 30);
            rollingBoulderIcon.Position = new Vector2(1075, 30);
            boilingOilIcon.Position = new Vector2(1175, 30);

            moneyCount.Position = new Vector2(800, 30);
            moneyCount.Text = "";

            clickableObjects.Add(overHeadArrowsIcon);
            clickableObjects.Add(rollingBoulderIcon);
            clickableObjects.Add(boilingOilIcon);

            Add(background);

            Add(overHeadArrowsIcon);
            Add(rollingBoulderIcon);
            Add(boilingOilIcon);
            Add(moneyCount);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if(background.Visible) spriteBatch.Draw(background.Sprite.Sprite, GlobalPosition, null, Color.White, 0f, background.Origin, new Vector2(1f, 2f), SpriteEffects.None, 0);

            if(overHeadArrowsIcon.Visible) spriteBatch.Draw(overHeadArrowsIcon.Sprite.Sprite, overHeadArrowsIcon.GlobalPosition, null, Color.White, 0f, background.Origin, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            if(rollingBoulderIcon.Visible) spriteBatch.Draw(rollingBoulderIcon.Sprite.Sprite, rollingBoulderIcon.GlobalPosition, null, Color.White, 0f, background.Origin, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            if(boilingOilIcon.Visible) spriteBatch.Draw(boilingOilIcon.Sprite.Sprite, boilingOilIcon.GlobalPosition, null, Color.White, 0f, background.Origin, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);

            moneyCount.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach(BuyableGameObject upgrade in Store.upgrades)
            {
                if (upgrade.Type == UpgradeType.OverheadArrows && upgrade.IsActive)
                {
                    overHeadArrowsIcon.SetActive();
                    upgrade.IsActive = false;
                }

                if (upgrade.Type == UpgradeType.RollingBoulder && upgrade.IsActive)
                {
                    rollingBoulderIcon.SetActive();
                    upgrade.IsActive = false;
                }

                if (upgrade.Type == UpgradeType.BoilingOil && upgrade.IsActive)
                {
                    boilingOilIcon.SetActive();
                    upgrade.IsActive = false;
                }
            }


            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            foreach(ClickableSpriteGameObject icon in clickableObjects.Objects)
            {
                if (MouseOver(inputHelper.MousePosition, icon) && inputHelper.MouseLeftButtonPressed() && icon.Clickable)
                {
                    icon.Clicked = true;
                }
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

        public GameObjectList ClickableObjects
        {
            get { return clickableObjects; }
        }

        public float MoneyCount
        {
            set
            {
                moneyCount.Text = "$" + value;
            }
        }
    }
}
