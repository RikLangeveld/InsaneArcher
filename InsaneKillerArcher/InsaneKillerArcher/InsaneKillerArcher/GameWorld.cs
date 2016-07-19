using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsaneKillerArcher;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace InsaneKillerArcher
{
    class GameWorld : GameObjectList
    {
        private SpriteGameObject castle;

        public GameWorld()
        {
            castle = new SpriteGameObject("spr_castle");
            castle.Position = new Vector2( 0 , InsaneKillerArcher.Screen.Y - castle.Height);

            Add(castle);
        }

        

    }
}
