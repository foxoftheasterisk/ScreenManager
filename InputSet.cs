using System;
using System.Collections.Generic;
using System.Text;

namespace ScreenManagement
{
    /// <summary>
    /// Represents all input for a single frame.
    /// </summary>
    public class InputSet
    {

        List<InputItem> inputs;

        public InputSet(List<InputItem> _inputs)
        {
            inputs = _inputs;
        }

        /// <summary>
        /// Returns true if there is an InputItem that matches the passed Identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public bool Has(IInputIdentifier identifier)
        {
            foreach (InputItem input in inputs)
            {
                if (identifier.Matches(input))
                    return true;
            }
            return false;
        }

        public bool IsEmpty()
        {
            return inputs.Count == 0;
        }

        /// <summary>
        /// Returns the first InputItem that matches the passed Identifier without removing it from the InputSet.
        /// Not recommended for most uses.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="identifier"></param>
        /// <returns>If a match was found</returns>
        public bool View(out InputItem match, IInputIdentifier identifier)
        {
            foreach (InputItem input in inputs)
            {
                if (identifier.Matches(input))
                {
                    match = input;
                    return true;
                }
            }
            match = null;
            return false;
        }

        /// <summary>
        /// Removes the first input item that matches the Identifier from the list and returns it, thus preventing its use on other screens.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>The first InputItem that matches, or null if none does.</returns>
        public bool Consume(out InputItem match, IInputIdentifier identifier)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                if (identifier.Matches(inputs[i]))
                {
                    InputItem item = inputs[i];
                    inputs.RemoveAt(i);
                    match = item;
                    return true;
                }
            }
            match = null;
            return false;
        }

        public bool ViewAllMatches(out List<InputItem> items, IInputIdentifier identifier)
        {
            bool foundAny = false;
            items = new List<InputItem>();
            foreach (InputItem input in inputs)
            {
                if (identifier.Matches(input))
                {
                    items.Add(input);
                    foundAny = true;
                }
            }
            return foundAny;
        }

        public bool ConsumeAllMatches(out List<InputItem> items, IInputIdentifier identifier)
        {
            bool foundAny = false;
            items = new List<InputItem>();
            for (int i = 0; i < inputs.Count; i++)
            {
                if (identifier.Matches(inputs[i]))
                {
                    items.Add(inputs[i]);
                    inputs.RemoveAt(i);
                    i--;
                    foundAny = true;
                }
            }
            return foundAny;
        }

        /// <summary>
        /// Removes the entire remaining list of inputs.
        /// </summary>
        public List<InputItem> ConsumeAll()
        {
            List<InputItem> oldInputs = inputs;
            inputs = new List<InputItem>();
            return oldInputs;
        }
    }
}
