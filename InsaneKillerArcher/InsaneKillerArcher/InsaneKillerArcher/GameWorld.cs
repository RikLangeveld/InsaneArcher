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

        private GameObjectList groundList;
        private SpriteGameObject ground;
        private Player player;


        public GameWorld()
        {
            castle = new SpriteGameObject("spr_castle");
            groundList = new GameObjectList();



            for (int i = 0; i < InsaneKillerArcher.Screen.X/32; i++)
            {
                ground = new SpriteGameObject("gras");
                ground.Position = new Vector2(i * ground.Width, InsaneKillerArcher.Screen.Y - ground.Height);
                groundList.Add(ground);
            }

            castle.Position = new Vector2(0, InsaneKillerArcher.Screen.Y - castle.Height - 18);

            Add(groundList);

            player = new Player();
            player.Position = new Vector2(50, InsaneKillerArcher.Screen.Y - castle.Height - player.Body.Height);

            Add(castle);
            Add(player);
        }
    }
}
