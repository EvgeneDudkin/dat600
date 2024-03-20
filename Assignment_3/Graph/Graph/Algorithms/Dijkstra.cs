using System;
using System.Collections.Generic;
using System.Linq;
using Graph.Models;

namespace Graph.Algorithms
{
    /// <summary>
    /// Dijkstra's algorithm implementation
    /// </summary>
    public class Dijkstra : SingleSourceShortestPathAlgorithmBase
    {
        public Dijkstra( GraphBase graph, int sourceId ) : base( graph, sourceId )
        {
        }

        /// <summary>
        /// Computes shortest paths to all vertices from the source 
        /// </summary>
        protected override bool ComputeShortestPathsInternal()
        {
            PriorityQueue<int, int> queue = new();

            foreach( KeyValuePair<int, VertexInfo> pair in _verticesDistancesInfo )
            {
                queue.Enqueue( pair.Key, _sourceId == pair.Key ? 0 : Int32.MaxValue );
            }

            while( queue.Count > 0 )
            {
                int tmpVertexId = queue.Dequeue();
                foreach( int adjacentVertexId in _graph.GetAdjacentVertices( tmpVertexId ) )
                {
                    int weight = _graph.GetEdgeWeight( tmpVertexId, adjacentVertexId );
                    if( Relax( tmpVertexId, adjacentVertexId, weight ) )
                    {
                        //change priority
                        if( queue.UnorderedItems.Count( x => x.Element == adjacentVertexId ) != 0 )
                        {
                            queue = new PriorityQueue<int, int>( queue.UnorderedItems.Where( x => x.Element != adjacentVertexId ) );
                            queue.Enqueue( adjacentVertexId, _verticesDistancesInfo[adjacentVertexId].Distance.GetValueOrDefault( 0 ) );
                        }
                    }
                }
            }

            return true;
        }
    }
}