using Microsoft.Xna.Framework;
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

        public Player()
        {
            body = new PlayerBody("spr_archer");
            weapon = new PlayerWeapon("spr_boog");

            body.Position = new Vector2(50, 250);
            weapon.Position = new Vector2(body.Position.X + 5, body.Position.Y + 25);

            Add(body);
            Add(weapon);
        }
    }
}
