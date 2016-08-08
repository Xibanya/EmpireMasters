using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Nebulous
{
    public class PauseMenu : GameMenu
    {
        //keeping these here to make them easy to find and change later
        private const string TITLE = "Main Menu";
        private const int PAUSE_WIDTH = 300;
        private const int PAUSE_HEIGHT = 200;

        private int state;
        private const int MAIN_STATE = 0;
        private const int QUIT_PROMPT = 1;

        public PauseMenu() : base(TITLE, PAUSE_WIDTH, PAUSE_HEIGHT)
        {
            state = MAIN_STATE;
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.LeftMouseClicked)
            {
                switch (state)
                {
                    case MAIN_STATE:

                        if (QuitArea.Contains(MouseZone))
                        {
                            state = QUIT_PROMPT;
                        }
                        else if (ResumeArea.Contains(MouseZone))
                        {
                            GameEngine.GameWindow.Pause();
                        }

                        break;
                    case QUIT_PROMPT:
                        if (LoadArea.Contains(MouseZone)) GameState.PendingControlState = ControlState.TITLE;
                        else if (OptionsArea.Contains(MouseZone)) state = MAIN_STATE;
                        break;
                    default:
                        break;
                }
            }

            base.Update(gameTime);
     
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (menuBox.Ready)
            {
                switch (state)
                {
                    case MAIN_STATE:

                        Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + BODY_MARGIN), "Menu", "Save", TextColor(SaveArea), 1.0f);
                        Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + BODY_MARGIN + (Text.GetStringHeight("Menu") * 2)), "Menu", "Load", TextColor(LoadArea), 1.0f);
                        Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + BODY_MARGIN + (Text.GetStringHeight("Menu") * 4)), "Menu", "Options", TextColor(OptionsArea), 1.0f);
                        Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + BODY_MARGIN + (Text.GetStringHeight("Menu") * 6)), "Menu", "Quit", TextColor(QuitArea), 1.0f);
                        Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + BODY_MARGIN + (Text.GetStringHeight("Menu") * 8)), "Menu", "Resume", TextColor(ResumeArea), 1.0f);

                        break;
                    case QUIT_PROMPT:
                        Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + BODY_MARGIN), "Menu", "Are you sure you want to quit?", Color.White, 1.0f);
                        Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + BODY_MARGIN + (Text.GetStringHeight("Menu") * 2)), "Menu", "Yes", TextColor(LoadArea), 1.0f);
                        Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + BODY_MARGIN + (Text.GetStringHeight("Menu") * 4)), "Menu", "No", TextColor(OptionsArea), 1.0f);
                        break;
                    default:
                        break;
                }
            }

            base.Draw(spriteBatch);
        }

        private Rectangle SaveArea
        {
            get
            {
                int width = Text.GetStringLength("Menu", "Save");
                int height = Text.GetStringHeight("Menu");
                int x = (GameEngine.GAME_WIDTH - width) / 2;
                int y = MENU_Y + BODY_MARGIN - 8;
                return new Rectangle(x, y, width, height);
            }
        }

        private Rectangle LoadArea
        {
            get
            {
                int width = Text.GetStringLength("Menu", "Load");
                int height = Text.GetStringHeight("Menu");
                int x = (GameEngine.GAME_WIDTH - width) / 2;
                int y = MENU_Y + BODY_MARGIN + Text.GetStringHeight("Menu") * 2 - 8;
                return new Rectangle(x, y, width, height);
            }
        }
        private Rectangle OptionsArea
        {
            get
            {
                int width = Text.GetStringLength("Menu", "Options");
                int height = Text.GetStringHeight("Menu");
                int x = (GameEngine.GAME_WIDTH - width) / 2;
                int y = MENU_Y + BODY_MARGIN + Text.GetStringHeight("Menu") * 4 - 8;
                return new Rectangle(x, y, width, height);
            }
        }

        private Rectangle QuitArea
        {
            get
            {
                int width = Text.GetStringLength("Menu", "Quit");
                int height = Text.GetStringHeight("Menu");
                int x = (GameEngine.GAME_WIDTH - width) / 2;
                int y = MENU_Y + BODY_MARGIN + Text.GetStringHeight("Menu") * 6 - 8;
                return new Rectangle(x, y, width, height);
            }
        }
        private Rectangle ResumeArea
        {
            get
            {
                int width = Text.GetStringLength("Menu", "Resume");
                int height = Text.GetStringHeight("Menu");
                int x = (GameEngine.GAME_WIDTH - width) / 2;
                int y = MENU_Y + BODY_MARGIN + Text.GetStringHeight("Menu") * 8 - 8;
                return new Rectangle(x, y, width, height);
            }
        }
    }
}
