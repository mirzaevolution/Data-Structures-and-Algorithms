using System;
using System.Collections.Generic;

namespace Stack
{
    /// <summary>
    /// Stack with Linked List backing store.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class Stack<T> : IEnumerable<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();
        /// <summary>
        /// Initialize stack with default constructor.
        /// </summary>
        public Stack() { }

        /// <summary>
        /// Initialize stack with collection of items.
        /// </summary>
        /// <param name="collection">Items that support IEnumerable of T</param>
        public Stack(IEnumerable<T> collection)
        {
            if (collection!=null)
            {
                foreach (var item in collection)
                    Push(item);
            }
        }

        /// <summary>
        /// Push (store) item to the stack.
        /// </summary>
        /// <param name="item">Item to push as generic type.</param>
        public void Push(T item)
        {
            _list.AddFirst(item);
        }

        /// <summary>
        /// Peek the top level item without removing it.
        /// </summary>
        /// <returns>Top level item.</returns>
        /// <exception cref="InvalidOperationException">Stack is empty.</exception>
        public T Peek()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Stack is empty!");
            return _list.First.Value;
        }

        /// <summary>
        /// Pop (remove) top level item from stack.
        /// </summary>
        /// <returns>Top level item being removed.</returns>
        /// <exception cref="InvalidOperationException">Stack is empty.</exception>
        public T Pop()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Stack is empty!");
            T value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }

        /// <summary>
        /// Check whether or not an item exists in stack.
        /// </summary>
        /// <param name="item">Item to check as generic type.</param>
        /// <returns>True if exists, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">Stack is empty.</exception>
        public bool Contains(T item)
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Stack is empty!");
            return _list.Contains(item);
        }

        /// <summary>
        /// Clear all items.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// Copy all items in stack to an array with start index.
        /// </summary>
        /// <param name="array">Target array.</param>
        /// <param name="arrayIndex">Start index of the array.</param>
        /// <exception cref="InvalidOperationException">Stack is empty.</exception>
        /// <exception cref="IndexOutOfRangeException">Destination array's length is less than stack's count.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Stack is empty!");
            if (array.Length < _list.Count)
                throw new IndexOutOfRangeException("Destination array's length is less than stack's count.");
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the total items in stack.
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// Gets IEnumerator of T.
        /// </summary>
        /// <returns>IEnumerator of T</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
