using MatrixChainMultiplication.Auxiliary;

namespace MatrixChainMultiplication;

class Program
{
    public static void Main( string[] args )
    {
        //int[] dimensions = { 10, 5, 50, 2, 100, 10 };
        int[] dimensions = { 5,2,3,10 };

        Utils.SolveMatrixChainMultiplicationProblem( dimensions );
    }
}