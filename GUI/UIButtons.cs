using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nebulous
{
    public static class UIButtons
    {
        private static Texture2D buttonSprites;
        private static Rectangle[] buttonSource;

        //This is so that button sheets can be of arbitrary size.
        //The fewer places this is stored, the easier it'll be to modify it later if need be
        private const int BUTTON_ROWS = 2;
        private const int BUTTON_COLUMNS = 4;

		private static Rectangle[] buttons_down;
		private static Rectangle[] buttons_up;
		private static Rectangle[] buttons_selected;

        //This is where you set the "origin point" for the user interface.
		private const int ORIGIN_X = 32;
        private const int ORIGIN_Y = 0;

        private const int PAUSE = 0;
        private const int PROFILE = 1;
        private const int NETWORK = 2;
        private const int EMPIRE = 3;

        public static void LoadContent(ContentManager contentManager)
        {
            buttonSprites = contentManager.Load<Texture2D>("Graphics/UserInterface/UI-Buttons");

            //This part organizes the button graphics into three arrays
            /*======================================================*
            *	buttons_up is the array of graphics for unselected	*
            *	buttons, buttons_down is the array of graphics		*
            *	for selected buttons, and finally, buttons_selected	*
            *	is the array that contains the current dynamic		*
            *	state of the buttons.								*
            *=======================================================*/

            buttonSource = new Rectangle[BUTTON_COLUMNS];

            buttons_down = new Rectangle[BUTTON_COLUMNS];
            buttons_up = new Rectangle[BUTTON_COLUMNS];
            buttons_selected = new Rectangle[BUTTON_COLUMNS];

            for (int i = 0; i < BUTTON_COLUMNS; i++)
            {
                
                buttons_up[i] = new Rectangle(i * Width, 0, Width, Height);
                buttons_down[i] = new Rectangle(i * Width, 64, Width, Height);
            }

            for (int v = 0; v < BUTTON_COLUMNS; v++)
            {
                buttons_selected[v] = new Rectangle(v * Width, 0, Width, Height);
            }

            for (int i = 0; i < BUTTON_COLUMNS; i++)
            {
                buttonSource[i] = new Rectangle(i * Width + ORIGIN_X, ORIGIN_Y, Width, Height);
            }
        }

        public static void Update()
        {
            /*=======================================
             * For this, add in a control for
             * where mousing over buttons changes
             * the graphic and the pending UI state,
             * and then a state execution layer
             * for when the UI button is clicked
             * ======================================*/

                for (int i = 0; i < BUTTON_COLUMNS; i++)
                {
                    //Reset buttons
                    buttons_selected[i] = buttons_up[i];

                    //Check if mouse is over any of the buttons
                    if (Input.MouseAbsoluteX > (i * Width) + ORIGIN_X && Input.MouseAbsoluteX < (i * Width) + Width + ORIGIN_X)
                    {
                        if (Input.MouseAbsoluteY > ORIGIN_Y && Input.MouseAbsoluteY < ORIGIN_Y + Height)
                        {
                            buttons_selected[i] = buttons_down[i];
                        }
                    }
                }

                switch (GameEngine.GameWindow.State)
                {
                    case GameWindowState.PAUSE:
                        buttons_selected[PAUSE] = buttons_down[PAUSE];
                        break;
                    default:
                        break;
                }


            if (Input.LeftMouseClicked)
            {
                if (OverPause) GameEngine.GameWindow.Pause();
                else if (OverProfile)
                {

                }
                else if (OverEmpire)
                {

                }
                else if (OverNetwork)
                {

                }
            }

        }

        private static bool OverPause
        {
            get
            {
                if (Input.MouseAbsoluteX > (PAUSE * Width) + ORIGIN_X && Input.MouseAbsoluteX < (PAUSE * Width) + Width + ORIGIN_X) return true;
                else return false;
            }
        }

        private static bool OverProfile
        {
            get
            {
                if (Input.MouseAbsoluteX > (PROFILE * Width) + ORIGIN_X && Input.MouseAbsoluteX < (PROFILE * Width) + Width + ORIGIN_X) return true;
                else return false;
            }
        }

        private static bool OverEmpire
        {
            get
            {
                if (Input.MouseAbsoluteX > (EMPIRE * Width) + ORIGIN_X && Input.MouseAbsoluteX < (EMPIRE * Width) + Width + ORIGIN_X) return true;
                else return false;
            }
        }

        private static bool OverNetwork
        {
            get
            {
                if (Input.MouseAbsoluteX > (NETWORK * Width) + ORIGIN_X && Input.MouseAbsoluteX < (NETWORK * Width) + Width + ORIGIN_X) return true;
                else return false;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {

			for (int i = 0; i< BUTTON_COLUMNS; i++)
			{
                spriteBatch.Draw(buttonSprites, buttonSource[i], buttons_selected[i], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.2f);
			}
		}

        private static int Width
        {
            get
            {
                return buttonSprites.Width / BUTTON_COLUMNS;
            }
        }

        private static int Height
        {
            get
            {
                return buttonSprites.Height / BUTTON_ROWS; 
            }
        }
    }
}