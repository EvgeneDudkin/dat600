using System.Collections.Generic;
using Graph.Algorithms;
using Graph.Models;

namespace MaximumFlow;

class Program
{
    static void Main( string[] args )
    {
        AdjacencySetGraph graph = new(new List<VertexBase>
        {
            new(1, "S"),
            new(2, "V1"),
            new(3, "V2"),
            new(4, "V3"),
            new(5, "V4"),
            new(6, "V5"),
            new(7, "V6"),
            new(8, "T"),
        }, true);
        graph.AddEdge( 1, 2, 14 ); //S - V1
        graph.AddEdge( 1, 3, 25 ); //S - V2
        graph.AddEdge( 2, 4, 3 ); //V1 - V3
        graph.AddEdge( 2, 5, 21 ); //V1 - V4
        graph.AddEdge( 3, 4, 13 ); //V2 - V3
        graph.AddEdge( 3, 6, 7 ); //V2 - V5
        graph.AddEdge( 4, 6, 15 ); //V3 - V5
        graph.AddEdge( 4, 7, 6 ); //V3 - V6
        graph.AddEdge( 7, 2, 6 ); //V6 - V1
        graph.AddEdge( 5, 4, 10 ); //V4 - V3
        graph.AddEdge( 5, 8, 20 ); //V4 - t
        graph.AddEdge( 6, 8, 10 ); //V5 - t
        graph.AddEdge( 6, 5, 5 ); //V5 - V4

        graph.Display();

        FordFulkerson ff = new(graph, 1, 8);
        GraphBase flowNetwork = ff.GetMaximumFlowNetwork();
        flowNetwork.Display();
    }
}