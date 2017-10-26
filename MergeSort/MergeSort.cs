using System;
namespace MergeSort
{
    /// <summary>
    /// Class that holds extension method for performing Merge Sort operation.
    /// </summary>
    public static class SortAlgorithm
    {
        /// <summary>
        /// Extension method to sort an array with Merge Sort algorithm.
        /// </summary>
        /// <typeparam name="T">Any generic type that implements IComparable of T interface.</typeparam>
        /// <param name="array">Array to sort</param>
        public static void MergeSort<T>(this T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
                return;
            Divide(array);
        }
        #region Private Methods
        private static void Divide<T>(T[] array) where T : IComparable<T>
        {
            if (array.Length <= 1)
                return;
            int leftSize = array.Length / 2;
            int rightSize = array.Length - leftSize;
            T[] leftArray = new T[leftSize];
            T[] rightArray = new T[rightSize];
            Array.Copy(array, 0, leftArray, 0, leftSize);
            Array.Copy(array, leftSize, rightArray, 0, rightSize);
            //divide left part
            Divide(leftArray);
            //divide right part
            Divide(rightArray);
            //merge them together
            Merge(array, leftArray, rightArray);
        }

        private static void Merge<T>(T[] array, T[] leftArray, T[] rightArray) where T : IComparable<T>
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int index = 0;
            int length = leftArray.Length + rightArray.Length;
            while (length != 0)
            {
                if (leftIndex >= leftArray.Length)
                    array[index] = rightArray[rightIndex++];
                else if (rightIndex >= rightArray.Length)
                    array[index] = leftArray[leftIndex++];
                else if (leftArray[leftIndex].CompareTo(rightArray[rightIndex]) <= 0)
                    array[index] = leftArray[leftIndex++];
                else
                    array[index] = rightArray[rightIndex++];
                index++;
                length--;
            }
        }
        #endregion
    }
}
