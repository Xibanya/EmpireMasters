using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Nebulous
{
    public class GameEngine : Game
    {

        public const int GAME_WIDTH = 960;
        public const int GAME_HEIGHT = 540;

        private static GraphicsDeviceManager graphics = null;
        private SpriteBatch spriteBatch = null;
        private RenderTarget2D postProcessTarget = null;
        private bool fullscreen = false;

        private List<WidgetWindow> windowList = new List<WidgetWindow>();

        private static GameWindow gameWindow;

        public GameEngine()
        {           
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();

            Window.Title = "Empire Masters: Ximena";

            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }
        
        protected override void Initialize()
        {			
            Input.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            postProcessTarget = new RenderTarget2D(GraphicsDevice, GAME_WIDTH, GAME_HEIGHT);

			WidgetWindow.LoadContent(Content);
            TitleMenu.LoadContent(Content);
            GameWindow.LoadContent(Content);
            Sound.LoadContent(Content);
            UIButtons.LoadContent(Content);
            UIPointer.LoadContent(Content);
            Text.LoadContent(Content);
			
		}
        protected override void Update(GameTime gameTime)
        {
            if (GameState.TerminateProgram)
            {
                Exit();
                return;
            }

            Input.Update();
            Sound.Update();

            if (GameState.PendingControlState != ControlState.NONE)
            {
                switch (GameState.PendingControlState)
                {
                    case ControlState.TITLE:
                        if (GameState.ControlState == ControlState.NONE)
                        {
                            windowList.Add(new TitleMenu());
                            GameState.ControlState = ControlState.TITLE;
                            GameState.PendingControlState = ControlState.NONE;
                        }
                        else
                        {
                            windowList.Clear();
                            windowList.Add(new TitleMenu());
                            GameState.ControlState = ControlState.TITLE;
                            GameState.PendingControlState = ControlState.NONE;
                        }
                        break;

                    case ControlState.GAME:
                        GameState.NewGame();
                        windowList.Clear();
                        GameState.ControlState = ControlState.GAME;
                        GameState.PendingControlState = ControlState.NONE;
                        gameWindow = new GameWindow();
                        windowList.Add(gameWindow);

                        break;
                }
            }
            else
            {
                switch (GameState.ControlState)
                {
                    case ControlState.TITLE:
                        windowList[0].Update(gameTime);
                        break;

                    case ControlState.GAME:
                        gameWindow.Update(gameTime);
                        break;

                    case ControlState.NONE:
                        //some kind of error handling
                        break;
                }
            }


            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
			
			spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            foreach (WidgetWindow window in windowList) window.Draw(spriteBatch);
			spriteBatch.End();
			
			base.Draw(gameTime);
        }

        public static GameWindow GameWindow
        {
            get
            {
                return gameWindow;
            }
        }
    }
}
