using Graph.Models;

namespace Graph.Algorithms
{
    /// <summary>
    /// Bellman-Ford algorithm implementation
    /// </summary>
    public class BellmanFord : SingleSourceShortestPathAlgorithmBase
    {
        public BellmanFord( GraphBase graph, int sourceId ) : base( graph, sourceId )
        {
        }

        /// <summary>
        /// Computes shortest paths to all vertices from the source 
        /// </summary>
        protected override bool ComputeShortestPathsInternal()
        {
            for( int i = 0; i < _graph.VerticesCount - 1; i++ )
            {
                foreach( VertexBase vertex in _graph.Vertices )
                {
                    foreach( int adjacentVertexId in _graph.GetAdjacentVertices( vertex.Id ) )
                    {
                        int weight = _graph.GetEdgeWeight( vertex.Id, adjacentVertexId );
                        Relax( vertex.Id, adjacentVertexId, weight );
                    }
                }
            }

            foreach( VertexBase vertex in _graph.Vertices )
            {
                foreach( int adjacentVertexId in _graph.GetAdjacentVertices( vertex.Id ) )
                {
                    int weight = _graph.GetEdgeWeight( vertex.Id, adjacentVertexId );
                    if( _verticesDistancesInfo[adjacentVertexId].Distance > _verticesDistancesInfo[vertex.Id].Distance + weight )
                        return false;
                }
            }

            return true;
        }
    }
}