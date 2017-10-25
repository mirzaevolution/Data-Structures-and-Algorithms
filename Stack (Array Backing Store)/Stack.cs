using System;
using System.Collections.Generic;

namespace Stack
{
    /// <summary>
    /// Stack with Array backing store.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class Stack<T> : IEnumerable<T>
    {
        private int _count;
        private T[] _array = new T[0];

        /// <summary>
        /// Initializes stack with default constructor.
        /// </summary>
        public Stack() { }

        /// <summary>
        /// Initializes stack with collection of items.
        /// </summary>
        /// <param name="collection">Items that support IEnumerable of T</param>
        public Stack(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                    Push(item);
            }
        }

        /// <summary>
        /// Pushes (store) item the stack.
        /// </summary>
        /// <param name="item">Item to push as generic type.</param>
        public void Push(T item)
        {
            if (_count == _array.Length)
            {
                int newLength = _count == 0 ? 4 : _count * 2;
                if (_count == 0)
                {
                    _array = new T[newLength];
                }
                else
                {
                    T[] newArray = new T[newLength];
                    _array.CopyTo(newArray, 0);
                    _array = newArray;
                }
            }
            _array[_count++] = item;
        }


        /// <summary>
        /// Peeks the top level item without removing it.
        /// </summary>
        /// <returns>Top level item.</returns>
        /// <exception cref="InvalidOperationException">Stack is empty.</exception>
        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty!");
            return _array[_count - 1];
        }


        /// <summary>
        /// Pops (remove) top level item from stack.
        /// </summary>
        /// <returns>Top level item being removed.</returns>
        /// <exception cref="InvalidOperationException">Stack is empty.</exception>
        public T Pop()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty!");
            T value = _array[_count - 1];
            _array[_count - 1] = default(T);
            _count--;
            return value;
        }


        /// <summary>
        /// Checks whether or not an item exists in stack.
        /// </summary>
        /// <param name="item">Item to check as generic type.</param>
        /// <returns>True if exists, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">Stack is empty.</exception>
        public bool Contains(T item)
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty!");
            for (int i = _count - 1; i >= 0; i--)
            {
                if (_array[i].Equals(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Clears all items.
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _array = new T[0];
        }


        /// <summary>
        /// Copies all items in stack to an array with start index.
        /// </summary>
        /// <param name="array">Target array.</param>
        /// <param name="arrayIndex">Start index of the array.</param>
        /// <exception cref="InvalidOperationException">Stack is empty.</exception>
        /// <exception cref="IndexOutOfRangeException">Destination array's length is less than stack's count.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty!");
            if (array.Length < _count)
                throw new IndexOutOfRangeException("Destination array's length is less than stack's count.");
            for (int i = _count - 1; i >= 0; i--)
            {
                array[arrayIndex++] = _array[i];
            }
        }

        /// <summary>
        /// Gets the total items in stack.
        /// </summary>
        public int Count
        {
            get { return _count; }
        }


        /// <summary>
        /// Gets IEnumerator of T.
        /// </summary>
        /// <returns>IEnumerator of T</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _array[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
