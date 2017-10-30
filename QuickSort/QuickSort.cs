using System;

namespace QuickSort
{
    /// <summary>
    /// Class that holds extension method for performing Quick Sort operation.
    /// </summary>
    public static class SortAlgorithm
    {
        /// <summary>
        /// Extension method to sort an array with Quick Sort algorithm.
        /// </summary>
        /// <typeparam name="T">Any generic type that implements IComparable of T interface.</typeparam>
        /// <param name="array">Array to sort</param>
        public static void QuickSort<T>(this T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
                return;
            Sort(ref array, 0, array.Length - 1);
        }

        #region Private Methods
        private static void Sort<T>(ref T[] array, int leftBound, int rightBound) where T : IComparable<T>
        {
            if (leftBound < rightBound)
            {
                int leftWall = Partition(ref array, leftBound, rightBound);
                Sort(ref array, leftBound, leftWall - 1);
                Sort(ref array, leftWall + 1, rightBound);
            }
        }
        private static int Partition<T>(ref T[] array, int leftBound, int rightBound) where T : IComparable<T>
        {
            T pivot = array[rightBound]; //taking rightmost item;
            int leftWall = leftBound;
            for (int iterator = leftBound; iterator < rightBound; iterator++)
            {
                if (array[iterator].CompareTo(pivot) < 0)
                {
                    T temp = array[iterator];
                    array[iterator] = array[leftWall];
                    array[leftWall] = temp;
                    leftWall++;
                }
            }
            array[rightBound] = array[leftWall];
            array[leftWall] = pivot;
            return leftWall;
        }
        #endregion

    }
}
