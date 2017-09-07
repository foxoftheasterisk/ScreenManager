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

        /// <summary>
        ///
        /// </summary>
        /// <param name="_overlay"></param>
        /// <param name="_tint"></param>
        /// <param name="endPause"></param>
        PauseScreen(Texture2D _overlay, Color _tint, IInputIdentifier endPause)
        {
            overlay = _overlay;
            tint = _tint;
            closingInput = endPause;
        }

        //for a pure tint overlay, pass a pure white pixel
        Texture2D overlay;
        Color tint;
        IInputIdentifier closingInput;

        //pauses the game... forEVer!
        public (bool updateBelow, bool shouldClose) Update(InputSet input)
        {
            //it almost really was forever
            //but then I remembered: input identifiers can be passed in!

            bool close = false;
            if (input.Has(closingInput))
                close = true;

            return (false, close);
        }

        public bool DrawUnder()
        {
            return true;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch drawer)
        {
            if(overlay != null)
                drawer.Draw(overlay, drawer.GraphicsDevice.Viewport.Bounds, tint);
        }

        public bool ShouldClose()
        {
            return false;
        }

        public void Close()
        { }
    }
}
