using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace ScreenManagement
{
    public interface Screen
    {

        /// <summary>
        /// Updates the screen.
        /// </summary>
        /// <param name="input">The set of input that has not been consumed by previous screens.</param>
        /// <returns>first, if the screen below should update;
        /// second, if this screen should close.</returns>
        (bool updateBelow, bool shouldClose) Update(InputSet input);

        /// <summary>
        /// polling function to determine if screens WHICH ARE NOT BEING UPDATED should close
        /// do not hijack to update :<
        /// </summary>
        /// <returns></returns>
        bool ShouldClose();

        /// <summary>
        /// Determines whether the screen under this one should be drawn.
        /// Screens are always drawn from the bottom of the stack to the top.
        /// </summary>
        /// <returns></returns>
        bool DrawUnder();
        void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch drawer);

        /// <summary>
        /// gives the screen a chance to do whatever before closing.
        /// </summary>
        void Close();
    }
}
