using System;
using System.Collections.Generic;
namespace Queue
{
    /// <summary>
    /// Queues with Array backing store.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        private int _count = 0;
        private T[] _array = new T[0];
        private int _head = 0;
        private int _tail = -1;
        /// <summary>
        /// Initializes queue with default constructor.
        /// </summary>
        public Queue()
        { }

        /// <summary>
        /// Initializes queue with collection of items.
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
        /// Enqueues (store) item the queue.
        /// </summary>
        /// <param name="item">Item to enqueue as generic type.</param>
        public void Enqueue(T item)
        {
            if (_count == _array.Length)
            {
                int newSize = _count == 0 ? 3 : _count * 2;
                if (_count == 0)
                    _array = new T[newSize];
                else
                {
                    T[] newArray = new T[newSize];
                    int index = 0;
                    int pointer = _head;
                    while (index < _count)
                    {
                        newArray[index] = _array[pointer];
                        pointer = (pointer + 1) % _array.Length;
                        index++;
                    }
                    _head = 0;
                    _tail = _count - 1;
                    _array = newArray;
                }
            }
            _tail = (_tail + 1) % _array.Length;
            _array[_tail] = item;
            _count++;
        }

        /// <summary>
        /// Peeks the top level item without removing it.
        /// </summary>
        /// <returns>Top level item.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty!");
            return _array[_head];
        }

        /// <summary>
        /// Dequeues (remove) top level item from queue.
        /// </summary>
        /// <returns>Top level item being removed.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty!");
            T item = _array[_head];
            _array[_head] = default(T);
            _head = (_head + 1) % _array.Length;
            _count--;
            return item;
        }

        /// <summary>
        /// Checks whether or not an item exists in Queue.
        /// </summary>
        /// <param name="item">Item to check as generic type.</param>
        /// <returns>True if exists, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        public bool Contains(T item)
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty!");
            int iterator = 0;
            int pointer = _head;
            while (iterator < _count)
            {
                if (_array[pointer].Equals(item))
                    return true;
                pointer = (pointer + 1) % _array.Length;
                iterator++;
            }
            return false;
        }

        /// <summary>
        /// Copies all items in queue to an array with start index.
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
            int iterator = 0;
            int pointer = _head;
            while (iterator < _count)
            {
                array[arrayIndex++] = _array[pointer];
                pointer = (pointer + 1) % _array.Length;
                iterator++;
            }
        }


        /// <summary>
        /// Clears all items.
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _array = new T[0];
            _head = 0;
            _tail = -1;
        }

        /// <summary>
        /// Gets the total items in queue.
        /// </summary>
        public int Count { get { return _count; } }

        /// <summary>
        /// Gets IEnumerator of T.
        /// </summary>
        /// <returns>IEnumerator of T</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int iterator = 0;
            int pointer = _head;
            while (iterator < _count)
            {
                yield return _array[pointer];
                pointer = (pointer + 1) % _array.Length;
                iterator++;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
