using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Nebulous
{
    public struct Font
    {
        public string fontName;
        public SpriteFont spriteFont;
        public int fontHeight;

        public Font(ContentManager contentManager, string initialFontName, int initialFontHeight)
        {
            fontName = initialFontName;
            spriteFont = contentManager.Load<SpriteFont>("Fonts//" + initialFontName);
            fontHeight = initialFontHeight;
        }
    }
    public static class Text
    {
        private const float TEXT_LAYER = 0.3f;

        public static readonly Color SYSTEM_TEXT_COLOR = new Color(172, 50, 50);

        private static List<Font> fontList = new List<Font>();

        private static Vector2 offset = Vector2.Zero;

        public static void LoadContent(ContentManager contentManager)
        {
            fontList.Add(new Font(contentManager, "Menu", 15));
            fontList.Add(new Font(contentManager, "Big", 24));
            fontList.Add(new Font(contentManager, "Book", 14));
        }

        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string fontName, string text, Color color)
        {
            spriteBatch.DrawString(fontList.Find(x => x.fontName.Equals(fontName)).spriteFont, text, position, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, TEXT_LAYER);
        }

        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string fontName, string text, Color color, float depth)
        {
            spriteBatch.DrawString(fontList.Find(x => x.fontName.Equals(fontName)).spriteFont, text, position, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, depth);
        }

        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string fontName, string text, int row)
        {
            Font font = fontList.Find(x => x.fontName.Equals(fontName));

            offset.Y = -row * font.fontHeight;
            spriteBatch.DrawString(font.spriteFont, text, position, Color.Black, 0.0f, offset, 1.0f, SpriteEffects.None, TEXT_LAYER);
        }

        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string fontName, string text, Color color, int row)
        {
            Font font = fontList.Find(x => x.fontName.Equals(fontName));

            offset.Y = -row * font.fontHeight;
            spriteBatch.DrawString(font.spriteFont, text, position, color, 0.0f, offset, 1.0f, SpriteEffects.None, TEXT_LAYER);
        }

        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string fontName, StringBuilder text, Color color)
        {
            spriteBatch.DrawString(fontList.Find(x => x.fontName.Equals(fontName)).spriteFont, text, position, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, TEXT_LAYER);
        }

        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string fontName, StringBuilder text, int row)
        {
            Font font = fontList.Find(x => x.fontName.Equals(fontName));

            offset.Y = -row * font.fontHeight;
            spriteBatch.DrawString(font.spriteFont, text, position, Color.Black, 0.0f, offset, 1.0f, SpriteEffects.None, TEXT_LAYER);
        }

        public static void DrawText(SpriteBatch spriteBatch, Vector2 position, string fontName, StringBuilder text, Color color, int row)
        {
            Font font = fontList.Find(x => x.fontName.Equals(fontName));

            offset.Y = -row * font.fontHeight;
            spriteBatch.DrawString(font.spriteFont, text, position, color, 0.0f, offset, 1.0f, SpriteEffects.None, TEXT_LAYER);
        }

        public static void DrawCenteredText(SpriteBatch spriteBatch, Vector2 position, string fontName, string text, Color color, float scale)
        {
            Font font = fontList.Find(x => x.fontName.Equals(fontName));

            position.X -= GetStringLength(fontName, text) * scale / 2.0f;
            position.Y -= font.fontHeight * scale;
            spriteBatch.DrawString(font.spriteFont, text, position, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, TEXT_LAYER);
        }

        public static void DrawCenteredText(SpriteBatch spriteBatch, Vector2 position, string fontName, string text, Color color, float scale, float depth)
        {
            Font font = fontList.Find(x => x.fontName.Equals(fontName));

            position.X -= GetStringLength(fontName, text) * scale / 2.0f;
            position.Y -= font.fontHeight * scale;
            spriteBatch.DrawString(font.spriteFont, text, position, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, depth);
        }

        public static int GetStringLength(string fontName, string text)
        {
            return (int)fontList.Find(x => x.fontName.Equals(fontName)).spriteFont.MeasureString(text).X;
        }

        public static int GetStringHeight(string fontName)
        {
            return fontList.Find(x => x.fontName.Equals(fontName)).fontHeight;
        }
    }
}
