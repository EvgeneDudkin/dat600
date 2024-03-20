using System.Collections.Generic;
using System.Linq;
using Graph.Models;

namespace Graph.Algorithms
{
    /// <summary>
    /// Strongly connected components search algorithm implementation 
    /// </summary>
    public static class StronglyConnectedComponents
    {
        /// <summary>
        /// Prints SCC
        /// </summary>
        public static void PrintStronglyConnectedComponents( GraphBase graph )
        {
            //DFS
            DepthFirstSearch dfs = new(graph);

            //G_t
            AdjacencySetGraph transposedGraph = new(graph.Vertices, graph.IsDirected);
            foreach( VertexBase vertex in graph.Vertices )
            {
                foreach( int adjacentVertexId in graph.GetAdjacentVertices( vertex.Id ) )
                {
                    int weight = graph.GetEdgeWeight( vertex.Id, adjacentVertexId );
                    transposedGraph.AddEdge( adjacentVertexId, vertex.Id, weight );
                }
            }

            //DFS G_t
            HashSet<int> mainLoopOrder = new(dfs.VerticesTimes.OrderByDescending( x => x.FinishTime ).Select( x => x.Id ));
            dfs = new(transposedGraph, mainLoopOrder);
            dfs.PrintDepthFirstForest();
        }
    }
}