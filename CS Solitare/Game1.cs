﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace CS_Solitare
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public const int WINDOW_WIDTH = 1140;
        public const int WINDOW_HEIGHT = 720;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        DeckSystem deckSystem;

        Texture2D cardSheet;
        Texture2D background;

        public static MouseState oldState;
        public static KeyboardState oldKBState;

        /// <summary>
        /// Sets resolution and cursor visibility.
        /// </summary>
        public Game1()
        {
            // Graphics
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            deckSystem = new DeckSystem();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("solitaire_bg");
            cardSheet = Content.Load<Texture2D>("card_sprite_sheet");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Escape))
                Exit();

            // Re-Initialize
            if (kbState.IsKeyDown(Keys.R) && oldKBState.IsKeyUp(Keys.R))
                Initialize();

            deckSystem.Update(gameTime);

            oldState = state;
            oldKBState = kbState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(
                    background,
                    Vector2.Zero,
                    null,
                    new Rectangle(0, 0, background.Width, background.Height),
                    Vector2.Zero,
                    0f,
                    null,
                    Color.White,
                    SpriteEffects.None,
                    1f);
            spriteBatch.End();
            deckSystem.Draw(spriteBatch, cardSheet);
            deckSystem.DrawSelected(spriteBatch, cardSheet);
            base.Draw(gameTime);
        }
    }
}
