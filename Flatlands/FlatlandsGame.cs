using Flatlands.Entities.Characters;
using Flatlands.Entities.Types;
using Flatlands.Inputs;
using Flatlands.Maps;
using Flatlands.Scenes;
using Flatlands.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static Flatlands.Inputs.GameInput;

namespace Flatlands
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FlatlandsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MatchScene match;

        public static GraphicsDevice SuperGraphics;

        private static int screenWidth;
        private static int screenHeight;

        public static int ScreenWidth { get { return screenWidth; } }
        public static int ScreenHeight { get { return screenHeight; } }

        public FlatlandsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            //800 px - 50 tiles (16px tiles)
            screenWidth = GraphicsDevice.Viewport.Width;
            //480 px - 30 tiles (16px tiles)
            screenHeight = GraphicsDevice.Viewport.Height;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SuperGraphics = GraphicsDevice;

            Global.EntityAtlas = Content.Load<Texture2D>("Graphics\\entity_atlas");
            Global.MapAtlas = Content.Load<Texture2D>("Graphics\\gonzalo_map");

            SoundEffect soundTrack = Content.Load<SoundEffect>("SoundTracks\\chaesd_by_teh_rievr");
            SoundTrack.Load(soundTrack, play: true);

            match = GetMatch();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected MatchScene GetMatch()
        {
            //Gonzalo gonzalo = new Gonzalo(AttachedTo.ControllerOne);
            //Maxine maxine = new Maxine(AttachedTo.ControllerTwo);
            Joey joey = new Joey(AttachedTo.ControllerOne);
            Tania tania = new Tania(AttachedTo.ControllerTwo);

            return new MatchScene()
            {
                Map = new GonzaloMap(new List<Gunslinger>()
                {
                    joey, tania
                })
            };
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Enter))
                match = GetMatch();

            InputManager.Update(gameTime);

            match.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            match.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
