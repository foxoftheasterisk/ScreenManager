using System;
using System.Collections.Generic;
using System.Text;

namespace Screens
{
    /// <summary>
    /// An interface for any class -Screen or not - that wants to capture input that lasts multiple frames.
    /// When the input starts, call ScreenManager.screenManager.RetainInput(this) to register it.
    /// Don't forget to call ScreenManager.screenManager.EndRetainedInput when it's done!
    /// </summary>
    interface IInputRetainer
    {

        void HandleRetainedInput(InputSet input);

    }
}
