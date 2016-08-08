using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Nebulous
{
    public class WidgetWindow
    {
        protected int transitionState = EXPANDING_STATE;
        protected int transitionTime = 0;

        public const int TRANSITION_LENGTH = 250;
        public const int EXPANDING_STATE = 0;
        public const int MAIN_STATE = 1;
        public const int COLLAPSING_STATE = 2;
        public const int TERMINATED_STATE = 3;

        protected static Texture2D boxSprite;
        protected static Rectangle[] boxSource;
        public const int MARGIN = 74;

        private const int TOP_LEFT = 0;
        private const int TOP = 1;
        private const int TOP_RIGHT = 2;
        private const int LEFT = 3;
        private const int MIDDLE = 4;
        private const int RIGHT = 5;
        private const int LOWER_LEFT = 6;
        private const int LOWER = 7;
        private const int LOWER_RIGHT = 8;

        public List<WidgetWindow> childWindowList = new List<WidgetWindow>();
        public List<WindowItem> itemList = new List<WindowItem>();

        public WidgetWindow()
        {

        }

        public static void LoadContent(ContentManager contentManager)
        {
            boxSprite = contentManager.Load<Texture2D>("Graphics/UserInterface/menu");
            boxSource = new Rectangle[9];
            boxSource[TOP_LEFT] = new Rectangle(0, 0, 74, 74);
            boxSource[TOP] = new Rectangle(74, 0, 1, 74);
            boxSource[TOP_RIGHT] = new Rectangle(75, 0, 74, 74);

            boxSource[LEFT] = new Rectangle(0, 74, 74, 1);
            boxSource[MIDDLE] = new Rectangle(74, 74, 1, 1);
            boxSource[RIGHT] = new Rectangle(75, 74, 74, 1);

            boxSource[LOWER_LEFT] = new Rectangle(0, 75, 74, 74);
            boxSource[LOWER] = new Rectangle(74, 75, 1, 74);
            boxSource[LOWER_RIGHT] = new Rectangle(75, 75, 74, 74);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (childWindowList.Count > 0)
            {
                int i = 0;
                while (i < childWindowList.Count)
                {
                    childWindowList[i].Update(gameTime);
                    if (childWindowList[i].Terminated) childWindowList.RemoveAt(i);
                    else i++;
                }
            }

            switch (transitionState)
            {
                case EXPANDING_STATE:
                    transitionTime += gameTime.ElapsedGameTime.Milliseconds;
                    if (transitionTime >= TRANSITION_LENGTH) transitionState = MAIN_STATE;
                    break;

                case MAIN_STATE:
                    for (int i = 0; i < itemList.Count; i++) itemList[i].Update(gameTime);
                    break;

                case COLLAPSING_STATE:
                    transitionTime += gameTime.ElapsedGameTime.Milliseconds;
                    if (transitionTime >= TRANSITION_LENGTH) transitionState = TERMINATED_STATE;
                    break;
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (WidgetWindow window in childWindowList)
            {
                window.Draw(spriteBatch);
            }

            if (transitionState == MAIN_STATE)
            {
                foreach (WindowItem item in itemList) item.Draw(spriteBatch);
            }
        }

        public void Close()
        {
            transitionState = COLLAPSING_STATE;
            transitionTime = 0;
        }

        public bool Ready
        {
            get
            {
                return transitionState == MAIN_STATE;
            }
        }

        public bool Terminated
        {
            get
            {
                return transitionState == TERMINATED_STATE;
            }
        }


    }
}
