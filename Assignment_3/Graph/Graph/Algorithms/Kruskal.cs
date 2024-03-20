using System.Collections.Generic;
using System.Linq;
using Graph.Models;

namespace Graph.Algorithms
{
    /// <summary>
    /// Kruskal algorithm implementation 
    /// </summary>
    public class Kruskal
    {
        public Kruskal( GraphBase graph, List<MaxEdgeCountRestriction> restrictions = null )
        {
            _graph = graph;
            _vertexMaxEdgeCountMap = restrictions?.ToDictionary( x => x.VertexId, x => x.MaxEdgeCount );
        }

        public GraphBase Mst()
        {
            //initialization
            _setVerticesMap.Clear();
            _vertexSetMap.Clear();
            foreach( VertexBase graphVertex in _graph.Vertices )
            {
                _setVerticesMap[graphVertex.Id] = new List<int> { graphVertex.Id };
                _vertexSetMap[graphVertex.Id] = graphVertex.Id;
            }

            List<(int, int, int)> edges = new();
            List<VertexBase> treeVertices = new();
            Dictionary<int, int> vertexEdgeCount = new();

            foreach( VertexBase tmpVertex in _graph.Vertices )
            {
                foreach( int adjacentVertexId in _graph.GetAdjacentVertices( tmpVertex.Id ) )
                {
                    edges.Add( (tmpVertex.Id, adjacentVertexId, _graph.GetEdgeWeight( tmpVertex.Id, adjacentVertexId )) );
                }

                treeVertices.Add( tmpVertex );
                vertexEdgeCount[tmpVertex.Id] = 0;
            }

            GraphBase minSpanningTree = new AdjacencySetGraph( treeVertices, _graph.IsDirected );

            //monotonically increasing order by weight
            edges = edges.OrderBy( x => x.Item3 ).ToList();

            foreach( (int, int, int) tmpEdge in edges )
            {
                int fromVertexId = tmpEdge.Item1;
                int toVertexId = tmpEdge.Item2;

                if( _vertexMaxEdgeCountMap != null && ((_vertexMaxEdgeCountMap.ContainsKey( fromVertexId ) &&
                                                        _vertexMaxEdgeCountMap[fromVertexId] < vertexEdgeCount[
                                                            fromVertexId] + 1) ||
                                                       (_vertexMaxEdgeCountMap.ContainsKey( toVertexId ) &&
                                                        _vertexMaxEdgeCountMap[toVertexId] < vertexEdgeCount[
                                                            toVertexId] + 1)) )
                    continue; //cannot take due to restrictions

                int firstSet = GetSet( fromVertexId );
                int secondSet = GetSet( toVertexId );
                if( firstSet != secondSet )
                {
                    Union( firstSet, secondSet );
                    minSpanningTree.AddEdge( fromVertexId, toVertexId, tmpEdge.Item3 );
                    vertexEdgeCount[fromVertexId]++;
                    vertexEdgeCount[toVertexId]++;
                }
            }

            return minSpanningTree;
        }

        private int GetSet( int vertexId )
        {
            return _vertexSetMap[vertexId];
        }

        private void Union( int sourceId, int targetId )
        {
            List<int> sourceVertices = _setVerticesMap[sourceId];
            foreach( int sourceVertexId in sourceVertices )
            {
                _vertexSetMap[sourceVertexId] = targetId;
            }

            _setVerticesMap[targetId].AddRange( sourceVertices );
            _setVerticesMap.Remove( sourceId );
        }

        public class MaxEdgeCountRestriction
        {
            public int VertexId { get; set; }
            public int MaxEdgeCount { get; set; }
        }

        private readonly GraphBase _graph;
        private readonly Dictionary<int, int> _vertexMaxEdgeCountMap;
        private readonly Dictionary<int, List<int>> _setVerticesMap = new();
        private readonly Dictionary<int, int> _vertexSetMap = new();
    }
}