using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Nebulous
{
    public class WindowBox : WidgetWindow
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public float depth;

        private static readonly Color WINDOW_COLOR = new Color(1.0f, 1.0f, 1.0f, 0.75f);

        public WindowBox(int initialX, int initialY, int initialWidth, int initialHeight, float initialDepth = 0.9f)
        {
            x = initialX;
            y = initialY;
            width = initialWidth;
            height = initialHeight;
            depth = initialDepth;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float transitionProgress;
            int transitionX, transitionY, transitionWidth, transitionHeight;

                switch (transitionState)
                {
                    case EXPANDING_STATE:
                        transitionProgress = (float)transitionTime / TRANSITION_LENGTH;
                        transitionWidth = (int)(width * transitionProgress);
                        transitionHeight = (int)(height * transitionProgress);
                        transitionX = x + (width - transitionWidth);
                        transitionY = y + (height - transitionHeight);

                        DrawTextbox(spriteBatch, transitionX, transitionY, transitionWidth, transitionHeight, depth);

                        break;

                    case MAIN_STATE:
                        DrawTextbox(spriteBatch, x, y, width, height, depth);

                        break;

                    case COLLAPSING_STATE:
                        transitionProgress = (float)(TRANSITION_LENGTH - transitionTime) / TRANSITION_LENGTH;
                        transitionWidth = (int)(width * transitionProgress);
                        transitionHeight = (int)(height * transitionProgress);
                        transitionX = x + (width - transitionWidth);
                        transitionY = y + (height - transitionHeight);

                        DrawTextbox(spriteBatch, transitionX, transitionY, transitionWidth, transitionHeight, depth);

                        break;
                }

                base.Draw(spriteBatch);
        }

        public void DrawTextbox(SpriteBatch spriteBatch, int x, int y, int width, int height, float depth)
        {
            Vector2 position = new Vector2(x, y);

            //spriteBatch.Draw(boxSprite, new Rectangle(x + MARGIN / 2, y + MARGIN, (width + 1) * MARGIN, (height + 1) * MARGIN), boxSource[4], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, depth);

            //First row

            //Top left corner
            spriteBatch.Draw(boxSprite, position, boxSource[0], Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, depth - 0.01f);
            position.X += MARGIN;

            //Top middle
            Rectangle topMiddle = new Rectangle((int)position.X, (int)position.Y, width, boxSource[1].Height);
            spriteBatch.Draw(boxSprite, topMiddle, boxSource[1], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, depth - 0.01f);

            //Top right corner
            position.X += width;
            spriteBatch.Draw(boxSprite, position, boxSource[2], Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, depth - 0.01f);

            position.X = x;
            position.Y += MARGIN;

            //2nd row
                Rectangle leftMiddle = new Rectangle((int)position.X, (int)position.Y, boxSource[3].Width, height);
                spriteBatch.Draw(boxSprite, leftMiddle, boxSource[3], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, depth - 0.01f);
                position.X += MARGIN;

                Rectangle middle = new Rectangle((int)position.X, (int)position.Y, width, height);
                spriteBatch.Draw(boxSprite, middle, boxSource[4], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, depth - 0.01f);

                position.X += width;

                Rectangle rightMiddle = new Rectangle((int)position.X, (int)position.Y, boxSource[5].Width, height);
                spriteBatch.Draw(boxSprite, rightMiddle, boxSource[5], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, depth - 0.01f);

            position.X = x;
            position.Y += height;
            //3rd row
                spriteBatch.Draw(boxSprite, position, boxSource[6], Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, depth - 0.01f);
                position.X += MARGIN;

                Rectangle bottomMiddle = new Rectangle((int)position.X, (int)position.Y, width, boxSource[7].Height);
                spriteBatch.Draw(boxSprite, bottomMiddle, boxSource[7], Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, depth - 0.01f);

                position.X += width;
                spriteBatch.Draw(boxSprite, position, boxSource[8], Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, depth - 0.01f);
        }
    }
}
