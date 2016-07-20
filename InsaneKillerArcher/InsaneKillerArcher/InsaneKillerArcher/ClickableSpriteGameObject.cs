using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{

    enum IconType
    {
        OverheadArrowsIcon,
        RollingBoulderIcon,
        BoilingOilIcon
    }

    class ClickableSpriteGameObject : SpriteGameObject
    {

        private bool clickable = false;
        private bool clicked = false;
        private IconType type;

        public ClickableSpriteGameObject(string assetname, IconType type) : base(assetname)
        {
            visible = false;
            this.type = type;
        }

        public void SetActive()
        {
            clickable = true;
            visible = true;
        }

        public void SetInactive()
        {
            clickable = false;
            visible = false;
            clicked = false;
        }

        public bool Clickable
        {
            get { return clickable; }
        }

        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; }
        }

        public IconType Type
        {
            get { return type; }
        }
    }
}
