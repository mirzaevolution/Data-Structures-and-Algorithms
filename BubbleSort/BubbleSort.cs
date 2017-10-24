using System;

namespace BubbleSort
{
    /// <summary>
    /// Class that holds extension method for performing Bubble Sort operation.
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
        /// <summary>
        /// Extension method to sort an array with Bubble Sort algorithm.
        /// </summary>
        /// <typeparam name="T">Any generic type that implements IComparable of T interface.</typeparam>
        /// <param name="array">Array to sort</param>
        public static void BubbleSort<T>(this T[] array)
            where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
                return;
            bool swapped;
            int len = array.Length;
            do
            {
                swapped = false;
                for (int i = 1; i < len; i++)
                {
                    if (array[i - 1].CompareTo(array[i]) > 0)
                    {
                        Swap(array, i - 1, i);
                        swapped = true;
                    }
                }
            } while (swapped);
        }

    }
}
