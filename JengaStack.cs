using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ScreenManagement
{
    /// <summary>
    /// so named because, like a stack, whatever was last added is at the front...
    /// but you can remove items from anywhere within it
    /// </summary>
    class JengaStack<T> : ICollection<T>
    {
        public JengaStack()
        {
            count = 0;
        }

        Layer<T> head;

        public int Count => count;
        private int count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            //this is probably safe to do in one line, but i don't want to risk it.
            Layer<T> top = new Layer<T>(head, item);
            head = top;
            count++;
        }

        public void Push(T item)
        {
            Add(item);
        }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            if (head is null)
                return false;
            return head.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException();
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();
            if (array.Length - arrayIndex > count)
                throw new ArgumentException("Insufficient space in array!");

            Layer<T> current = head;

            while (current != null)
            {
                array[arrayIndex] = current.item;
                current = current.nextLayer;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Descender<T>(head);
        }

        public bool Remove(T item)
        {
            if (head is null)
                return false;
            if (head.item.Equals(item))
            {
                head = head.nextLayer;
                count--;
                return true;
            }

            bool result = head.Remove(item);
            if (result is true)
                count--;
            return result;
        }

        public void Remove(IEnumerable<T> items)
        {
            foreach(T item in items)
            {
                Remove(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Descender<T>(head);
        }

        private class Layer<R>
        {
            public Layer<R> nextLayer;

            public R item;

            public Layer(Layer<R> next, R _item)
            {
                nextLayer = next;
                item = _item;
            }

            public bool Contains(R target)
            {
                if (item.Equals(target))
                    return true;
                if (nextLayer is null)
                    return false;
                return nextLayer.Contains(target);
            }

            public bool Remove(R target)
            {
                if (nextLayer is null)
                    return false;
                if(nextLayer.item.Equals(target))
                {
                    //not a confusing line at all
                    nextLayer = nextLayer.nextLayer;
                    return true;
                }
                return nextLayer.Remove(target);
            }
        }

        private class Descender<Q> : IEnumerator<Q>
        {
            public Descender(Layer<Q> head)
            {
                next = head;
            }

            Layer<Q> next;
            Q current;

            public Q Current => current;

            object IEnumerator.Current => current;

            public void Dispose()
            {
                next = null;
            }

            public bool MoveNext()
            {
                if (next == null)
                    return false;
                current = next.item;
                next = next.nextLayer;
                return true;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }
    }
}
