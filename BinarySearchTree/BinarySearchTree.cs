
/*
 * ======================================================================
 * 
 * Dynamic Binary Search Tree that supports any type that implements
 * IComparable<T> interface. Supports 6 different ways of traversal.
 * 
 * January 21, 2015
 * Originally created by: Mirza Ghulam Rasyid.
 * First Uploaded to Github: October 23, 2017.
 * Please report if you find any bug within the code. :)
 * 
 * ======================================================================
 *
 * */

using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    /// <summary>
    /// Binary Search Tree Node.
    /// </summary>
    /// <typeparam name="T">Any generic type that implements IComparable of T interface.</typeparam>
    public class BinarySearchTreeNode<T> : IComparable<T> where T : IComparable<T>
    {
        /// <summary>
        /// Gets/Sets value for current next.
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Gets/Sets left node reference.
        /// </summary>
        public BinarySearchTreeNode<T> Left { get; set; }
        /// <summary>
        /// Gets/Sets right node reference.
        /// </summary>
        public BinarySearchTreeNode<T> Right { get; set; }
        /// <summary>
        /// Initializes default constructor.
        /// </summary>
        public BinarySearchTreeNode() { }
        /// <summary>
        /// Initializes secondary constructor with parameter.
        /// </summary>
        /// <param name="value">Value that implements IComparable of T.</param>
        public BinarySearchTreeNode(T value) { Value = value; }

        /// <summary>
        /// IComparable of T CompareTo method.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns> A value that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.
        /// Zero This object is equal to other. Greater than zero This object is greater than other.
        /// </returns>
        public int CompareTo(T other)
        {
            return this.Value.CompareTo(other);
        }
    }

    /// <summary>
    /// Binary Search Tree class.
    /// </summary>
    /// <typeparam name="T">Any generic type that implements IComparable of T interface.</typeparam>
    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private int _count;
        private BinarySearchTreeNode<T> _root;

        /// <summary>
        /// Gets total count for items in tree.
        /// </summary>
        public int Count { get { return _count; } }

        /// <summary>
        /// Initializes default constructor.
        /// </summary>
        public BinarySearchTree() { }

        /// <summary>
        /// Initializes secondary constructor with collection.
        /// </summary>
        /// <param name="collection">A collection that will be added to tree.</param>
        public BinarySearchTree(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                    Add(item);
            }
        }

        /// <summary>
        /// Adds new node to tree by specifying its value.
        /// </summary>
        /// <param name="value">Any generic type that implements IComparable of T interface.</param>
        public void Add(T value)
        {
            if (_root == null)
                _root = new BinarySearchTreeNode<T>(value);
            else
            {
                Add(new BinarySearchTreeNode<T>(value), _root);
            }
            _count++;
        }

        /// <summary>
        /// Finds a node by specifying its value.
        /// </summary>
        /// <param name="value">Any generic type that implements IComparable of T interface.</param>
        /// <returns>Found node. Null if it's not found.</returns>
        /// <exception cref="InvalidOperationException">Binary Search Tree is empty.</exception>
        public BinarySearchTreeNode<T> Find(T value)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            BinarySearchTreeNode<T> parent = null;
            return FindNodeAndParent(value, out parent);
        }

        /// <summary>
        /// Removes a node by specifying its value.
        /// </summary>
        /// <param name="value">Any generic type that implements IComparable of T interface.</param>
        /// <returns>True if deleted. Otherwise false.</returns>
        /// <exception cref="InvalidOperationException">Binary Search Tree is empty.</exception>
        public bool Remove(T value)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            BinarySearchTreeNode<T> parent = null;
            BinarySearchTreeNode<T> node = FindNodeAndParent(value, out parent);
            if (node == null)
            {
                return false;
            }
            //decrement first!
            _count--;

            if (node.Right == null)
            {
                //it's a root!!
                if (parent == null)
                {
                    _root = node.Left;
                }
                else
                {
                    //it's left
                    if (parent.CompareTo(value) > 0)
                    {
                        parent.Left = node.Left;
                    }
                    else
                    {
                        parent.Right = node.Left;
                    }
                }
            }
            else if (node.Right.Left == null)
            {
                node.Right.Left = node.Left;
                if (parent == null)
                {
                    _root = node.Right;
                }
                else
                {
                    if (parent.CompareTo(value) > 0)
                    {
                        parent.Left = node.Right;
                    }
                    else
                    {
                        parent.Right = node.Right;
                    }
                }
            }
            else
            {
                BinarySearchTreeNode<T> parentLeftEnd = node.Right;
                BinarySearchTreeNode<T> leftEnd = node.Right.Left;

                while (leftEnd.Left != null)
                {
                    parentLeftEnd = leftEnd;
                    leftEnd = leftEnd.Left;
                }

                parentLeftEnd.Left = leftEnd.Right;
                leftEnd.Left = node.Left;
                leftEnd.Right = node.Right;

                if (parent == null)
                {
                    _root = leftEnd;
                }
                else
                {
                    if (parent.CompareTo(value) > 0)
                    {
                        parent.Left = leftEnd;
                    }
                    else
                    {
                        parent.Right = leftEnd;
                    }
                }
            }
            node = null;
            return true;
        }

        /// <summary>
        /// Traverses tree in pre-order manner.
        /// </summary>
        /// <param name="action">An action to take while traversing the tree.</param>
        /// <param name="parent">Parent in which traversal will be started from.</param>
        /// <exception cref="InvalidOperationException">Binary Search Tree is empty.</exception>
        /// <exception cref="ArgumentNullException">Action cannot be null.</exception>
        /// <exception cref="Exception">'Parent' doesn't exist in tree.</exception>
        public void PreOrderTraversal(Action<T> action, BinarySearchTreeNode<T> parent = null)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Action cannot be null");
            if (parent != null && !Contains(parent.Value))
                throw new Exception(string.Format("Node with value: {0} doesn't exist!", parent.Value));
            if (parent == null)
                parent = _root;
            PreOrderTraversal(parent, action);
        }

        /// <summary>
        /// Traverses tree in in-order manner.
        /// </summary>
        /// <param name="action">An action to take while traversing the tree.</param>
        /// <param name="parent">Parent in which traversal will be started from.</param>
        /// <exception cref="InvalidOperationException">Binary Search Tree is empty.</exception>
        /// <exception cref="ArgumentNullException">Action cannot be null.</exception>
        /// <exception cref="Exception">'Parent' doesn't exist in tree.</exception>
        public void InOrderTraversal(Action<T> action, BinarySearchTreeNode<T> parent = null)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Action cannot be null");
            if (parent != null && !Contains(parent.Value))
                throw new Exception(string.Format("Node with value: {0} doesn't exist!", parent.Value));
            if (parent == null)
                parent = _root;
            InOrderTraversal(parent, action);
        }

        /// <summary>
        /// Traverses tree in post-order manner.
        /// </summary>
        /// <param name="action">An action to take while traversing the tree.</param>
        /// <param name="parent">Parent in which traversal will be started from.</param>
        /// <exception cref="InvalidOperationException">Binary Search Tree is empty.</exception>
        /// <exception cref="ArgumentNullException">Action cannot be null.</exception>
        /// <exception cref="Exception">'Parent' doesn't exist in tree.</exception>
        public void PostOrderTraversal(Action<T> action, BinarySearchTreeNode<T> parent = null)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Action cannot be null");
            if (parent != null && !Contains(parent.Value))
                throw new Exception(string.Format("Node with value: {0} doesn't exist!", parent.Value));
            if (parent == null)
                parent = _root;
            PostOrderTraversal(parent, action);
        }

        /// <summary>
        /// Traverses tree in inverse-pre-order manner.
        /// </summary>
        /// <param name="action">An action to take while traversing the tree.</param>
        /// <param name="parent">Parent in which traversal will be started from.</param>
        /// <exception cref="InvalidOperationException">Binary Search Tree is empty.</exception>
        /// <exception cref="ArgumentNullException">Action cannot be null.</exception>
        /// <exception cref="Exception">'Parent' doesn't exist in tree.</exception>
        public void InversePreOrderTraversal(Action<T> action, BinarySearchTreeNode<T> parent = null)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Action cannot be null");
            if (parent != null && !Contains(parent.Value))
                throw new Exception(string.Format("Node with value: {0} doesn't exist!", parent.Value));
            if (parent == null)
                parent = _root;
            InversePreOrderTraversal(parent, action);
        }

        /// <summary>
        /// Traverses tree in inverse-in-order manner.
        /// </summary>
        /// <param name="action">An action to take while traversing the tree.</param>
        /// <param name="parent">Parent in which traversal will be started from.</param>
        /// <exception cref="InvalidOperationException">Binary Search Tree is empty.</exception>
        /// <exception cref="ArgumentNullException">Action cannot be null.</exception>
        /// <exception cref="Exception">'Parent' doesn't exist in tree.</exception>
        public void InverseInOrderTraversal(Action<T> action, BinarySearchTreeNode<T> parent = null)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Action cannot be null");
            if (parent != null && !Contains(parent.Value))
                throw new Exception(string.Format("Node with value: {0} doesn't exist!", parent.Value));
            if (parent == null)
                parent = _root;
            InverseInOrderTraversal(parent, action);
        }

        /// <summary>
        /// Traverses tree in inverse-post-order manner.
        /// </summary>
        /// <param name="action">An action to take while traversing the tree.</param>
        /// <param name="parent">Parent in which traversal will be started from.</param>
        /// <exception cref="InvalidOperationException">Binary Search Tree is empty.</exception>
        /// <exception cref="ArgumentNullException">Action cannot be null.</exception>
        /// <exception cref="Exception">'Parent' doesn't exist in tree.</exception>
        public void InversePostOrderTraversal(Action<T> action, BinarySearchTreeNode<T> parent = null)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Action cannot be null");
            if (parent != null && !Contains(parent.Value))
                throw new Exception(string.Format("Node with value: {0} doesn't exist!", parent.Value));
            if (parent == null)
                parent = _root;
            InversePostOrderTraversal(parent, action);
        }

        /// <summary>
        /// Checks whether or not a node exists in tree by specifying its value.
        /// </summary>
        /// <param name="value">Any generic type that implements IComparable of T interface.</param>
        /// <returns>True if exists. Otherwise false.</returns>
        public bool Contains(T value)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            BinarySearchTreeNode<T> parent = null;
            return FindNodeAndParent(value, out parent) != null;
        }

        /// <summary>
        /// Clears all nodes in tree.
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _root = null;
        }
        
        #region Private Methods
        private void Add(BinarySearchTreeNode<T> node, BinarySearchTreeNode<T> parent)
        {
            //go to left
            if (parent.CompareTo(node.Value) > 0)
            {
                if (parent.Left == null)
                    parent.Left = node;
                else
                    Add(node, parent.Left);
            }
            //go to right
            else
            {
                if (parent.Right == null)
                    parent.Right = node;
                else
                    Add(node, parent.Right);
            }
        }
        private BinarySearchTreeNode<T> FindNodeAndParent(T value, out BinarySearchTreeNode<T> parent)
        {
            if (_count == 0)
                throw new InvalidOperationException("Binary Search Tree is empty!");
            parent = null;
            BinarySearchTreeNode<T> pointer = _root;
            while (pointer != null)
            {
                //go to left
                if (pointer.CompareTo(value) > 0)
                {
                    parent = pointer;
                    pointer = pointer.Left;
                }
                //go to right
                else if (pointer.CompareTo(value) < 0)
                {
                    parent = pointer;
                    pointer = pointer.Right;
                }
                //match!
                else
                    break;
            }
            return pointer;
        }
        private void PreOrderTraversal(BinarySearchTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(node.Left, action);
                PreOrderTraversal(node.Right, action);
            }
        }
        private void InOrderTraversal(BinarySearchTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, action);
                action(node.Value);
                InOrderTraversal(node.Right, action);
            }
        }
        private void PostOrderTraversal(BinarySearchTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                PostOrderTraversal(node.Left, action);
                PostOrderTraversal(node.Right, action);
                action(node.Value);
            }
        }
        private void InversePreOrderTraversal(BinarySearchTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                action(node.Value);
                InversePreOrderTraversal(node.Right, action);
                InversePreOrderTraversal(node.Left, action);
            }
        }
        private void InverseInOrderTraversal(BinarySearchTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                InverseInOrderTraversal(node.Right, action);
                action(node.Value);
                InverseInOrderTraversal(node.Left, action);
            }
        }
        private void InversePostOrderTraversal(BinarySearchTreeNode<T> node, Action<T> action)
        {
            if (node != null)
            {
                InversePostOrderTraversal(node.Right, action);
                InversePostOrderTraversal(node.Left, action);
                action(node.Value);
            }
        }
        #endregion

        #region IEnumerable<T>
        /// <summary>
        /// Gets IEnumerator for this tree.
        /// </summary>
        /// <returns>IEnumerator of T where T is data type for this tree that implements IComparable of T.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            List<T> enumerator = new List<T>();
            InOrderTraversal((val) => enumerator.Add(val));
            return enumerator.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
        #endregion
    }
}
