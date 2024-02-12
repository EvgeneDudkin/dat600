namespace CoinChange.Auxiliary
{
    /// <summary>
    /// Coin Change Utilities
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Solves coin problem with greedy approach
        /// </summary>
        /// <remarks>
        /// Determines the fewest coins needed to achieve a total
        /// </remarks>
        /// <param name="coins">Coins</param>
        /// <param name="total">Needed total</param>
        public static void SolveCoinProblemWithGreedyApproach( int[] coins, int total )
        {
            //<coin, count>
            int n = total;
            Dictionary<int, int> solution = new();
            for( int i = coins.Length - 1; i >= 0; i-- )
            {
                int coin = coins[i];
                if( coin > total )
                    continue;

                int number = total / coin;
                solution[coin] = number;
                total -= number * coin;
            }

            Console.WriteLine( "Greedy solution:" );
            int count = solution.Sum( x => x.Value );
            Console.WriteLine( $"Fewest coins needed to achieve {n} - {count}" );
            foreach( KeyValuePair<int, int> keyValuePair in solution )
            {
                Console.Write( $"{keyValuePair.Value} of {keyValuePair.Key}   " );
            }
        }

        /// <summary>
        /// Solves coin problem with dynamic approach
        /// </summary>
        /// <remarks>
        /// Determines the fewest coins needed to achieve a total
        /// </remarks>
        /// <param name="coins">Coins</param>
        /// <param name="total">Needed total</param>
        public static void SolveCoinProblemWithDynamicApproach( int[] coins, int total )
        {
            int[] values = new int[total + 1];
            values[0] = 0;
            for( int i = 1; i < values.Length; i++ )
                values[i] = Int32.MaxValue;

            for( int i = 1; i < values.Length; i++ )
            {
                for( int j = 0; j < coins.Length; j++ )
                {
                    int coin = coins[j];
                    if( coin <= i )
                    {
                        int tmpValue = values[i - coin];
                        if( tmpValue != Int32.MaxValue && tmpValue + 1 < values[i] )
                            values[i] = tmpValue + 1;
                    }
                }
            }

            Console.WriteLine( "Dynamic solution:" );
            Console.WriteLine( $"Fewest coins needed to achieve {total} - {values[^1]}" );
        }
    }
}