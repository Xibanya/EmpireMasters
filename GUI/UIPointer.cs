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
    public class UIPointer
    {
        public static Texture2D pointerSprite;
        public int y_cells { get; set; }
        public int x_cells { get; set; }
        private int current_tile;
        private int num_tiles;

        public UIPointer(int columns, int rows)
        {
            //This is so that pointer sheets can be of arbitrary size. No hardcoding!
            
            x_cells = columns;
            y_cells = rows;
            current_tile = 0;
            num_tiles = rows * columns;
		}

        public static void LoadContent(ContentManager contentManager)
        {
          pointerSprite = contentManager.Load<Texture2D>("Graphics/UserInterface/UI-Pointers");
        }

        public void Update(GameTime gameTime)
        {
            /*----------------------------------------------
            |	Here is where you can control update logic
            |	for mouse pointer states. They're based
            |   on tileset index for the pointer!
            |	0 = normal, 1 = pan, 2 = inspect, 3 = ???
            ----------------------------------------------*/
            current_tile = 0;

            //Holding down RMB should pan the view.
            if (Input.RightMouseDown == true)
            {
                current_tile = 1;
            }
            if (Input.KeyDown(Keys.LeftAlt) == true)
            {
                current_tile = 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 location = new Vector2(Input.MouseAbsoluteX, Input.MouseAbsoluteY);

            //This bit calculates the size of the tiles based on the number
            //of columns and rows the class initialized with.
            int width = pointerSprite.Width / x_cells;
            int height = pointerSprite.Height / y_cells;
            int row = (int)((float)current_tile / (float)y_cells);
            int column = current_tile % x_cells;

            //This part grabs the actual tile data.
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            spriteBatch.Draw(pointerSprite, destinationRectangle, sourceRectangle, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.9f);
        }
    }

}
