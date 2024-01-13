using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SortingAlgorithms.Auxiliary;
using Sorting = SortingAlgorithms.SortingAlgorithms;

namespace SortingAlgorithmsTests
{
    public class SortingAlgorithmsCollection
    {
        public static IEnumerable<Func<int[], int>> Algoritms
        {
            get
            {
                yield return Sorting.InsertionSort;
                yield return Sorting.MergeSort;
                yield return Sorting.QuickSort;
                yield return Sorting.HeapSort;
            }
        }
    }

    internal class SortingAlgorithmTests
    {
        [Repeat( 25 )]
        [Test]
        [TestCaseSource( typeof(SortingAlgorithmsCollection), nameof(SortingAlgorithmsCollection.Algoritms) )]
        public void SortingAlgorithm_SortsArrayCorrectly( Func<int[], int> sortingAlgorithm )
        {
            // Arrange
            Random random = new();
            int arrayLength = random.Next( 0, 10000 );
            int[] arrayToSort = Utils.GetRandomArray( arrayLength );
            int[] sortedArray = arrayToSort.OrderBy( x => x ).ToArray();

            // Act
            sortingAlgorithm( arrayToSort );

            // Assert
            CollectionAssert.AreEqual( sortedArray, arrayToSort );
        }

        [Test]
        [TestCaseSource( typeof(SortingAlgorithmsCollection), nameof(SortingAlgorithmsCollection.Algoritms) )]
        public void SortingAlgorithm_SortsPredefinedArrayCorrectly( Func<int[], int> sortingAlgorithm )
        {
            // Arrange
            int[] arrayToSort = { 5, -10, 6, 11, 5, 17, 100 };
            int[] sortedArray = { -10, 5, 5, 6, 11, 17, 100 };

            // Act
            sortingAlgorithm( arrayToSort );

            // Assert
            CollectionAssert.AreEqual( sortedArray, arrayToSort );
        }

        [Test]
        [TestCaseSource( typeof(SortingAlgorithmsCollection), nameof(SortingAlgorithmsCollection.Algoritms) )]
        public void InsertionSort_SortsEmptyArrayCorrectly( Func<int[], int> sortingAlgorithm )
        {
            // Arrange
            int[] arrayToSort = Array.Empty<int>();
            int[] sortedArray = Array.Empty<int>();

            // Act
            sortingAlgorithm( arrayToSort );

            // Assert
            CollectionAssert.AreEqual( sortedArray, arrayToSort );
        }
    }
}