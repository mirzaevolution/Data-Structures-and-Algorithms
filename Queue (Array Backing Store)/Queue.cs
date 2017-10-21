using System;
using System.Collections.Generic;
namespace Queue
{
    /// <summary>
    /// Queue with Array backing store.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        private int _count;
        private T[] _array = new T[0];

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
        /// Peek the top level item without removing it.
        /// </summary>
        /// <returns>Top level item.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty!");
            return _array[0];
        }

        /// <summary>
        /// Dequeue (remove) top level item from queue.
        /// </summary>
        /// <returns>Top level item being removed.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty!");
            T value = _array[0];

            if (_count == 1)
            {
                _array = new T[0];
            }
            else
            {
                T[] newArray = new T[_array.Length - 1];
                for (int i = 1; i < _count; i++)
                {
                    newArray[i - 1] = _array[i];
                }
                _array = newArray;
            }
            _count--;
            return value;
        }

        /// <summary>
        /// Clear all items.
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _array = new T[0];
        }

        /// <summary>
        /// Check whether or not an item exists in Queue.
        /// </summary>
        /// <param name="item">Item to check as generic type.</param>
        /// <returns>True if exists, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public bool Contains(T item)
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty!");
            for (int i = 0; i < _count; i++)
            {
                if (_array[i].Equals(item))
                    return true;
            }
            return false;
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
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty!");
            if (array.Length < _count)
                throw new IndexOutOfRangeException("Destination array's length is less than queue's count.");
            for (int i = 0; i < _count; i++)
            {
                array[arrayIndex++] = _array[i];
            }
        }

        /// <summary>
        /// Gets the total items in queue.
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
            for (int i = 0; i < _count; i++)
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
