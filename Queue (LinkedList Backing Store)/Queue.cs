using System;
using System.Collections.Generic;
namespace Queue
{
    /// <summary>
    /// Queue with Linked List backing store.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class Queue<T> : IEnumerable<T>
    {

        private LinkedList<T> _list = new LinkedList<T>();

        /// <summary>
        /// Initialize queue with default constructor.
        /// </summary>
        public Queue() { }

        /// <summary>
        /// Initialize queue with collection of items.
        /// </summary>
        /// <param name="collection">Items that support IEnumerable of T</param>
        public Queue(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                    Enqueue(item);
            }
        }

        /// <summary>
        /// Enqueue (store) item the queue.
        /// </summary>
        /// <param name="item">Item to enqueue as generic type.</param>
        public void Enqueue(T item)
        {
            _list.AddLast(item);
        }

        /// <summary>
        /// Peek the top level item without removing it.
        /// </summary>
        /// <returns>Top level item.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public T Peek()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Queue is empty!");
            return _list.Last.Value;
        }

        /// <summary>
        /// Dequeue (remove) top level item from queue.
        /// </summary>
        /// <returns>Top level item being removed.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public T Dequeue()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Queue is empty!");
            T value = _list.Last.Value;
            _list.RemoveLast();
            return value;
        }

        /// <summary>
        /// Clear all items.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// Check whether or not an item exists in Queue.
        /// </summary>
        /// <param name="item">Item to check as generic type.</param>
        /// <returns>True if exists, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public bool Contains(T item)
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Queue is empty!");
            return _list.Contains(item);
        }

        /// <summary>
        /// Copy all items in queue to an array with start index.
        /// </summary>
        /// <param name="array">Target array.</param>
        /// <param name="arrayIndex">Start index of the array.</param>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        /// <exception cref="IndexOutOfRangeException">Destination array's length is less than queue's count.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {

            if (_list.Count == 0)
                throw new InvalidOperationException("Queue is empty!");
            if (array.Length < _list.Count)
                throw new IndexOutOfRangeException("Destination array's length is less than queue's count.");
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the total items in queue.
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
