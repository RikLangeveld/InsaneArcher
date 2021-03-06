﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Player : GameObjectList
    {
        private PlayerBody body;
        private PlayerWeapon weapon;

        private float money = 0;

        public Player()
        {
            body = new PlayerBody("spr_archer");
            weapon = new PlayerWeapon("spr_boog");

            body.Position = new Vector2(position.X, position.Y);
            weapon.Position = new Vector2(position.X + 15, position.Y + 7);

            Add(body);
            Add(weapon);
        }

        public PlayerBody Body
        {
            get { return body; }
        }

        public PlayerWeapon Weapon
        {
            get { return weapon; }
        }

        public float Money
        {
            get { return money; }
            set { money = value; }
        }
    }
}
