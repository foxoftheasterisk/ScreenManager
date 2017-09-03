﻿using System;
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

        Stack<Screen> screenStack;

        private ScreenManager()
        {
            screenStack = new Stack<Screen>();
        }

        public void push(Screen screen)
        {
            screenStack.Push(screen);
        }

        public void update()
        {
            if (screenStack.Count == 0)
                return;

            foreach(Screen screen in screenStack)
            {
                bool stop;
                if (screen == screenStack.Peek())
                    stop = !screen.update(true);
                else
                    stop = !screen.update(false);
                if (stop)
                    break;
            }

            while(true)
            {
                if (!screenStack.Peek().shouldClose())
                    break;
                screenStack.Pop();
            }
        }

        public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch drawer)
        {
            Stack<Screen> drawStack = new Stack<Screen>();
            foreach(Screen screen in screenStack)
            {
                drawStack.Push(screen);
                if (!screen.drawUnder())
                    break;
            }

            while(drawStack.Count != 0)
            {
                drawer.Begin();
                drawStack.Pop().draw(drawer);
                drawer.End();
            }
        }

    }
}
