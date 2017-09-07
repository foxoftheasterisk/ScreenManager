using System;
using System.Collections.Generic;
using System.Text;

namespace Screens
{
    //so alliterative!

    /// <summary>
    /// represents a single piece of input - say, a mouse position or click, or a gesture on a touchscreen
    /// should not have multiple pieces of input that you may want to use seperately - a mouse click with a position may be acceptable, but a set of key presses probably not
    /// </summary>
    public class InputItem
    {
        IInputIdentifier identifier;

        public bool Matches(IInputIdentifier other)
        {
            return other.Matches(identifier);
        }


    }

    /// <summary>
    /// Any piece of information that allows you to uniquely identify the input you're looking for.
    /// Examples include a Key that may be pressed, a MouseButton, or a Gesture on a touchscreen.
    /// Can also be more specific, say - a mouse click on a certain portion of the screen.
    /// MATCHES DOES NOT NEED TO BE SYMMETRICAL.
    /// </summary>
    public interface IInputIdentifier
    {
        bool Matches(IInputIdentifier other);
    }
}
