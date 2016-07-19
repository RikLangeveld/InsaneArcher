using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace InsaneKillerArcher
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class InsaneKillerArcher : GameEnvironment
 
    {
        protected Vector2 SCREEN_SIZE = new Vector2(
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            );

        public InsaneKillerArcher()
        {
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;

            // zet de buffer hoogte naar 1080 en breedte naar 1920
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            //maak de mouse visable in de game om te kunnen mikken
            IsMouseVisible = true;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            base.LoadContent();

            screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // TODO: use this.Content to load your game content here
            // Adds a playingstate to the game
            gameStateManager.AddGameState("playingState", new GameWorld());
            gameStateManager.AddGameState("store", new Store());

            // sets the gamestate to playing
            gameStateManager.SwitchTo("playingState");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
            Matrix.CreateScale(SCREEN_SIZE.X / 1280, SCREEN_SIZE.Y / 800, 1));
            gameStateManager.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
