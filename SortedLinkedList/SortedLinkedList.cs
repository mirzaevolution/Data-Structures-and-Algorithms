using System;
using System.Collections.Generic;

namespace SortedLinkedList
{
    /// <summary>
    /// Node class that holds value and references for the next and previous nodes.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class LinkedListNode<T> : IComparable<T> where T : IComparable<T>
    {
        /// <summary>
        /// Gets/Sets value for current node. It is generic type.
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Gets/Sets reference to the next node.
        /// </summary>
        public LinkedListNode<T> Next { get; set; }
        /// <summary>
        /// Gets/Sets reference to the previous node.
        /// </summary>
        public LinkedListNode<T> Previous { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LinkedListNode() { }
        /// <summary>
        /// Secondary constructor that accepts value for current node.
        /// </summary>
        /// <param name="value">Generic type.</param>
        public LinkedListNode(T value) { Value = value; }

        /// <summary>
        /// Compare current value to other value.
        /// </summary>
        /// <param name="other">Other value.</param>
        /// <returns>-1 if current is less than other. 0 if both are same. 1 if current is greater than other</returns>
        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }

    /// <summary>
    /// Sorted Linked List class that supports collection-like iteration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortedLinkedList<T> : ICollection<T> where T : IComparable<T>
    {
        private int _count;
        private void AddFirst(LinkedListNode<T> node)
        {
            if (_count == 0)
                Head = Tail = node;
            else
            {
                node.Next = Head;
                Head.Previous = node;
                Head = node;
            }
            _count++;
        }
        private void AddLast(LinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (_count == 0)
                Head = Tail = node;
            else
            {
                node.Previous = Tail;
                Tail.Next = node;
                Tail = node;
            }
            _count++;
        }
        /// <summary>
        /// Gets current Head property.
        /// </summary>
        public LinkedListNode<T> Head { get; private set; }

        /// <summary>
        /// Gets current Tail property.
        /// </summary>
        public LinkedListNode<T> Tail { get; private set; }

        /// <summary>
        /// Add new node to linked list.
        /// </summary>
        /// <param name="node">New node. Must be non-null.</param>
        /// <exception cref="ArgumentNullException">'node' cannot be null.</exception>
        public void Add(LinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (_count == 0)
                Head = Tail = node;

            //add first
            if (Head.CompareTo(node.Value) > 0)
            {
                AddFirst(node);
            }
            //Add Last
            else if (Tail.CompareTo(node.Value) <= 0)
            {
                AddLast(node);
            }
            else
            {
                LinkedListNode<T> current = Head;
                while (current != null)
                {
                    if ((current.CompareTo(node.Value) <= 0) && (current.Next.CompareTo(node.Value) > 0))
                    {
                        LinkedListNode<T> currentNext = current.Next;
                        node.Next = currentNext;
                        currentNext.Previous = node;
                        current.Next = node;
                        node.Previous = current;
                        break;
                    }
                    current = current.Next;
                }

                _count++;
            }

        }
        /// <summary>
        /// Remove the first item in the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">List is empty.</exception>
        public void RemoveFirst()
        {
            if (_count == 0)
                throw new InvalidOperationException("List is empty!");
            if (_count == 1)
                Head = Tail = null;
            else
            {
                Head = Head.Next;
                Head.Previous = null;
            }
            _count--;
        }

        /// <summary>
        /// Remove the last item in the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">List is empty.</exception>
        public void RemoveLast()
        {
            if (_count == 0)
                throw new InvalidOperationException("List is empty!");
            if (_count == 1)
                Head = Tail = null;
            else
            {
                Tail = Tail.Previous;
                Tail.Next = null;
            }
            _count--;
        }

        #region ICollection

        /// <summary>
        /// Gets current total items in the list.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gets whether or not list is read only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Add an item to the last position in the list.
        /// </summary>
        /// <param name="value">Item to add.</param>
        public void Add(T value)
        {
            Add(new LinkedListNode<T>(value));
        }

        /// <summary>
        /// Clear the items in the list.
        /// </summary>
        public void Clear()
        {
            _count = 0;
            Head = Tail = null;
        }

        /// <summary>
        /// Check whether or not an item exists in the list.
        /// </summary>
        /// <param name="value">Item to check. Generic type.</param>
        /// <returns>True if found. Otherwise false.</returns>
        // <exception cref="InvalidOperationException">List is empty.</exception>
        public bool Contains(T value)
        {
            if (_count == 0)
                throw new InvalidOperationException("List is empty!");
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                    return true;
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Copy items in the list to array.
        /// </summary>
        /// <param name="array">Array to be copied to.</param>
        /// <param name="arrayIndex">Array start index.</param>
        // <exception cref="InvalidOperationException">List is empty.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_count == 0)
                throw new InvalidOperationException("List is empty!");
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Remove an item from the list.
        /// </summary>
        /// <param name="value">Item to remove. Generic type.</param>
        /// <returns>True if an item is removed. Otherwise false.</returns>
        /// <exception cref="InvalidOperationException">List is empty.</exception>
        public bool Remove(T value)
        {
            if (_count == 0)
                throw new InvalidOperationException("List is empty!");
            if (Head.Value.Equals(value))
            {
                RemoveFirst();
                return true;
            }
            else if (Tail.Value.Equals(value))
            {
                RemoveLast();
                return true;
            }
            else
            {
                LinkedListNode<T> current = Head;
                while (current != null)
                {
                    if (current.Value.Equals(value))
                    {
                        LinkedListNode<T> prev = current.Previous;
                        LinkedListNode<T> next = current.Next;
                        prev.Next = next;
                        next.Previous = prev;
                        current = null;
                        _count--;
                        return true;
                    }
                    current = current.Next;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets IEnumerator of T for current list.
        /// </summary>
        /// <returns>IEnumerator of current generic type.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> node = Head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}
