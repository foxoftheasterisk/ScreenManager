using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace ScreenManagement
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

        private JengaStack<Screen> screenStack;
        //still *mostly* a logical stack,
        //but things can be removed from the middle - if they get an Update call, anyway.

        private IInputRetainer retainer;

        private ScreenManager()
        {
            screenStack = new JengaStack<Screen>();
        }

        public void Push(Screen screen)
        {
            screenStack.Push(screen);
        }

        public bool HasRetainer()
        {
            return retainer != null;
        }

        public bool IsEmpty()
        {
            return screenStack.Count == 0;
        }

        public void RetainInput(IInputRetainer _retainer)
        {
            if (retainer != null)
                throw new InvalidOperationException("Something is already retaining input!");

            retainer = _retainer;
        }

        public void EndRetainedInput(IInputRetainer _retainer)
        {
            if (_retainer != retainer)
                throw new InvalidOperationException("This is not retaining input!");

            retainer = null;
        }

        public void Update(InputSet inputs)
        {
            if (retainer != null)
                retainer.HandleRetainedInput(inputs);

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
