using System.Diagnostics;
using SortingAlgorithms.Auxiliary;

namespace ExecutionTime;

class Program
{
    static void Main( string[] args )
    {
        List<int> arraySizes = new()
        {
            50000, 100000, 500000, 10000000, 30000000
        };
        foreach( int tmpArraySize in arraySizes )
        {
            int[] arrayToBeSorted = Utils.GetRandomArray( tmpArraySize );
            Stopwatch s = Stopwatch.StartNew();
            SortingAlgorithms.SortingAlgorithms.QuickSort( arrayToBeSorted );
            s.Stop();
            Console.WriteLine( $"{tmpArraySize} : {s.ElapsedMilliseconds} ms" );
        }
    }
}