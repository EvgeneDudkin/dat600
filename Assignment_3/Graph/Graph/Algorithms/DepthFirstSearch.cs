using Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Algorithms
{
    /// <summary>
    ///Depth-first search algorithm
    /// </summary>
    public class DepthFirstSearch
    {
        public DepthFirstSearch( GraphBase graph, HashSet<int> mainLoopOrder = null )
        {
            _graph = graph;

            _vertices = new();
            foreach( VertexBase graphVertex in graph.Vertices )
            {
                _vertices[graphVertex.Id] = new VertexInfo
                {
                    Id = graphVertex.Id,
                    Name = graphVertex.Name,
                    DiscoveryTime = null,
                    FinishTime = null,
                    ParentId = null,
                    VertexColor = VertexInfo.Color.White
                };
            }

            DFS( mainLoopOrder );
        }

        /// <summary>
        /// Vertices with their times info
        /// </summary>
        public List<VertexTimeInfo> VerticesTimes
        {
            get
            {
                return _vertices.Select( x => new VertexTimeInfo
                {
                    Id = x.Key,
                    DiscoveryTime = x.Value.DiscoveryTime.GetValueOrDefault( 0 ),
                    FinishTime = x.Value.FinishTime.GetValueOrDefault( 0 ),
                } ).ToList();
            }
        }

        /// <summary>
        /// DFS
        /// </summary>
        public void DFS( HashSet<int> mainLoopOrder = null )
        {
            //initialization
            _time = 0;
            foreach( KeyValuePair<int, VertexInfo> pair in _vertices )
            {
                pair.Value.ParentId = null;
                pair.Value.VertexColor = VertexInfo.Color.White;
                pair.Value.DiscoveryTime = null;
                pair.Value.FinishTime = null;
            }

            _topologicalSortedVertices.Clear();
            _forest.Clear();

            //algorithm
            IEnumerable<int> verticesIds = mainLoopOrder ?? _graph.Vertices.Select( x => x.Id );
            foreach( int vertexId in verticesIds )
            {
                Guid treeId = Guid.NewGuid();
                if( _vertices[vertexId].VertexColor == VertexInfo.Color.White )
                {
                    _forest[treeId] = new HashSet<int>();
                    DfsVisit( vertexId, treeId );
                }
            }
        }

        /// <summary>
        /// Prints topological sort
        /// </summary>
        public void PrintTopologicalSort()
        {
            Console.WriteLine();
            if( !IsDag( out List<(VertexBase, VertexBase)> _ ) )
            {
                Console.WriteLine( "Provided graph is not a DAG:" );
                return;
            }

            Console.WriteLine( "Topological sort:" );
            foreach( VertexInfo tmpVertex in _topologicalSortedVertices )
            {
                Console.Write( $"{tmpVertex.Name} " );
            }
        }

        /// <summary>
        /// Method for checking that the graph is a DAG
        /// </summary>
        /// <param name="backEdges">Back edges</param>
        /// <returns></returns>
        public bool IsDag( out List<(VertexBase, VertexBase)> backEdges )
        {
            backEdges = new List<(VertexBase, VertexBase)>();

            foreach( VertexBase fromVertex in _graph.Vertices )
            {
                foreach( int adjacentVertexId in _graph.GetAdjacentVertices( fromVertex.Id ) )
                {
                    VertexInfo from = _vertices[fromVertex.Id];
                    VertexInfo to = _vertices[adjacentVertexId];

                    //back edge
                    if( to.DiscoveryTime <= from.DiscoveryTime && from.DiscoveryTime < from.FinishTime && from.FinishTime <= to.FinishTime )
                    {
                        backEdges.Add( (new VertexBase( from.Id, from.Name ), new VertexBase( to.Id, to.Name )) );
                    }
                }
            }

            return backEdges.Count == 0 && _graph.IsDirected;
        }

        /// <summary>
        /// Prints dfs visits info
        /// </summary>
        public void PrintDfsVisitsInfo()
        {
            Console.WriteLine();
            Console.WriteLine( "Dfs visits info:" );
            foreach( VertexInfo tmpVertex in _topologicalSortedVertices )
            {
                Console.WriteLine( $"{tmpVertex.Name} $(d={tmpVertex.DiscoveryTime}, f={tmpVertex.FinishTime})   " );
            }
        }

        public void PrintDepthFirstForest()
        {
            foreach( KeyValuePair<Guid, HashSet<int>> pair in _forest )
            {
                Console.WriteLine( $"---{string.Join( ",", pair.Value.Select( x => _vertices[x].Name ) )}---" );
            }
        }

        private void DfsVisit( int vertexId, Guid treeId )
        {
            _time++;
            VertexInfo tmpVertex = _vertices[vertexId];
            tmpVertex.DiscoveryTime = _time;
            tmpVertex.VertexColor = VertexInfo.Color.Gray;
            foreach( int adjacentVertexId in _graph.GetAdjacentVertices( vertexId ) )
            {
                if( _vertices[adjacentVertexId].VertexColor == VertexInfo.Color.White )
                {
                    _vertices[adjacentVertexId].ParentId = vertexId;
                    DfsVisit( adjacentVertexId, treeId );
                }
            }

            _time++;
            tmpVertex.FinishTime = _time;
            tmpVertex.VertexColor = VertexInfo.Color.Black;
            _topologicalSortedVertices.Insert( 0, _vertices[vertexId] );
            _forest[treeId].Add( vertexId );
        }

        public class VertexTimeInfo
        {
            public int Id { get; init; }
            public int DiscoveryTime { get; set; }
            public int FinishTime { get; set; }
        }

        private class VertexInfo
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public int? DiscoveryTime { get; set; }
            public int? FinishTime { get; set; }

            public int? ParentId { get; set; }
            public Color VertexColor { get; set; }

            public enum Color
            {
                White,
                Gray,
                Black
            }
        }

        private readonly GraphBase _graph;
        private readonly Dictionary<int, VertexInfo> _vertices;
        private int _time;
        private readonly List<VertexInfo> _topologicalSortedVertices = new();
        private readonly Dictionary<Guid, HashSet<int>> _forest = new();
    }
}