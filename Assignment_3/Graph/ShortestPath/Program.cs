using System.Collections.Generic;
using Graph.Algorithms;
using Graph.Models;

namespace ShortestPath;

class Program
{
    static void Main( string[] args )
    {
        //Dijkstra algorithm correct example
        //AdjacencySetGraph graph = new(new List<VertexBase>
        //{
        //    new(1, "A"),
        //    new(2, "B"),
        //    new(3, "C"),
        //    new(4, "D"),
        //    new(5, "E")
        //}, true);
        //graph.AddEdge(1, 2, 10); // A - B
        //graph.AddEdge(2, 3, 1); // B - C
        //graph.AddEdge(1, 4, 5); // A - D
        //graph.AddEdge(1, 2, 10); // A - B
        //graph.AddEdge(2, 4, 2); // B - D
        //graph.AddEdge(4, 2, 3); // D - B
        //graph.AddEdge(4, 3, 9); // D - C
        //graph.AddEdge(4, 5, 2); // D - E
        //graph.AddEdge(5, 1, 7); // E - A
        //graph.AddEdge(3, 5, 4); // C - E
        //graph.AddEdge(5, 3, 6); // E - C
        //graph.Display();
        //Dijkstra d = new(graph, 1);

        //I
        AdjacencySetGraph graph = new(new List<VertexBase>
        {
            new(1, "A"),
            new(2, "B"),
            new(3, "C"),
            new(4, "D"),
        }, true);
        graph.AddEdge(1, 2, 1); //A - B
        graph.AddEdge(1, 3, 4); //A - C
        graph.AddEdge(3, 2, -5); //C - B
        graph.AddEdge(2, 4, 1); //B - D
        //graph.Display();

        //Dijkstra d = new(graph, 1);

        //II
        //AdjacencySetGraph graph = new(new List<VertexBase>
        //{
        //    new(1, "A"),
        //    new(2, "B"),
        //    new(3, "C"),
        //    new(4, "D"),
        //}, true);
        //graph.AddEdge(1, 2, 1); //A - B
        //graph.AddEdge(1, 3, 4); //A - C
        //graph.AddEdge(3, 2, -5); //C - B
        //graph.AddEdge(2, 4, 1); //B - D
        //graph.Display();

        BellmanFord bf = new(graph, 1);
    }
}