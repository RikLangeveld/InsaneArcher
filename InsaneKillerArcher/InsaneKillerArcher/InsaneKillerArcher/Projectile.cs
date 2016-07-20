using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneKillerArcher
{
    class Projectile : AnimatedGameObject
    {

        Dictionary<string, string> spriteNames;
        Dictionary<string, Animation> currentAnimations;

        public Projectile(int layer, string id, Dictionary<string, string> spriteNames, Dictionary<string, Animation> currentAnimations, Vector2 position, Vector2 velocity) : base(layer, id)
        {
            this.spriteNames = spriteNames;
            this.currentAnimations = currentAnimations;

            foreach (var a in currentAnimations)
            {
                this.LoadAnimation(spriteNames[a.Key], a.Key, currentAnimations[a.Key].IsLooping);
            }

            base.position = position;
            base.velocity = velocity;
        } 

        public void StartAnimation(string animationName)
        {
            this.PlayAnimation(animationName);
        }

        public void AddAnimation(string spriteName, string animationName, bool isLooping)
        {
            spriteNames.Add(animationName, spriteName);
            currentAnimations.Add(animationName, new Animation(spriteName, isLooping));

            this.LoadAnimation(spriteName, animationName, isLooping);
        }
    }
}
