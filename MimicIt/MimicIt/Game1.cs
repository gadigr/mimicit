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

namespace MimicIt
{
    public enum GameState
    {
        Menu,
        Practice,
        TimeAttack,
        Survival,
        Exit
    }


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager   graphics;
        SpriteBatch             spriteBatch;

        public static GameState state;

        //Screens
        Menu        screenMenu;
        Survival    screenSurvival;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            graphics.IsFullScreen = true ;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Buttons
            MyGraphics.AddTexture("btn_practice", Content.Load<Texture2D>("Images/Buttons/practice"));
            MyGraphics.AddTexture("btn_close", Content.Load<Texture2D>("Images/Buttons/X"));
            MyGraphics.AddTexture("btn_TimeAttack", Content.Load<Texture2D>("Images/Buttons/TimeAttack"));
            MyGraphics.AddTexture("btn_Survival", Content.Load<Texture2D>("Images/Buttons/survival"));

            // Images
            MyGraphics.AddTexture("title", Content.Load<Texture2D>("Images/title2"));
            MyGraphics.AddTexture("g_up", Content.Load<Texture2D>("Images/up"));
            MyGraphics.AddTexture("g_down", Content.Load<Texture2D>("Images/down"));
            MyGraphics.AddTexture("g_left", Content.Load<Texture2D>("Images/left"));
            MyGraphics.AddTexture("g_right", Content.Load<Texture2D>("Images/right"));
            MyGraphics.AddTexture("g_peace", Content.Load<Texture2D>("Images/peace"));

            // Backgorund
            MyGraphics.AddTexture("mnu_back", Content.Load<Texture2D>("Images/MenuBackground"));
            MyGraphics.AddTexture("mnu_backcloud", Content.Load<Texture2D>("Images/BackCloud"));

            // Fonts
            MyFonts.AddFont("test", Content.Load<SpriteFont>("Fonts/Font"));
            MyFonts.AddFont("f_countdown", Content.Load<SpriteFont>("Fonts/fCountdown"));
            MyFonts.AddFont("f_time", Content.Load<SpriteFont>("Fonts/fTime"));
            MyFonts.AddFont("f_opac", Content.Load<SpriteFont>("Fonts/fOpac"));


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
            MyGraphics.Init(spriteBatch, graphics);
            InputUtils.Init();
            MyMouse.Init();

            // Game Vars
            state = GameState.Menu;

            // Screens
            screenMenu = new Menu();
            screenSurvival = new Survival();

            // Init Screens
            screenMenu.Init();
            screenSurvival.Init();
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            InputUtils.End();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            InputUtils.Update();
            MyMouse.Update();

            switch (state)
            {
                case GameState.Menu:
                    screenMenu.Update(gameTime);
                    break;
                case GameState.Practice:
                    break;
                case GameState.TimeAttack:
                    break;
                case GameState.Survival:
                    screenSurvival.Update(gameTime);
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (state)
            {
                case GameState.Menu:
                    screenMenu.Draw();
                    break;
                case GameState.Practice:
                    break;
                case GameState.TimeAttack:
                    break;
                case GameState.Survival:
                    screenSurvival.Draw();
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
