using Knapsack.Models;

namespace Knapsack.Auxiliary
{
    /// <summary>
    /// Knapsack Utilities
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Solves 0-1 knapsack problem
        /// </summary>
        /// <remarks>
        /// Prints the solution of the problem
        /// </remarks>
        /// <param name="items">Items (with weights and prices)</param>
        /// <param name="knapsackCapacity">Maximum capacity of the knapsack</param>
        public static void SolveZeroOneKnapsackProblem( List<Item> items, int knapsackCapacity )
        {
            int[,] matrix = new int[items.Count, knapsackCapacity + 1];
            for( int i = 0; i < items.Count; i++ )
            {
                matrix[i, 0] = 0;
            }

            for( int i = 0; i < items.Count; i++ )
            {
                for( int j = 1; j < knapsackCapacity + 1; j++ )
                {
                    Item tmpItem = items[i];
                    int itemPrice = tmpItem.Price;
                    int itemWeight = tmpItem.Weight;
                    int priceWithoutItem = i > 0 ? matrix[i - 1, j] : 0;

                    if( j >= itemWeight )
                    {
                        int tmp = i > 0 ? matrix[i - 1, j - itemWeight] : 0;
                        matrix[i, j] = Math.Max( tmp + itemPrice, priceWithoutItem );
                    }
                    else
                    {
                        matrix[i, j] = priceWithoutItem;
                    }
                }
            }

            int? maxProfit = null;
            int? index = null;
            for( int i = 1; i < knapsackCapacity + 1; i++ )
            {
                int tmpValue = matrix[items.Count - 1, i];
                if( !maxProfit.HasValue || tmpValue > maxProfit.Value )
                {
                    maxProfit = tmpValue;
                    index = i;
                }
            }

            //answer
            Console.WriteLine( $"Max. Profit: {maxProfit}" );
            if( index.HasValue )
            {
                Console.WriteLine( "Need to take:" );
                for( int i = items.Count - 1; i >= 0; i-- )
                {
                    if( (i > 0 && matrix[i, index.Value] != matrix[i - 1, index.Value]) || (i == 0 && matrix[i, index.Value] > 0) )
                    {
                        Console.Write( $"Item-{i + 1}   " );
                        index -= items[i].Weight;
                    }
                }
            }
        }

        /// <summary>
        /// Solves fractional knapsack problem
        /// </summary>
        /// <remarks>
        /// Prints the solution of the problem
        /// </remarks>
        /// <param name="items">Items (with weights and prices)</param>
        /// <param name="knapsackCapacity">Maximum capacity of the knapsack</param>
        public static void SolveFractionalKnapsackProblem( List<Item> items, int knapsackCapacity )
        {
            List<ItemExtendedInfo> itemsExtendedList = new();
            for( int i = 0; i < items.Count; i++ )
            {
                Item tmpItem = items[i];
                double density = (double)tmpItem.Price / tmpItem.Weight;
                itemsExtendedList.Add( new ItemExtendedInfo
                {
                    Price = tmpItem.Price,
                    Weight = tmpItem.Weight,
                    Density = density,
                    Index = i
                } );
            }

            itemsExtendedList.Sort( ( x, y ) => y.Density.CompareTo( x.Density ) );
            double maxProfit = 0.0;
            int index = 0;
            while( knapsackCapacity > 0 && index < itemsExtendedList.Count )
            {
                ItemExtendedInfo item = itemsExtendedList[index];
                double ratio = 1.0;
                if( item.Weight <= knapsackCapacity )
                {
                    knapsackCapacity -= item.Weight;
                    maxProfit += item.Price;
                }
                else
                {
                    ratio = knapsackCapacity / (double)item.Weight;
                    knapsackCapacity = 0;
                    maxProfit += ratio * item.Price;
                }

                Console.Write( $"{ratio:N2} * Item-{item.Index + 1}   " );
                index++;
            }

            Console.WriteLine();
            Console.WriteLine( $"Max. Profit: {maxProfit:N2}" );
        }

        private static void PrintMatrix( int[,] matrix )
        {
            for( int i = 0; i < matrix.GetLength( 0 ); i++ )
            {
                for( int j = 0; j < matrix.GetLength( 1 ); j++ )
                {
                    Console.Write( $"{matrix[i, j]} " );
                }

                Console.WriteLine();
            }
        }

        private class ItemExtendedInfo
        {
            public int Weight { get; set; }
            public int Price { get; set; }
            public int Index { get; set; }
            public double Density { get; set; }
        }
    }
}