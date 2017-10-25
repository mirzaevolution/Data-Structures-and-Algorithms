using System;
using System.Collections.Generic;

namespace Set
{
    /// <summary>
    /// Set class that uses list backing store.
    /// </summary>
    /// <typeparam name="T">Any generic type that implements IComparable of T interface.</typeparam>
    public class Set<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> _list = new List<T>();
        /// <summary>
        /// Initializes default constructor.
        /// </summary>
        public Set() { }
        /// <summary>
        /// Initializes secondary constructor that accepts collection of items.
        /// </summary>
        /// <param name="items">Collection of items that implements IComparable of T interface.</param>
        public Set(IEnumerable<T> items)
        {
            AddRange(items);
        }

        /// <summary>
        /// Adds item to the set.
        /// </summary>
        /// <param name="item">Any generic type that implements IComparable of T interface.</param>
        public void Add(T item)
        {
            if (Contains(item))
                throw new InvalidOperationException($"Item `{item}` already exists in the list");
            _list.Add(item);
        }
        /// <summary>
        /// Adds collection of items to the set.
        /// </summary>
        /// <param name="items">Collection of items that implements IComparable of T interface.</param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }
        /// <summary>
        /// Removes an item from the set.
        /// </summary>
        /// <param name="item">Any generic type that implements IComparable of T interface.</param>
        /// <returns>True if item is deleted. Otherwise is false.</returns>
        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        /// <summary>
        /// Checks whether or not an item exists in the set.
        /// </summary>
        /// <param name="item">True if exists. Otherwise false.</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// Gets total items in the set.
        /// </summary>
        public int Count => _list.Count;


        /// <summary>
        /// Unions current set with another set.
        /// </summary>
        /// <param name="other">Another set.</param>
        /// <returns>Result of union.</returns>
        public Set<T> Union(Set<T> other)
        {
            Set<T> result = new Set<T>(_list);
            foreach (var item in other)
            {
                if (!Contains(item))
                    result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Intersects current set with another set.
        /// </summary>
        /// <param name="other">Another set.</param>
        /// <returns>Result of intersection.</returns>
        public Set<T> Intersection(Set<T> other)
        {
            Set<T> result = new Set<T>();
            foreach (var item in _list)
            {
                if (other.Contains(item))
                    result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Performs except operation between current set with another set.
        /// </summary>
        /// <param name="other">Another set.</param>
        /// <returns>Result of except/difference.</returns>
        public Set<T> Difference(Set<T> other)
        {
            Set<T> result = new Set<T>();
            foreach (var item in _list)
            {
                if (!other.Contains(item))
                    result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Performs symmetric except/difference between current set with another set.
        /// </summary>
        /// <param name="other">Another set.</param>
        /// <returns>Result of symmetric difference.</returns>
        public Set<T> SymmetricDifference(Set<T> other)
        {
            Set<T> union = Union(other);
            Set<T> intersect = Intersection(other);
            return union.Difference(intersect);
        }
        /// <summary>
        /// Gets Enumerator for current set.
        /// </summary>
        /// <returns>IEnumerator of T</returns>
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
