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
    public class WindowItem 
    {
        protected Vector2 location;
        protected Rectangle clickZone;
        protected bool active;

        public delegate void ItemClickDelegate();
        private ItemClickDelegate clickAction;
        private static Texture2D itemSprite;

        private float itemDepth = 0.8f;

        public WindowItem(Texture2D icon, Vector2 position, int width, int height, float depth)
        {
            itemSprite = icon;

            clickZone.X = (int)position.X;
            clickZone.Y = (int)position.Y;
            clickZone.Width = width;
            clickZone.Height = height;

            active = false;
        }

        public WindowItem(Texture2D icon, ItemClickDelegate action, Vector2 position, int width, int height, float depth)
        {
            itemSprite = icon;
            clickAction = action;

            clickZone.X = (int)position.X;
            clickZone.Y = (int)position.Y;
            clickZone.Width = width;
            clickZone.Height = height;

            active = false;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(itemSprite, clickZone, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, itemDepth);
        }

        public bool Clicked(Point clickSpot)
        {
            if (clickZone.Contains(clickSpot)) return true;
            else return false;
        }

        public Vector2 Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                clickZone.Location = new Point((int)location.X, (int)location.Y);
            }
        }

        public int Width
        {
            get
            {
                return clickZone.Width;
            }
        }

        public int Height
        {
            get
            {
                return clickZone.Height;
            }
        }
    }
}
