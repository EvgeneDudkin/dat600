using System.Collections.Generic;
using System.Threading;

namespace SortingAlgorithms
{
    /// <summary>
    /// Class contains different sorting algorithms
    ///<remarks>Each sorting algorithm returns number of operations (rough)</remarks>
    /// </summary>
    public static class SortingAlgorithms
    {
        /// <summary>
        /// Insertion sort
        /// </summary>
        /// <param name="array">Array for sorting</param>
        /// <returns>Approximate number of operations performed during sorting</returns>
        public static int InsertionSort( int[] array )
        {
            int numberOfOperations = 0;
            for( int j = 1; j < array.Length; j++ )
            {
                int key = array[j];
                int i = j - 1;
                numberOfOperations += 2;
                while( i >= 0 && array[i] > key )
                {
                    array[i + 1] = array[i];
                    i--;
                    numberOfOperations++;
                }

                array[i + 1] = key;
                numberOfOperations++;
            }

            return numberOfOperations;
        }

        /// <summary>
        /// Qsort
        /// </summary>
        /// <param name="array">Array for sorting</param>
        /// <returns>Approximate number of operations performed during sorting</returns>
        public static int QuickSort( int[] array )
        {
            return QSort( array, 0, array.Length - 1 );
        }

        /// <summary>
        /// Merge sort
        /// </summary>
        /// <param name="array">Array for sorting</param>
        /// <returns>Approximate number of operations performed during sorting</returns>
        public static int MergeSort( int[] array )
        {
            return MergeSortInternal( array, 0, array.Length );
        }

        /// <summary>
        /// Heap sort
        /// </summary>
        /// <param name="array">Array for sorting</param>
        /// <returns>Approximate number of operations performed during sorting</returns>
        public static int HeapSort( int[] array )
        {
            int numberOfOperations = 0;

            numberOfOperations += ConstructMaxHeap( array );

            int size = array.Length;
            numberOfOperations++;
            while( size > 0 )
            {
                (array[0], array[size - 1]) = (array[size - 1], array[0]);
                size--;
                numberOfOperations += SiftDown( array, 1, size ) + 2;
            }

            return numberOfOperations;
        }

        private static int QSort( int[] array, int l, int r )
        {
            int numberOfOperations = 0;
            if( l >= r )
                return numberOfOperations;
            numberOfOperations += Partition( array, l, r, out int m );
            numberOfOperations += QSort( array, l, m - 1 );
            numberOfOperations += QSort( array, m + 1, r );

            return numberOfOperations;
        }

        private static int Partition( int[] array, int l, int r, out int p )
        {
            int numberOfOperations = 0;
            int x = array[l];
            int j = l;
            numberOfOperations += 2;

            for( int i = l + 1; i <= r; i++ )
            {
                if( array[i] <= x )
                {
                    j++;
                    (array[j], array[i]) = (array[i], array[j]);
                }
            }

            (array[l], array[j]) = (array[j], array[l]);
            p = j;
            return numberOfOperations;
        }

        private static int MergeSortInternal( int[] array, int l, int r )
        {
            int numberOfOperations = 0;
            if( l + 1 >= r )
                return numberOfOperations;

            int m = (l + r) / 2;
            numberOfOperations++;
            numberOfOperations += MergeSortInternal( array, l, m );
            numberOfOperations += MergeSortInternal( array, m, r );
            numberOfOperations += MergeInternal( array, l, r, m );

            return numberOfOperations;
        }

        private static int MergeInternal( int[] array, int l, int r, int m )
        {
            int i = l, j = m;
            List<int> tempList = new();
            int numberOfOperations = 0;
            //merge in tmp-array
            while( i < m && j < r )
            {
                if( array[i] > array[j] )
                {
                    tempList.Add( array[j] );
                    j++;
                    numberOfOperations += 2;
                }
                else
                {
                    tempList.Add( array[i] );
                    i++;
                    numberOfOperations += 2;
                }
            }

            //additional check for first part
            while( i < m )
            {
                tempList.Add( array[i] );
                i++;
                numberOfOperations += 2;
            }

            //additional check for second part
            while( j < r )
            {
                tempList.Add( array[j] );
                j++;
                numberOfOperations += 2;
            }

            //copy data from tmp-array to the base-array
            foreach( int t in tempList )
            {
                array[l] = t;
                l++;
                numberOfOperations += 2;
            }

            return numberOfOperations;
        }

        private static int ConstructMaxHeap( int[] array )
        {
            int numberOfOperations = 0;
            int n = (array.Length) / 2;
            numberOfOperations++;
            for( int i = n; i > 0; i-- )
            {
                numberOfOperations += SiftDown( array, i, array.Length );
            }

            return numberOfOperations;
        }

        private static int SiftDown( int[] array, int i, int size )
        {
            int numberOfOperations = 0;
            int leftChildIndex = 2 * i - 1;
            int rightChildIndex = 2 * i;
            numberOfOperations += 2;

            int localMaxIndex = FindIndexWithLocalMax( array, size, out int tmpOp, i - 1, leftChildIndex, rightChildIndex );
            numberOfOperations += tmpOp;
            if( localMaxIndex >= 0 && localMaxIndex != i - 1 )
            {
                (array[i - 1], array[localMaxIndex]) = (array[localMaxIndex], array[i - 1]);
                numberOfOperations += SiftDown( array, localMaxIndex + 1, size );
            }

            return numberOfOperations;
        }

        private static int FindIndexWithLocalMax( int[] array, int size, out int numberOfOperations, params int[] indexes )
        {
            int result = -1;
            numberOfOperations = 0;
            foreach( int currentIndex in indexes )
            {
                if( 0 <= currentIndex && currentIndex < size && (result < 0 || array[currentIndex] >= array[result]) )
                {
                    result = currentIndex;
                    numberOfOperations++;
                }

                numberOfOperations++;
            }

            return result;
        }
    }
}