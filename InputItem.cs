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
    public abstract class InputItem
    {
        public bool Matches(IInputIdentifier identifier)
        {
            return identifier.Matches(this);
        }
    }

    /// <summary>
    /// A class which allows you to find any input you're looking for, while ignoring any you're not.
    /// </summary>
    public interface IInputIdentifier
    {
        bool Matches(InputItem input);
    }
}
