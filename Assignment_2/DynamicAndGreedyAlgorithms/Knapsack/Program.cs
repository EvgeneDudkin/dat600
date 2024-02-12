using Knapsack.Auxiliary;
using Knapsack.Models;

namespace Knapsack;

class Program
{
    public static void Main( string[] args )
    {
        int numberOfItems = GetNextValue( "Number of items" );

        List<Item> items = new();
        while( items.Count != numberOfItems )
        {
            int nextItemIndex = items.Count + 1;
            int weight = GetNextValue( $"weight of {nextItemIndex} item" );
            int price = GetNextValue( $"price of {nextItemIndex} item" );
            items.Add( new Item( weight, price ) );
        }

        int maxCapacity = GetNextValue( "the maximum capacity of the knapsack" );
        Console.WriteLine( "0/1 knapsack problem:" );
        Utils.SolveZeroOneKnapsackProblem( items, maxCapacity );
        Console.WriteLine();

        Console.WriteLine( "fractional knapsack problem:" );
        Utils.SolveFractionalKnapsackProblem( items, maxCapacity );
    }

    private static int GetNextValue( string valueName )
    {
        while( true )
        {
            Console.WriteLine( $"Enter {valueName}:" );
            string? numberOfItemsString = Console.ReadLine();
            if( string.IsNullOrEmpty( numberOfItemsString ) || !Int32.TryParse( numberOfItemsString, out int value ) ||
                value <= 0 )
            {
                Console.WriteLine( $"Sorry,{valueName} is invalid!" );
                continue;
            }

            return value;
        }
    }
}