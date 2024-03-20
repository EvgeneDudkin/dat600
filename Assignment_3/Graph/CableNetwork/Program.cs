using System;
using System.Collections.Generic;
using Graph.Algorithms;
using Graph.Models;

namespace CableNetwork;

class Program
{
    static void Main( string[] args )
    {
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
        });
        graph.AddEdge( 1, 2, 5 ); // A - B
        graph.AddEdge( 1, 4, 1 ); // A - D
        graph.AddEdge( 2, 4, 4 ); // B - D
        graph.AddEdge( 2, 8, 8 ); // B - H
        graph.AddEdge( 3, 4, 2 ); // C - D
        graph.AddEdge( 3, 7, 6 ); // C - G
        graph.AddEdge( 4, 5, 2 ); // D - E
        graph.AddEdge( 4, 6, 4 ); // D - F
        graph.AddEdge( 5, 8, 8 ); // E - H
        graph.AddEdge( 6, 7, 9 ); // F - G
        graph.AddEdge( 6, 8, 7 ); // F - H
        graph.Display();

        //I
        SolveCableNetworkProblem( graph, null, 30 );

        //II. Restriction
        SolveCableNetworkProblem(graph, new List<Kruskal.MaxEdgeCountRestriction>()
        {
            new() { VertexId = 4, MaxEdgeCount = 3 }
        }, 30);

        SolveCableNetworkProblem( graph, null, 25, true );
    }

    private static void SolveCableNetworkProblem( GraphBase graph, List<Kruskal.MaxEdgeCountRestriction> restrictions, int budgetLimit,
                                                  bool isAdjustmentNeeded = false )
    {
        Kruskal kruskal = new(graph, restrictions);
        GraphBase minSpanningTree = kruskal.Mst();
        bool isWithinBudget = minSpanningTree.TotalWeight <= budgetLimit;
        string resultString = isWithinBudget ? "Yes! It's " : "No! It's not";
        Console.WriteLine(
            $"{resultString} possible to connect all the nodes in the network while still staying within the budget constraint" );
        minSpanningTree.Display();
        Console.WriteLine( $"Total weight: {minSpanningTree.TotalWeight}" );

        if( !isWithinBudget && isAdjustmentNeeded )
        {
            List<(int, int, int)> edges = new();
            Dictionary<int, string> _vertexNameMap = new();
            foreach( VertexBase tmpVertex in graph.Vertices )
            {
                _vertexNameMap[tmpVertex.Id] = tmpVertex.Name;
                foreach( int adjacentVertexId in graph.GetAdjacentVertices( tmpVertex.Id ) )
                {
                    edges.Add( (tmpVertex.Id, adjacentVertexId, graph.GetEdgeWeight( tmpVertex.Id, adjacentVertexId )) );
                }
            }

            for( int i = 0; i < edges.Count; i++ )
            {
                (int, int, int) firstEdge = edges[i];
                for( int j = i + 1; j < edges.Count; j++ )
                {
                    (int, int, int) secondEdge = edges[j];

                    graph.RemoveEdge( firstEdge.Item1, firstEdge.Item2 );
                    graph.AddEdge( firstEdge.Item1, firstEdge.Item2, secondEdge.Item3 );

                    graph.RemoveEdge( secondEdge.Item1, secondEdge.Item2 );
                    graph.AddEdge( secondEdge.Item1, secondEdge.Item2, firstEdge.Item3 );

                    //try to solve with new graph
                    kruskal = new(graph, restrictions);
                    minSpanningTree = kruskal.Mst();
                    isWithinBudget = minSpanningTree.TotalWeight <= budgetLimit;

                    if( isWithinBudget )
                    {
                        Console.WriteLine(
                            $"Need to swap edge ({_vertexNameMap[firstEdge.Item1]} <-> {_vertexNameMap[firstEdge.Item2]}) with ({_vertexNameMap[secondEdge.Item1]} <-> {_vertexNameMap[secondEdge.Item2]})" );
                        SolveCableNetworkProblem( graph, restrictions, budgetLimit );
                        return;
                    }

                    //return back
                    graph.AddEdge( firstEdge.Item1, firstEdge.Item2, firstEdge.Item3 );
                    graph.AddEdge( secondEdge.Item1, secondEdge.Item2, secondEdge.Item3 );
                }
            }
        }
    }
}