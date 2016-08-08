using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Nebulous
{
    public class TitleMenu : WidgetWindow
    {
        private UIPointer pointer;
        private const int POINTER_COLUMNS = 2;
        private const int POINTER_ROWS = 2;

        private Color selectColor = Color.Yellow; //this is just a placeholder for UI assets
        private Color enabledColor = Color.White;
        private Color disabledColor = Color.Gray;

        private Rectangle newGame;
        private Rectangle loadGame;
        private Rectangle options;
        private Rectangle exit;

        private int menuState = 0;
        private const int TITLE_STATE = 0;
        private const int LOAD_STATE = 1;
        private const int OPTIONS_STATE = 2;

        private static bool fullscreen = false;

        public TitleMenu()
        {
            pointer = new UIPointer(POINTER_COLUMNS, POINTER_ROWS);
            transitionState = MAIN_STATE;
            newGame = new Rectangle(GameEngine.GAME_WIDTH / 2 - 100, 290, 300, 30);
            loadGame = new Rectangle(GameEngine.GAME_WIDTH / 2 - 100, 325, 300, 30);
            options = new Rectangle(GameEngine.GAME_WIDTH / 2 - 100, 360, 300, 30);
            exit = new Rectangle(GameEngine.GAME_WIDTH / 2 - 100, 395, 300, 30);
        }

        public static new void LoadContent(ContentManager contentManager)
        {

        }

        public override void Update(GameTime gameTime)
        {
            switch (menuState)
            {
                case (TITLE_STATE):
                    if (Input.LeftMouseClicked)
                    {
                        Point clickPoint = new Point(Input.MouseAbsoluteX, Input.MouseAbsoluteY);
                        if (newGame.Contains(clickPoint))
                        {
                            GameState.PendingControlState = ControlState.GAME;
                        }
                        else if (loadGame.Contains(clickPoint))
                        {
                            menuState = LOAD_STATE;
                        }
                        else if (options.Contains(clickPoint))
                        {
                            menuState = OPTIONS_STATE;
                        }
                        else if (exit.Contains(clickPoint))
                        {
                            GameState.TerminateProgram = true;
                        }
                    }
                    break;

                case (LOAD_STATE):
                    if (Input.LeftMouseClicked)
                    {
                        if (exit.Contains(new Point(Input.MouseAbsoluteX, Input.MouseAbsoluteY)))
                        {
                            menuState = TITLE_STATE;
                        }
                    }
                    break;

                case (OPTIONS_STATE):
                    if (Input.LeftMouseClicked)
                    {
                        Point clickPoint = new Point(Input.MouseAbsoluteX, Input.MouseAbsoluteY);
                        if (newGame.Contains(clickPoint))
                        {
                        }
                        else if (loadGame.Contains(clickPoint))
                        {
                        }
                        else if (options.Contains(clickPoint))
                        {
                        }
                        else if (exit.Contains(clickPoint))
                        {
                            menuState = TITLE_STATE;
                        }
                    }
                    break;
            }
            pointer.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            pointer.Draw(spriteBatch);

            switch (menuState)
            {
                case (TITLE_STATE):
                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 300), "Big", "New Game", TextColor(newGame), 1.0f, 0.3f);
                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 335), "Big", "Load Game", TextColor(loadGame), 1.0f, 0.3f);
                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 370), "Big", "Options", TextColor(options), 1.0f, 0.3f);
                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 405), "Big", "Exit", TextColor(exit), 1.0f, 0.3f);
                    break;
                case (LOAD_STATE):

                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 405), "Big", "Back", TextColor(exit), 1.0f, 0.3f);
                    break;
                case (OPTIONS_STATE):
                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 300), "Big", FullscreenOption, TextColor(newGame), 1.0f, 0.3f);
                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 335), "Big", "Volume", TextColor(loadGame), 1.0f, 0.3f);
                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 370), "Big", "Key Bindings", TextColor(options), 1.0f, 0.3f);
                    Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, 405), "Big", "Back", TextColor(exit), 1.0f, 0.3f);
                    break;
            }
            
            base.Draw(spriteBatch);
        }

        private Color TextColor(Rectangle menuItem)
        {
            if (menuItem.Contains(new Point(Input.MouseAbsoluteX, Input.MouseAbsoluteY)))
            {
                return selectColor;
            }
            else
            {
                return enabledColor;
            }
        }

        private string FullscreenOption
        {
            get
            {
                if (fullscreen) return "Windowed";
                else return "Fullscreen";
            }
        }
    }
}
