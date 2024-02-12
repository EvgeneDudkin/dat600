using CoinChange.Auxiliary;

namespace CoinChange;

class Program
{
    static void Main( string[] args )
    {
        //Utils.SolveCoinProblemWithGreedyApproach( new[] { 1, 5, 11 }, 15 );
        Utils.SolveCoinProblemWithDynamicApproach( new[] { 1, 5, 11 }, 15 );
    }
}