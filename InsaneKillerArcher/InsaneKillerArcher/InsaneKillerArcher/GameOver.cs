﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsaneKillerArcher;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InsaneKillerArcher
{
    class GameOver : GameObjectList
    {
        public GameOver ()
        {
            Add(new SpriteGameObject("background_Gameover"));
        }
    }
}
