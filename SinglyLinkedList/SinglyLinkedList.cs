using System;
using System.Collections.Generic;

namespace SinglyLinkedList
{
    public enum AddMode
    {
        FirstPosition,
        LastPosition
    }

    /// <summary>
    /// Node class that holds value and reference for the next node.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    /// 
    public class SinglyLinkedListNode<T>
    {
        /// <summary>
        /// Value for current node. It is generic type.
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Reference to the next node.
        /// </summary>
        public SinglyLinkedListNode<T> Next { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SinglyLinkedListNode() { }
        /// <summary>
        /// Secondary constructor that accepts value for current node.
        /// </summary>
        /// <param name="value">Generic type.</param>
        public SinglyLinkedListNode(T value)
        {
            Value = value;
        }
    }
    
    /// <summary>
    /// Singly Linked List class that supports collection-like iteration.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class SinglyLinkedList<T> : ICollection<T>
    {

        private int _count;
        
        /// <summary>
        /// Get current Head property.
        /// </summary>
        public SinglyLinkedListNode<T> Head { get; private set; }
        
        /// <summary>
        /// Get current Tail property.
        /// </summary>
        public SinglyLinkedListNode<T> Tail { get; private set; }

        /// <summary>
        /// Add an item to the first position in the list.
        /// </summary>
        /// <param name="node">Node that contains generic value type. Must be non-null.</param>
        /// <exception cref="ArgumentNullException">'node' cannot be null.</exception>
        public void AddFirst(SinglyLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (_count == 0)
                Head = Tail = node;
            else
            {
                node.Next = Head;
                Head = node;
            }
            _count++;
        }

        /// <summary>
        /// Add an item to the first position in the list.
        /// </summary>
        /// <param name="value">Item to add.</param>
        public void AddFirst(T value)
        {
            AddFirst(new SinglyLinkedListNode<T>(value));
        }

        /// <summary>
        /// Add an item to the last position in the list.
        /// </summary>
        /// <param name="node">Node that contains generic value type. Must be non-null.</param>
        /// <exception cref="ArgumentNullException">'node' cannot be null.</exception>
        public void AddLast(SinglyLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (_count == 0)
                Head = Tail = node;
            else
            {
                Tail.Next = node;
                Tail = node;
            }
            _count++;
        }

        /// <summary>
        /// Add an item to the last position in the list.
        /// </summary>
        /// <param name="value">Item to add.</param>
        public void AddLast(T value)
        {
            AddLast(new SinglyLinkedListNode<T>(value));
        }
        
        /// <summary>
        /// Add an item to list based on the selected option in AddMode enumeration.
        /// </summary>
        /// <param name="value">Item to add. Generic type.</param>
        /// <param name="addMode">Option to add an item.</param>
        public void Add(T value, AddMode addMode)
        {
            switch(addMode)
            {
                case AddMode.FirstPosition:
                    AddFirst(new SinglyLinkedListNode<T>(value));
                    break;
                case AddMode.LastPosition:
                    AddLast(new SinglyLinkedListNode<T>(value));
                    break;
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
            }
            _count--;
        }

        /// <summary>
        /// Remove the last item in the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">List is empty.</exception>
        /// 
        public void RemoveLast()
        {
            if (_count == 0)
                throw new InvalidOperationException("List is empty!");

            if (_count == 1)
                Head = Tail = null;
            else
            {
                SinglyLinkedListNode<T> current = Head;
                while (current.Next != Tail)
                {
                    current = current.Next;
                }
                Tail = current;
                Tail.Next = null;
            }
            _count--;
        }

        /// <summary>
        /// Add an item to the last position in the list.
        /// </summary>
        /// <param name="value">Item to add.</param>
        public void Add(T value)
        {
            AddLast(new SinglyLinkedListNode<T>(value));
        }

        #region ICollection<T>

        /// <summary>
        /// Get current total items in the list.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Get whether or not list is read only.
        /// </summary>
        public bool IsReadOnly => false;

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
            SinglyLinkedListNode<T> current = Head;
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
            SinglyLinkedListNode<T> current = Head;
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
                SinglyLinkedListNode<T> node = Head;
                SinglyLinkedListNode<T> prev = null;
                while (node != null)
                {
                    if (node.Value.Equals(value))
                    {
                        prev.Next = node.Next;
                        node = null;
                        _count--;
                        return true;
                    }
                    prev = node;
                    node = node.Next;
                }
            }
            return false;
        }
        /// <summary>
        /// Get IEnumerator of T for current list.
        /// </summary>
        /// <returns>IEnumerator of current generic type.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            SinglyLinkedListNode<T> node = Head;
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
