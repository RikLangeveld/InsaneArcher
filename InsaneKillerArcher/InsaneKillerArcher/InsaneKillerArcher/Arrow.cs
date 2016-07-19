using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Arrow : SpriteGameObject
    {

        public Arrow(string assetname, Vector2 position) : base(assetname)
        {
            this.position = position;
            velocity = new Vector2(100f, 0f);
        }
    }
}
