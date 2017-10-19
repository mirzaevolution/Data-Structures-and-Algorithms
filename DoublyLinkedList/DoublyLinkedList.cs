using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    public enum AddMode
    {
        FirstPosition,
        LastPosition
    }
    /// <summary>
    /// Node class that holds value and references for the next and previous nodes.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class DoublyLinkedListNode<T>
    {
        /// <summary>
        /// Gets/Sets value for current node. It is generic type.
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Gets/Sets reference to the next node.
        /// </summary>
        public DoublyLinkedListNode<T> Next { get; set; }
        /// <summary>
        /// Gets/Sets reference to the previous node.
        /// </summary>
        public DoublyLinkedListNode<T> Previous { get; set; }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DoublyLinkedListNode() { }
        /// <summary>
        /// Secondary constructor that accepts value for current node.
        /// </summary>
        /// <param name="value">Generic type.</param>
        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }
    }
    /// <summary>
    /// Doubly Linked List class that supports collection-like iteration.
    /// </summary>
    /// <typeparam name="T">Generic type.</typeparam>
    public class DoublyLinkedList<T> : ICollection<T>
    {
        private int _count;
        /// <summary>
        /// Gets current Head property.
        /// </summary>
        public DoublyLinkedListNode<T> Head { get; private set; }

        /// <summary>
        /// Gets current Tail property.
        /// </summary>
        public DoublyLinkedListNode<T> Tail { get; private set; }

        /// <summary>
        /// Add an item to the first position in the list.
        /// </summary>
        /// <param name="node">Node that contains generic value type. Must be non-null</param>
        public void AddFirst(DoublyLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
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

        /// <summary>
        /// Add an item to the first position in the list.
        /// </summary>
        /// <param name="value">Item to add.</param>
        public void AddFirst(T value)
        {
            AddFirst(new DoublyLinkedListNode<T>(value));
        }


        /// <summary>
        /// Add an item to the last position in the list.
        /// </summary>
        /// <param name="node">Node that contains generic value type. Must be non-null</param>
        public void AddLast(DoublyLinkedListNode<T> node)
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
        /// Add an item to the last position in the list.
        /// </summary>
        /// <param name="value">Item to add.</param>
        public void AddLast(T value)
        {
            AddLast(new DoublyLinkedListNode<T>(value));
        }

        /// <summary>
        /// Add an item after selected node. Selected node cannot be null.
        /// </summary>
        /// <param name="node">Selected node.</param>
        /// <param name="valueToAdd">Item to add.</param>
        /// <exception cref="ArgumentNullException">'node' cannot be null.</exception>
        /// <exception cref="NullReferenceException">'node' does not exist.</exception>
        public void AddAfter(DoublyLinkedListNode<T> node, T valueToAdd)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (FindNode(node.Value) == null)
                throw new NullReferenceException("Cannot find a node with the specified 'node' parameter.");
            if (node.Next == null)
                AddLast(new DoublyLinkedListNode<T>(valueToAdd));
            else
            {
                DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(valueToAdd);
                DoublyLinkedListNode<T> currentNext = node.Next;
                node.Next = newNode;
                newNode.Previous = node;
                newNode.Next = currentNext;
                currentNext.Previous = newNode;
                _count++;
            }
        }

        /// <summary>
        /// Add an item before selected node. Selected node cannot be null.
        /// </summary>
        /// <param name="node">Selected node.</param>
        /// <param name="valueToAdd">Item to add.</param>
        /// <exception cref="ArgumentNullException">'node' cannot be null.</exception>
        /// <exception cref="NullReferenceException">'node' does not exist.</exception>
        public void AddBefore(DoublyLinkedListNode<T> node, T valueToAdd)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (FindNode(node.Value) == null)
                throw new NullReferenceException("Cannot find a node with the specified 'node' parameter.");
            if (node.Previous == null)
                AddFirst(new DoublyLinkedListNode<T>(valueToAdd));
            else
            {
                DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(valueToAdd);
                DoublyLinkedListNode<T> currentPrev = node.Previous;
                node.Previous = newNode;
                newNode.Next = node;
                newNode.Previous = currentPrev;
                currentPrev.Next = newNode;
                _count++;
            }
        }

        /// <summary>
        /// Add an item to list based on the selected option in AddMode enumeration.
        /// </summary>
        /// <param name="value">Item to add. Generic type.</param>
        /// <param name="addMode">Option to add an item.</param>
        public void Add(T value, AddMode addMode)
        {
            switch (addMode)
            {
                case AddMode.FirstPosition:
                    AddFirst(new DoublyLinkedListNode<T>(value));
                    break;
                case AddMode.LastPosition:
                    AddLast(new DoublyLinkedListNode<T>(value));
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

        /// <summary>
        /// Find a node in the list.
        /// </summary>
        /// <param name="value">Item to find.</param>
        /// <returns>Found node. Null if it is not found.</returns>
        public DoublyLinkedListNode<T> FindNode(T value)
        {
            if (_count == 0)
                throw new InvalidOperationException("List is empty!");
            DoublyLinkedListNode<T> current = Head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                    return current;
                current = current.Next;
            }
            return null;
        }
        #region ICollection<T>
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
            AddLast(new DoublyLinkedListNode<T>(value));
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
                DoublyLinkedListNode<T> current = Head;
                while (current != null)
                {
                    if (current.Value.Equals(value))
                    {
                        DoublyLinkedListNode<T> prev = current.Previous;
                        DoublyLinkedListNode<T> next = current.Next;
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
            return FindNode(value) != null;
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
            DoublyLinkedListNode<T> current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Gets IEnumerator of T for current list.
        /// </summary>
        /// <returns>IEnumerator of current generic type.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListNode<T> node = Head;
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
