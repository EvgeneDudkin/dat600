using RDotNet;
using SortingAlgorithms.Auxiliary;
using StepsCounting.Auxiliary;

namespace StepsCounting;

class Program
{
    static void Main( string[] args )
    {
        //5000
        List<double> arraySizes = Enumerable.Range( 1, 5000 ).Select( i => (double)i ).ToList();
        Dictionary<string, Tuple<Func<int[], int>, Func<int, double>, Func<double, string>>> dictionaryOfAlgorithms = new()
        {
            {
                "Insertion Sort",
                new Tuple<Func<int[], int>, Func<int, double>, Func<double, string>>(
                    SortingAlgorithms.SortingAlgorithms.InsertionSort, x => x * x, c => $"f(n) = {c:N2} * n^2" )
            },
            {
                "Merge Sort",
                new Tuple<Func<int[], int>, Func<int, double>, Func<double, string>>(
                    SortingAlgorithms.SortingAlgorithms.MergeSort, x => x * Math.Log10( x ),
                    c => $"f(n) = {c:N2} * n * log(n)" )
            },
            {
                "Heap Sort",
                new Tuple<Func<int[], int>, Func<int, double>, Func<double, string>>(
                    SortingAlgorithms.SortingAlgorithms.HeapSort, x => x * Math.Log10( x ),
                    c => $"f(n) = {c:N2} * n * log(n)" )
            },
            {
                "Quick Sort", new Tuple<Func<int[], int>, Func<int, double>, Func<double, string>>(
                    SortingAlgorithms.SortingAlgorithms.QuickSort,
                    x => x * Math.Log10( x ),
                    c => $"f(n) = {c:N2} * n * log(n)" )
            },
        };

        using REngineWrapper wrapper = new();
        wrapper.REngine.Evaluate( $"par(mfrow = c(2, {dictionaryOfAlgorithms.Count / 2}))" );
        NumericVector arraySizesNumericVector = wrapper.REngine.CreateNumericVector( arraySizes );
        wrapper.REngine.SetSymbol( "arraySizes", arraySizesNumericVector );

        foreach( KeyValuePair<string, Tuple<Func<int[], int>, Func<int, double>, Func<double, string>>> tmpSortingAlgorithmInfo in
                dictionaryOfAlgorithms )
        {
            string algorithmName = tmpSortingAlgorithmInfo.Key;
            Func<int[], int> tmpSortingAlgorithm = tmpSortingAlgorithmInfo.Value.Item1;
            Func<int, double> expectedAsymptoticFunction = tmpSortingAlgorithmInfo.Value.Item2;
            Func<double, string> asymptoticFunctionNameFunction = tmpSortingAlgorithmInfo.Value.Item3;

            List<double> numberOfOperationList = new();
            foreach( double arraySize in arraySizes )
            {
                int[] arrayToBeSorted = Utils.GetRandomArray( (int)arraySize );
                int tmpNumberOfOperations = tmpSortingAlgorithm( arrayToBeSorted );
                numberOfOperationList.Add( tmpNumberOfOperations );
            }

            NumericVector numberOfOperationsNumericVector = wrapper.REngine.CreateNumericVector( numberOfOperationList );
            wrapper.REngine.SetSymbol( "operations", numberOfOperationsNumericVector );

            //calculate C
            int indexForCalculation = arraySizes.Count / 2;
            int arraySizeForCalculation = (int)arraySizes[indexForCalculation];
            double c = numberOfOperationList[indexForCalculation] / expectedAsymptoticFunction( arraySizeForCalculation );
            c *= 10.0 / 9.0;
            NumericVector asymptoticFunctionNumericVector = wrapper.REngine.CreateNumericVector( arraySizes.Select( x =>
                c * expectedAsymptoticFunction( (int)x ) ) );
            wrapper.REngine.SetSymbol( "asymptoticValues", asymptoticFunctionNumericVector );

            string name = $"{algorithmName} vs\r\n {asymptoticFunctionNameFunction( c )}";
            string plotRCode = "plot(arraySizes,operations, type=\"l\"," +
                               $"main=\"{name}\", xlab=\"Array length\", ylab=\"Number of operations\"," +
                               "col=\"red\")\r\n" +
                               "lines(arraySizes, asymptoticValues, col=\"blue\", lty=\"dashed\")";
            wrapper.REngine.Evaluate( plotRCode );
        }
    }
}