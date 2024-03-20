using System;
using System.Collections.Generic;
using Graph.Algorithms;
using Graph.Models;

namespace BasicGraph;

class Program
{
    static void Main( string[] args )
    {
        //I
        //1
        AdjacencyMatrixGraph matrixGraph = new(new[,]
        {
            { 0, 1, 0, 0, 0, 0 },
            { 0, 1, 1, 1, 0, 0 },
            { 1, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 1, 1 },
            { 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 1, 0, 0 },
        }, true);
        AdjacencySetGraph setGraph = new(matrixGraph);
        matrixGraph.Display();

        //////2
        setGraph.Display();

        //3
        AdjacencySetGraph graph = new(new List<VertexBase>
        {
            new(1, "A"),
            new(2, "B"),
            new(3, "C"),
            new(4, "D"),
            new(5, "E"),
            new(6, "F"),
            new(7, "G"),
            new(8, "H"),
            new(9, "I"),
            new(10, "J"),
        }, true);
        graph.AddEdge( 1, 2 ); // A - B
        graph.AddEdge( 2, 3 ); // B - C
        graph.AddEdge( 2, 4 ); // B - D
        graph.AddEdge( 3, 5 ); // C - E
        graph.AddEdge( 3, 6 ); // C - F
        graph.AddEdge( 4, 5 ); // D - E
        graph.AddEdge( 4, 6 ); // D - F
        graph.AddEdge( 6, 2 ); // F - B
        graph.AddEdge( 5, 6 ); // E - F
        graph.AddEdge( 5, 7 ); // E - G
        graph.AddEdge( 6, 7 ); // F - G
        graph.AddEdge( 5, 10 ); // E - J
        graph.AddEdge( 6, 8 ); // F - H
        graph.AddEdge( 6, 10 ); // F - J
        graph.AddEdge( 8, 9 ); // H - I
        graph.AddEdge( 10, 9 ); // J - I

        graph.Display();

        //II
        //BFS
        BreadthFirstSearch bfs = new(graph, 1);
        bfs.PrintVisitingOrder();
        bfs.PrintPath( 1, 10 );

        //DFS
        DepthFirstSearch dfs = new(graph);
        dfs.PrintDfsVisitsInfo();

        //III
        graph.RemoveEdge( 6, 2 );
        dfs.DFS();
        dfs.PrintTopologicalSort();

        //IV
        graph.AddEdge( 9, 3 ); // I - C
        graph.AddEdge( 3, 1 ); // C - A
        dfs.DFS();
        if( IsDag( dfs, graph ) )
            dfs.PrintTopologicalSort();
    }

    private static bool IsDag( DepthFirstSearch dfs, GraphBase graph )
    {
        bool isDag = dfs.IsDag( out List<(VertexBase, VertexBase)> backEdges );
        if( !isDag && backEdges.Count > 0 )
        {
            Console.WriteLine( "Graph is not a DAG!" );
            Console.WriteLine( "Back Edges should be removed:" );
            foreach( (VertexBase, VertexBase) tuple in backEdges )
            {
                Console.WriteLine( $"{tuple.Item1.Name} -> {tuple.Item2.Name}" );
            }

            Console.WriteLine( "Do you want to transform it into DAG? Y/N" );
            //read key
            ConsoleKey consoleKey = Console.ReadKey().Key;
            if( ConsoleKey.Y == consoleKey )
            {
                foreach( (VertexBase, VertexBase) tuple in backEdges )
                {
                    graph.RemoveEdge( tuple.Item1.Id, tuple.Item2.Id );
                }

                dfs.DFS();
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}