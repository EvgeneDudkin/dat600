using System;
using System.Collections.Generic;
using Graph.Models;

namespace Graph.Algorithms
{
    /// <summary>
    ///Breadth-first search algorithm
    /// </summary>
    public class BreadthFirstSearch
    {
        public BreadthFirstSearch( GraphBase graph, int? sourceId = null )
        {
            _graph = graph;

            _vertices = new();
            foreach( VertexBase graphVertex in graph.Vertices )
            {
                _vertices[graphVertex.Id] = new VertexInfo
                {
                    Id = graphVertex.Id,
                    Name = graphVertex.Name,
                    Distance = null,
                    ParentId = null,
                    VertexColor = VertexInfo.Color.White
                };
            }

            if( sourceId.HasValue )
                BFS( sourceId.Value );
        }

        /// <summary>
        /// BFS
        /// </summary>
        /// <param name="sourceId">Source Id</param>
        public void BFS( int sourceId )
        {
            _sourceId = sourceId;
            //initialization
            foreach( KeyValuePair<int, VertexInfo> pair in _vertices )
            {
                pair.Value.Distance = pair.Key == sourceId ? 0 : null;
                pair.Value.ParentId = null;
                pair.Value.VertexColor = pair.Key == sourceId ? VertexInfo.Color.Gray : VertexInfo.Color.White;
            }

            _visitingOrderQueue.Clear();

            if( !_vertices.ContainsKey( sourceId ) )
                throw new ArgumentException( "Graph doesn't contain the source" );

            Queue<int> queue = new(new[] { sourceId });
            _visitingOrderQueue.Enqueue( _vertices[sourceId].Name );

            while( queue.Count > 0 )
            {
                int tmpId = queue.Dequeue();
                VertexInfo u = _vertices[tmpId];
                // search the neighbors of tmp
                foreach( int adjacentVertexId in _graph.GetAdjacentVertices( tmpId ) )
                {
                    VertexInfo v = _vertices[adjacentVertexId];
                    if( v.VertexColor == VertexInfo.Color.White ) // is v being discovered now?
                    {
                        v.VertexColor = VertexInfo.Color.Gray;
                        v.Distance = u.Distance + 1;
                        v.ParentId = u.Id;
                        queue.Enqueue( v.Id );
                        _visitingOrderQueue.Enqueue( v.Name );
                    }
                }

                u.VertexColor = VertexInfo.Color.Black;
            }
        }

        /// <summary>
        /// Prints shortest path
        /// </summary>
        /// <param name="sourceId">Source vertex</param>
        /// <param name="targetId">Target vertex</param>
        public void PrintPath( int sourceId, int targetId )
        {
            if( !_vertices.ContainsKey( sourceId ) || !_vertices.ContainsKey( targetId ) )
                throw new ArgumentException( "Invalid vertex" );

            Console.WriteLine();
            PrintPathInternal( sourceId, targetId );
        }

        /// <summary>
        /// Returns path's existence
        /// </summary>
        /// <param name="sourceId">Source vertex</param>
        /// <param name="targetId">Target vertex</param>
        public bool IsPath( int sourceId, int targetId )
        {
            if( !_vertices.ContainsKey( sourceId ) || !_vertices.ContainsKey( targetId ) )
                throw new ArgumentException( "Invalid vertex" );

            return IsPathInternal( sourceId, targetId );
        }

        /// <summary>
        /// Prints visiting order
        /// </summary>
        public void PrintVisitingOrder()
        {
            Console.WriteLine();
            Console.WriteLine( $"Visiting order from the vertex {_vertices[_sourceId].Name}:" );
            Queue<string> queueCopy = new(_visitingOrderQueue);
            while( queueCopy.Count > 0 )
            {
                Console.Write( $"{queueCopy.Dequeue()} " );
            }
        }

        /// <summary>
        /// Returns parent in the shortest path
        /// </summary>
        /// <param name="vertexId"></param>
        /// <returns></returns>
        public int? GetParentIdInPath( int vertexId )
        {
            return _vertices.ContainsKey( vertexId ) ? _vertices[vertexId].ParentId : null;
        }

        private void PrintPathInternal( int sourceId, int targetId )
        {
            if( sourceId == targetId )
                Console.Write( $"Path: {_vertices[sourceId].Name}" );
            else if( !_vertices[targetId].ParentId.HasValue )
                Console.Write( $"no path from {_vertices[sourceId].Name} to {_vertices[targetId].Name} exists " );
            else
            {
                PrintPathInternal( sourceId, _vertices[targetId].ParentId.Value );
                Console.Write( $" -> {_vertices[targetId].Name}" );
            }
        }

        private bool IsPathInternal( int sourceId, int targetId )
        {
            if( sourceId == targetId )
                return true;

            if( !_vertices[targetId].ParentId.HasValue )
                return false;

            return IsPathInternal( sourceId, _vertices[targetId].ParentId.Value );
        }

        private class VertexInfo
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public int? Distance { get; set; }

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
        private int _sourceId;
        private readonly Queue<string> _visitingOrderQueue = new();
    }
}