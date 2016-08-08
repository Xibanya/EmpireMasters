using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Nebulous
{
    public class GameMenu
    {
        protected WindowBox menuBox;
        protected const int MARGIN = 148; //add 148px to parameter width & height for width and height of entire menu including border
        protected const int TITLE_MARGIN = 50;
        protected const int BODY_MARGIN = 80;
        protected float depth = 0.3f;

        protected int MENU_Y = 80;
        protected int MENU_X;

        //For actual stuff, this is a good place to start
        //Vector2 position = new Vector2(MENU_X + TITLE_MARGIN, MENU_Y + BODY_MARGIN);

        protected string title;

        public GameMenu(string initialTitle, int width, int height)
        {
            title = initialTitle;
            int menuWidth = GameEngine.GAME_WIDTH - width; //set the width (this can be arbitrary)
            MENU_X = (GameEngine.GAME_WIDTH - (menuWidth + MARGIN)) / 2; //draws the menu in the center of the screen
            menuBox = new WindowBox(MENU_X, MENU_Y, menuWidth, height, depth);
            GameEngine.GameWindow.childWindowList.Add(menuBox);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Draw menu's title
            if (menuBox.Ready)
            {
                Text.DrawCenteredText(spriteBatch, new Vector2(GameEngine.GAME_WIDTH / 2, MENU_Y + TITLE_MARGIN), "Big", title, Color.White, 1.0f);
            }
        }

        public void Close()
        {
            menuBox.Close();
        }

        public bool Terminated
        {
            get
            {
                return menuBox.Terminated;
            }
        }
        protected Point MouseZone
        {
            get
            {
                return new Point(Input.MouseAbsoluteX, Input.MouseAbsoluteY);
            }
        }

        protected Color TextColor(Rectangle menuItem)
        {
            if (menuItem.Contains(MouseZone)) return Color.Yellow;
            else return Color.White;
        }
    }
}
