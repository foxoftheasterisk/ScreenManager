using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace Screens
{ 
    class ScreenManager
    {
        //singleton
        private static ScreenManager mainManager = null;
        public static ScreenManager screenManager
        {
            get
            {
                if(mainManager == null)
                {
                    mainManager = new ScreenManager();
                }
                return mainManager;
            }
        }

        JengaStack<Screen> screenStack;
        //still *mostly* a logical stack,
        //but things can be removed from the middle - if they get an Update call, anyway.

        private ScreenManager()
        {
            screenStack = new JengaStack<Screen>();
        }

        public void Push(Screen screen)
        {
            screenStack.Push(screen);
        }

        public void Update(InputSet inputs)
        {
            if (screenStack.Count == 0)
                return;

            List<Screen> toClose = new List<Screen>();
            bool update = true;
            foreach(Screen screen in screenStack)
            {
                bool close;

                if (update)
                    (update, close) = screen.Update(inputs);
                else
                    close = screen.ShouldClose();

                if (close)
                    toClose.Add(screen);
            }

            foreach(Screen screen in toClose)
            {
                screen.Close();
                screenStack.Remove(screen);
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch drawer, Microsoft.Xna.Framework.Graphics.SpriteSortMode sortMode, Microsoft.Xna.Framework.Graphics.SamplerState samplerState)
        {
            Stack<Screen> drawStack = new Stack<Screen>();
            foreach(Screen screen in screenStack)
            {
                drawStack.Push(screen);
                if (!screen.DrawUnder())
                    break;
            }

            while(drawStack.Count != 0)
            {
                drawer.Begin(sortMode, null, samplerState);
                drawStack.Pop().Draw(drawer);
                drawer.End();
            }
        }

    }
}
