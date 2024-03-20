using System;
using System.Collections.Generic;
using System.Linq;
using Graph.Algorithms;
using Graph.Models;

namespace FindingChampion;

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
        }, true);
        graph.AddEdge( 1, 2 ); // A - B
        graph.AddEdge( 1, 4 ); // A - D
        graph.AddEdge( 2, 1 ); // B - A
        graph.AddEdge( 2, 3 ); // B - C
        graph.AddEdge( 4, 2 ); // D - B
        graph.AddEdge( 4, 3 ); // D - C
        graph.AddEdge( 3, 5 ); // C - E
        graph.AddEdge( 3, 6 ); // C - F
        graph.AddEdge( 6, 5 ); // F - E
        graph.AddEdge( 5, 7 ); // E - G
        graph.AddEdge( 7, 6 ); // G - F

        graph.Display();

        //I
        List<VertexBase> champions = new();
        BreadthFirstSearch bfs = new(graph);

        foreach (VertexBase vertex in graph.Vertices)
        {
            bfs.BFS(vertex.Id);
            bool isChampion = true;
            foreach (VertexBase tmpVertex in graph.Vertices)
            {
                if (vertex.Id == tmpVertex.Id)
                    continue;

                if (!bfs.IsPath(vertex.Id, tmpVertex.Id))
                {
                    isChampion = false;
                    break;
                }
            }

            if (isChampion)
                champions.Add(vertex);
        }

        PrintChampions(champions);

        //II
        StronglyConnectedComponents.PrintStronglyConnectedComponents( graph );
    }

    private static void PrintChampions( List<VertexBase> champions )
    {
        if( champions.Count == 0 )
        {
            Console.WriteLine( "There are no champions!" );
        }
        else
        {
            Console.WriteLine( $"Champions: {string.Join( ",", champions.Select( x => x.Name ) )}" );
        }
    }
}