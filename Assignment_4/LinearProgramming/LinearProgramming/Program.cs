using Google.OrTools.LinearSolver;

namespace LinearProgramming;

class Program
{
    static void Main()
    {
        //I
        //SolveExamProblem();

        //II
        //SolveBottleneckProblem();

        //III
        SolveMaxFlowProblem();
    }

    private static void SolveExamProblem()
    {
        Solver solver = Solver.CreateSolver( "GLOP" );
        Variable x = solver.MakeNumVar( 0.0, double.PositiveInfinity, "x" );
        Variable y = solver.MakeNumVar( 0.0, double.PositiveInfinity, "y" );
        // Maximize 505/3 x + 770 / 3 y
        Objective objective = solver.Objective();
        objective.SetCoefficient( x, 505.0 / 3.0 );
        objective.SetCoefficient( y, 770.0 / 3.0 );
        objective.SetMaximization();

        // x >= 10 
        Constraint c0 = solver.MakeConstraint( 10, double.PositiveInfinity );
        c0.SetCoefficient( x, 1 );

        // y >= 0
        Constraint c1 = solver.MakeConstraint( 0.0, double.PositiveInfinity );
        c1.SetCoefficient( y, 1 );

        //1/4 x + 1/3 y <= 40
        Constraint c2 = solver.MakeConstraint( 0, 40 );
        c2.SetCoefficient( x, 1.0 / 4.0 );
        c2.SetCoefficient( y, 1.0 / 3.0 );

        //1/3 x + 1/2 y <= 35
        Constraint c3 = solver.MakeConstraint( 0, 35 );
        c3.SetCoefficient( x, 1.0 / 3.0 );
        c3.SetCoefficient( y, 1.0 / 2.0 );

        SolveProblem( solver );
    }

    private static void SolveBottleneckProblem()
    {
        Solver solver = Solver.CreateSolver( "GLOP" );
        Variable d_s_1 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_s_1" );
        Variable d_s_2 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_s_2" );
        Variable d_1_3 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_1_3" );
        Variable d_1_4 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_1_4" );
        Variable d_2_3 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_2_3" );
        Variable d_2_5 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_2_5" );
        Variable d_3_1 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_3_1" );
        Variable d_3_5 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_3_5" );
        Variable d_4_3 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_4_3" );
        Variable d_4_t = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_4_t" );
        Variable d_5_t = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_5_t" );
        Variable d_5_4 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "d_5_4" );

        Variable v_1 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "v_1" );
        Variable v_2 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "v_2" );
        Variable v_3 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "v_3" );
        Variable v_4 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "v_4" );
        Variable v_5 = solver.MakeNumVar( 0.0, double.PositiveInfinity, "v_5" );


        Objective objective = solver.Objective();
        objective.SetCoefficient( d_s_1, 14 );
        objective.SetCoefficient( d_s_2, 25 );
        objective.SetCoefficient( d_1_3, 3 );
        objective.SetCoefficient( d_1_4, 21 );
        objective.SetCoefficient( d_2_3, 13 );
        objective.SetCoefficient( d_2_5, 7 );
        objective.SetCoefficient( d_3_1, 6 );
        objective.SetCoefficient( d_3_5, 15 );
        objective.SetCoefficient( d_4_3, 10 );
        objective.SetCoefficient( d_4_t, 20 );
        objective.SetCoefficient( d_5_t, 10 );
        objective.SetCoefficient( d_5_4, 5 );
        objective.SetMinimization();

        Constraint c1 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c1.SetCoefficient( d_1_3, 1 );
        c1.SetCoefficient( v_1, -1 );
        c1.SetCoefficient( v_3, 1 );

        Constraint c2 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c2.SetCoefficient( d_1_4, 1 );
        c2.SetCoefficient( v_1, -1 );
        c2.SetCoefficient( v_4, 1 );

        Constraint c3 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c3.SetCoefficient( d_2_3, 1 );
        c3.SetCoefficient( v_2, -1 );
        c3.SetCoefficient( v_3, 1 );

        Constraint c4 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c4.SetCoefficient( d_2_5, 1 );
        c4.SetCoefficient( v_2, -1 );
        c4.SetCoefficient( v_5, 1 );

        Constraint c5 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c5.SetCoefficient( d_3_1, 1 );
        c5.SetCoefficient( v_3, -1 );
        c5.SetCoefficient( v_1, 1 );

        Constraint c6 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c6.SetCoefficient( d_3_5, 1 );
        c6.SetCoefficient( v_3, -1 );
        c6.SetCoefficient( v_5, 1 );

        Constraint c7 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c7.SetCoefficient( d_4_3, 1 );
        c7.SetCoefficient( v_4, -1 );
        c7.SetCoefficient( v_3, 1 );

        Constraint c8 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c8.SetCoefficient( d_5_4, 1 );
        c8.SetCoefficient( v_5, -1 );
        c8.SetCoefficient( v_4, 1 );

        Constraint c9 = solver.MakeConstraint( 1, double.PositiveInfinity );
        c9.SetCoefficient( d_s_1, 1 );
        c9.SetCoefficient( v_1, 1 );
        Constraint c10 = solver.MakeConstraint( 1, double.PositiveInfinity );
        c10.SetCoefficient( d_s_2, 1 );
        c10.SetCoefficient( v_2, 1 );
        Constraint c11 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c11.SetCoefficient( d_4_t, 1 );
        c11.SetCoefficient( v_4, -1 );
        Constraint c12 = solver.MakeConstraint( 0, double.PositiveInfinity );
        c12.SetCoefficient( d_5_t, 1 );
        c12.SetCoefficient( v_5, -1 );
        SolveProblem( solver );
    }

    private static void SolveMaxFlowProblem()
    {
        Solver solver = Solver.CreateSolver( "GLOP" );
        Variable x_s_1 = solver.MakeNumVar( 0.0, 14, "x_s_1" );
        Variable x_s_2 = solver.MakeNumVar( 0.0, 25, "x_s_2" );
        Variable x_1_3 = solver.MakeNumVar( 0.0, 3, "x_1_3" );
        Variable x_1_4 = solver.MakeNumVar( 0.0, 21, "x_1_4" );
        Variable x_2_3 = solver.MakeNumVar( 0.0, 13, "x_2_3" );
        Variable x_2_5 = solver.MakeNumVar( 0.0, 7, "x_2_5" );
        Variable x_3_1 = solver.MakeNumVar( 0.0, 6, "x_3_1" );
        Variable x_3_5 = solver.MakeNumVar( 0.0, 15, "x_3_5" );
        Variable x_4_3 = solver.MakeNumVar( 0.0, 10, "x_4_3" );
        Variable x_4_t = solver.MakeNumVar( 0.0, 20, "x_4_t" );
        Variable x_5_t = solver.MakeNumVar( 0.0, 10, "x_5_t" );
        Variable x_5_4 = solver.MakeNumVar( 0.0, 5, "x_5_4" );

        Objective objective = solver.Objective();
        objective.SetCoefficient( x_s_1, 1 );
        objective.SetCoefficient( x_s_2, 1 );
        objective.SetCoefficient(x_1_3, 1);
        objective.SetCoefficient(x_1_4, 1);
        objective.SetCoefficient(x_2_3, 1);
        objective.SetCoefficient(x_2_5, 1);
        objective.SetCoefficient(x_3_1, 1);
        objective.SetCoefficient(x_3_5, 1);
        objective.SetCoefficient(x_4_3, 1);
        objective.SetCoefficient(x_4_t, 1);
        objective.SetCoefficient(x_5_t, 1);
        objective.SetCoefficient(x_5_4, 1);
        objective.SetMaximization();

        // v1: 
        Constraint c1 = solver.MakeConstraint( 0, 0 );
        c1.SetCoefficient( x_s_1, 1 );
        c1.SetCoefficient( x_3_1, 1 );
        c1.SetCoefficient( x_1_4, -1 );
        c1.SetCoefficient( x_1_3, -1 );

        // v2: 
        Constraint c2 = solver.MakeConstraint( 0, 0 );
        c2.SetCoefficient( x_s_2, 1 );
        c2.SetCoefficient( x_2_3, -1 );
        c2.SetCoefficient( x_2_5, -1 );

        // v3: 
        Constraint c3 = solver.MakeConstraint( 0, 0 );
        c3.SetCoefficient( x_1_3, 1 );
        c3.SetCoefficient( x_2_3, 1 );
        c3.SetCoefficient( x_4_3, 1 );
        c3.SetCoefficient( x_3_1, -1 );
        c3.SetCoefficient( x_3_5, -1 );

        // v4: 
        Constraint c4 = solver.MakeConstraint( 0, 0 );
        c4.SetCoefficient( x_1_4, 1 );
        c4.SetCoefficient( x_5_4, 1 );
        c4.SetCoefficient( x_4_3, -1 );
        c4.SetCoefficient( x_4_t, -1 );

        // v5: 
        Constraint c5 = solver.MakeConstraint( 0, 0 );
        c5.SetCoefficient( x_3_5, 1 );
        c5.SetCoefficient( x_2_5, 1 );
        c5.SetCoefficient( x_5_4, -1 );
        c5.SetCoefficient( x_5_t, -1 );

        SolveProblem( solver );
    }

    //static void Main()
    //{
    //    Solver solver = Solver.CreateSolver( "GLOP" );
    //    Variable x = solver.MakeNumVar( 0.0, double.PositiveInfinity, "x" );
    //    Variable y = solver.MakeNumVar( 0.0, double.PositiveInfinity, "y" );
    //    // Maximize 2*y+x.
    //    Objective objective = solver.Objective();
    //    objective.SetCoefficient( x, 1 );
    //    objective.SetCoefficient( y, 2 );
    //    objective.SetMaximization();

    //    // 0 <= x <= 15 
    //    Constraint c0 = solver.MakeConstraint( 0, 15 );
    //    c0.SetCoefficient( x, 1 );

    //    // 0 <= y <= 8
    //    Constraint c1 = solver.MakeConstraint( 0, 8 );
    //    c1.SetCoefficient( y, 1 );
    //    SolveProblem( solver );
    //}

    private static void SolveProblem( Solver solver )
    {
        Solver.ResultStatus resultStatus = solver.Solve();

        // Check that the problem has an optimal solution.
        if( resultStatus != Solver.ResultStatus.OPTIMAL )
        {
            Console.WriteLine( "The problem does not have an optimal solution!" );
            return;
        }

        Console.WriteLine( "Problem solved in " + solver.WallTime() + " milliseconds" );

        // The objective value of the solution.
        Console.WriteLine( "Optimal objective value = " + solver.Objective().Value() );
        //Improved solution display
        DisplaySolution( solver.variables() );
    }

    private static void DisplaySolution( MPVariableVector variables )
    {
        foreach( var tmpVariable in variables.Select( a => new { Name = a.Name(), Value = a.SolutionValue() } ) )
        {
            Console.WriteLine( $"{tmpVariable.Name}: {tmpVariable.Value}" );
        }
    }
}