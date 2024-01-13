using System;

namespace SortingAlgorithms.Auxiliary
{
    /// <summary>
    /// Holder for different utilities
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Gets array with specified length, filled with random values from min to max
        /// </summary>
        /// <param name="length">Length of the array</param>
        /// <param name="min">Min possible element</param>
        /// <param name="max">Max possible element</param>
        /// <returns>Random array</returns>
        public static int[] GetRandomArray( int length = 100, int min = -1000, int max = 1000 )
        {
            int[] array = new int[length];
            Random r = new();
            for( int i = 0; i < length; i++ )
            {
                array[i] = r.Next( min, max );
            }

            return array;
        }
    }
}