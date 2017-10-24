using System;
namespace InsertionSort
{
    /// <summary>
    /// Class that holds extension method for performing Insertion Sort operation.
    /// </summary>
    public static class SortAlgorithm
    {
        /// <summary>
        /// Extension method to sort an array with Insertion Sort algorithm.
        /// </summary>
        /// <typeparam name="T">Any generic type that implements IComparable of T interface.</typeparam>
        /// <param name="array">Array to sort</param>
        public static T[] InsertionSort<T>(this T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
                return null;
            int len = array.Length;
            T[] newArray = new T[len];
            newArray[0] = array[0];
            int total = 1;
            for (int index = 1; index < len; index++)
            {
                bool inserted = false;
                for (int newIndex = 0; newIndex < total; newIndex++)
                {
                    if (newArray[newIndex].CompareTo(array[index]) > 0)
                    {
                        inserted = true;
                        Array.Copy(newArray, newIndex, newArray, newIndex + 1, total - newIndex);
                        newArray[newIndex] = array[index];
                        total++;
                    }
                    if (inserted)
                        break;
                }
                if (!inserted)
                {
                    newArray[total++] = array[index];
                }
            }
            return newArray;
        }
    }
}
