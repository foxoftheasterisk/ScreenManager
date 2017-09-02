using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Screens
{
    class PauseScreen : Screen
    {
        PauseScreen(Texture2D _overlay, Color _tint)
        {
            overlay = _overlay;
            tint = _tint;
        }

        Texture2D overlay;
        Color tint;
        bool close = false;

        //pauses the game... forEVer!
        public bool update(bool useInput)
        {
            KeyboardState keys;
            try
            {
                keys = Keyboard.GetState();
            }
            catch (InvalidOperationException)
            {
                keys = new KeyboardState();//bad idea??
            }

            if (keys.IsKeyDown(Keys.Enter))
                close = true;

            return false;
        }

        public bool drawUnder()
        {
            return true;
        }

        public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch drawer)
        {
            if(overlay != null)
                drawer.Draw(overlay, drawer.GraphicsDevice.Viewport.Bounds, tint);
        }

        public bool shouldClose()
        {
            return close;
        }
    }
}
