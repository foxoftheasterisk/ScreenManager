using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScreenManagement
{
    /// <summary>
    /// A very basic pause screen that displays a tinted overlay.
    /// Closes when it recieves an input defined on construction.
    /// </summary>
    class PauseScreen : Screen
    {

        /// <summary>
        /// Creates a new PauseScreen with the defined parameters.
        /// </summary>
        /// <param name="_overlay">The overlay to use.  For pure tint, pass a pure white pixel.</param>
        /// <param name="_tint">The color to tint the overlay.</param>
        /// <param name="endPause">The input that will cause the screen to close.</param>
        public PauseScreen(Texture2D _overlay, Color _tint, IInputIdentifier endPause)
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
