using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace ScreenManagement
{
    class LoadingScreen : Screen
    {

        protected static SpriteFont font = null;

        public static void Display(string message)
        {
            if(activeScreen == null)
            {
                activeScreen = new LoadingScreen(message);
                ScreenManager.screenManager.Push(activeScreen);
            }
            else
            {
                activeScreen.message = message;
            }
        }
        private static LoadingScreen activeScreen = null;

        /// <summary>
        /// Changes the message displayed,
        /// but doesn't create a loading screen if one doesn't already exist.
        /// </summary>
        /// <param name="message"></param>
        public static void ChangeMessage(string message)
        {
            if (activeScreen != null)
                activeScreen.message = message;
        }

        public static void ChangeFont(SpriteFont _font)
        {
            font = _font;
        }

        public static void EndLoading()
        {
            activeScreen.hardClose = true;
        }

        private string message;

        private bool hardClose = false;

        private LoadingScreen(string _message)
        {
            message = _message;
            font = null;
        }

        public (bool updateBelow, bool shouldClose) Update(InputSet input)
        {
            input.ConsumeAll(); //generally you don't want whatever might be under a loading screen to respond to input
            return (false, hardClose);
        }

        public bool DrawUnder()
        {
            return false;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch drawer)
        {
            drawer.GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(0, 0, 200));
            //TODO: make this customizeable

            if(font != null)
                drawer.DrawString(font, message, new Microsoft.Xna.Framework.Vector2(100, 100), new Microsoft.Xna.Framework.Color(0, 0, 0));
            //TODO: center this probably
            //TODO: insert newlines if necessary?
            //TODO: probably also font color
        }

        public bool ShouldClose()
        {
            return true;
        }

        public void Close()
        {
            activeScreen = null;
        }
    }
}
