using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Boulder : Projectile
    {
        public Boulder(Vector2 position, Vector2 velocity) : base(0, "boulder", new Dictionary<string, string>(), new Dictionary<string, Animation>(), position, velocity)
        {

            addAnimation("spr_bolder_50p@4x4", "rolling", true);

            PlayAnimation("rolling");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
