namespace MatrixChainMultiplication.Auxiliary
{
    /// <summary>
    /// Matrix Chain Multiplication Utilities
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Solves Matrix Chain Multiplication Problem
        /// </summary>
        /// <remarks>
        /// Prints parenthesization in matrix chain multiplication along with optimal number of operations 
        /// </remarks>
        /// <param name="dimensions">Matrices dimensions</param>
        public static void SolveMatrixChainMultiplicationProblem( int[] dimensions )
        {
            int matricesCount = dimensions.Length - 1;
            if( matricesCount <= 0 )
                throw new ArgumentException( $"Invalid parameter {nameof(dimensions)}" );

            McmItem[,] helperArray = new McmItem[matricesCount, matricesCount];
            for( int j = 0; j < matricesCount; j++ )
            {
                helperArray[j, j] = new McmItem( 0, j );
                for( int i = j - 1; i >= 0; i-- )
                {
                    int? optimalValue = null;
                    int? index = null;
                    for( int k = i; k < j; k++ )
                    {
                        int tmpNumberOfOperations = dimensions[i] * dimensions[k + 1] * dimensions[j + 1];
                        int tmpValue = helperArray[i, k].OptimalNumberOfOperations + helperArray[k + 1, j].OptimalNumberOfOperations +
                                       tmpNumberOfOperations;
                        if( !optimalValue.HasValue || tmpValue < optimalValue.Value )
                        {
                            optimalValue = tmpValue;
                            index = k;
                        }
                    }

                    if( !optimalValue.HasValue || !index.HasValue )
                        throw new ArgumentException( "Something went wrong" );

                    helperArray[i, j] = new McmItem( optimalValue.Value, index.Value );
                }
            }

            Console.WriteLine( "Problem:" );
            PrintProblem( dimensions );
            Console.WriteLine();

            Console.WriteLine( "Solution:" );
            PrintOptimalParens( helperArray, 0, matricesCount - 1 );
            Console.WriteLine();

            Console.WriteLine( $"Optimal number of operations: {helperArray[0, matricesCount - 1].OptimalNumberOfOperations}" );
        }

        private static void PrintProblem( int[] dimensions )
        {
            for( int i = 1; i < dimensions.Length; i++ )
            {
                Console.Write( $"A{i}[{dimensions[i - 1]}x{dimensions[i]}]" );
            }
        }

        private static void PrintOptimalParens( McmItem[,] helperArray, int i, int j )
        {
            if( i == j )
                Console.Write( $"A{i + 1}" );
            else
            {
                Console.Write( "(" );
                PrintOptimalParens( helperArray, i, helperArray[i, j].ParenthesisIndex );
                PrintOptimalParens( helperArray, helperArray[i, j].ParenthesisIndex + 1, j );
                Console.Write( ")" );
            }
        }

        private class McmItem
        {
            public McmItem( int optimalNumberOfOperations, int parenthesisIndex )
            {
                OptimalNumberOfOperations = optimalNumberOfOperations;
                ParenthesisIndex = parenthesisIndex;
            }

            public int OptimalNumberOfOperations { get; }
            public int ParenthesisIndex { get; }
        }
    }
}