using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Nebulous
{
    public enum GameWindowState
    {
        INVALID = -1, //for error handling
        MAIN = 0,
        PAUSE = 1, //1st button menu
        PROFILE = 2, //2nd button menu
        NETWORK = 3, //3rd button menu
        EMPIRE = 4, //4th button menu
        DIALOGUE = 5, //for textbox JRPG-style dialogue
        SCENE = 6 //for cut scenes
    }
    public class GameWindow : WidgetWindow
    {
        private GameWindowState state;
        private UIPointer pointer;
        private PauseMenu pauseMenu;
		static MapRenderer gameMap = new MapRenderer("Content/Graphics/Environments/Mockup.txt");

		public GameWindow()
        {
            pointer = new UIPointer(2, 2);
            state = GameWindowState.MAIN;
		}

        public static new void LoadContent(ContentManager contentManager)
        {
			gameMap.LoadContent(contentManager);
		}

        public override void Update(GameTime gameTime)
        {
            pointer.Update(gameTime);
            UIButtons.Update();


            switch (State)
            {
                case GameWindowState.MAIN:
                    break;
                case GameWindowState.PAUSE:
                    pauseMenu.Update(gameTime);
                    break;
                case GameWindowState.PROFILE:
                    break;
                case GameWindowState.NETWORK:
                    break;
                case GameWindowState.EMPIRE:
                    break;
                case GameWindowState.DIALOGUE:
                    break;
                case GameWindowState.SCENE:
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            pointer.Draw(spriteBatch);
            UIButtons.Draw(spriteBatch);
			gameMap.Draw(spriteBatch);
            base.Draw(spriteBatch);

            if (State == GameWindowState.PAUSE)
            {
                pauseMenu.Draw(spriteBatch);
            }
        }

        public void Pause()
        {
            if (State == GameWindowState.MAIN)
            {
                pauseMenu = new PauseMenu();
                state = GameWindowState.PAUSE;
            }
            else if (State == GameWindowState.PAUSE)
            {
                pauseMenu.Close();
                state = GameWindowState.MAIN;
            }
        }

        public GameWindowState State
        {
            get
            {
                return state;
            }
        }
    }
}
