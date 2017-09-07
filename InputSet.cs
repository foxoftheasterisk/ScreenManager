﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Screens
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
                if (input.Matches(identifier))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the first InputItem that matches the passed Identifier without removing it from the InputSet.
        /// Not recommended for most uses.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>The first InputItem that matches, or null if none does.</returns>
        public InputItem View(IInputIdentifier identifier)
        {
            foreach (InputItem input in inputs)
            {
                if (input.Matches(identifier))
                    return input;
            }
            return null;
        }

        /// <summary>
        /// Removes the first input item that matches the Identifier from the list and returns it, thus preventing its use on other screens.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>The first InputItem that matches, or null if none does.</returns>
        public InputItem Consume(IInputIdentifier identifier)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].Matches(identifier))
                {
                    InputItem item = inputs[i];
                    inputs.RemoveAt(i);
                    return item;
                }
            }
            return null;
        }

        public List<InputItem> ViewAllMatches(IInputIdentifier identifier)
        {
            List<InputItem> items = new List<InputItem>();
            foreach (InputItem input in inputs)
            {
                if (input.Matches(identifier))
                    items.Add(input);
            }
            return items;
        }

        public List<InputItem> ConsumeAllMatches(IInputIdentifier identifier)
        {
            List<InputItem> items = new List<InputItem>();
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].Matches(identifier))
                {
                    items.Add(inputs[i]);
                    inputs.RemoveAt(i);
                    i--;
                }
            }
            return items;
        }

        /// <summary>
        /// Removes the entire remaining list of inputs.
        /// </summary>
        public void ConsumeAll()
        {
            inputs = new List<InputItem>();
        }
    }
}
