using System;
namespace SelectionSort
{

    /// <summary>
    /// Class that holds extension method for performing Selection Sort operation.
    /// </summary>

    public static class SortAlgorithm
    {
        private static void Swap<T>(T[] array, int previousIndex, int nextIndex)
        {
            if (previousIndex != nextIndex)
            {
                T item = array[previousIndex];
                array[previousIndex] = array[nextIndex];
                array[nextIndex] = item;
            }
        }
        private static int FindSmallestIndex<T>(T[] array, int currentSmallestIndex) where T : IComparable<T>
        {
            T smallestItem = array[currentSmallestIndex];
            int len = array.Length;
            for (int i = currentSmallestIndex + 1; i < len; i++)
            {
                if (smallestItem.CompareTo(array[i]) > 0)
                {
                    smallestItem = array[i];
                    currentSmallestIndex = i;
                }
            }
            return currentSmallestIndex;
        }
        /// <summary>
        /// Extension method to sort an array with Selection Sort algorithm.
        /// </summary>
        /// <typeparam name="T">Any generic type that implements IComparable of T interface.</typeparam>
        /// <param name="array">Array to sort</param>
        public static void SelectionSort<T>(this T[] array) where T : IComparable<T>
        {
            int index = 0;
            int len = array.Length;
            while (index < len)
            {
                int smallestIndex = FindSmallestIndex(array, index);
                Swap(array, index, smallestIndex);
                index++;
            }
        }
    }
}
