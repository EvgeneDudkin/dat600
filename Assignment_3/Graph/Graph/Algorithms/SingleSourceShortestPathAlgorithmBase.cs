using Graph.Models;
using System;
using System.Collections.Generic;

namespace Graph.Algorithms
{
    /// <summary>
    /// Bellman-Ford algorithm implementation
    /// </summary>
    public abstract class SingleSourceShortestPathAlgorithmBase
    {
        protected SingleSourceShortestPathAlgorithmBase( GraphBase graph, int sourceId )
        {
            _graph = graph;
            ComputeShortestPaths( sourceId );
        }

        /// <summary>
        /// Computes shortest paths to all vertices from the source 
        /// </summary>
        /// <param name="sourceId">Source</param>
        public bool ComputeShortestPaths( int sourceId )
        {
            //initialization
            _sourceId = sourceId;
            _verticesDistancesInfo.Clear();

            foreach( VertexBase graphVertex in _graph.Vertices )
            {
                VertexInfo info = new()
                {
                    Id = graphVertex.Id,
                    Name = graphVertex.Name,
                    Distance = graphVertex.Id == sourceId ? 0 : null,
                    ParentId = null
                };
                _verticesDistancesInfo[graphVertex.Id] = info;
            }

            if( ComputeShortestPathsInternal() )
            {
                PrintShortestPaths();
                return true;
            }

            return false;
        }

        public void PrintShortestPaths()
        {
            Console.WriteLine( "Shortest paths:" );
            foreach( KeyValuePair<int, VertexInfo> pair in _verticesDistancesInfo )
            {
                if( _sourceId == pair.Key )
                    continue;

                if( pair.Value.Distance.HasValue )
                {
                    Console.WriteLine( $"Distance to {pair.Value.Name} = {pair.Value.Distance.Value}" );
                    PrintPathInternal( _sourceId, pair.Key );
                }
                else
                {
                    Console.WriteLine( $"There is no path from {_verticesDistancesInfo[_sourceId].Name} to {pair.Value.Name}" );
                }

                Console.WriteLine();
            }
        }

        protected abstract bool ComputeShortestPathsInternal();

        private void PrintPathInternal( int sourceId, int targetId )
        {
            if( sourceId == targetId )
                Console.Write( $"Path: {_verticesDistancesInfo[sourceId].Name}" );
            else if( !_verticesDistancesInfo[targetId].ParentId.HasValue )
                Console.Write( $"no path from {_verticesDistancesInfo[sourceId].Name} to {_verticesDistancesInfo[targetId].Name} exists " );
            else
            {
                PrintPathInternal( sourceId, _verticesDistancesInfo[targetId].ParentId.Value );
                Console.Write( $" -> {_verticesDistancesInfo[targetId].Name}" );
            }
        }

        protected bool Relax( int sourceId, int targetId, int weight )
        {
            bool result = false;
            if( _verticesDistancesInfo[sourceId].Distance.HasValue && (!_verticesDistancesInfo[targetId].Distance.HasValue ||
                                                                       _verticesDistancesInfo[sourceId].Distance.Value + weight <
                                                                       _verticesDistancesInfo[targetId].Distance.Value) )
            {
                _verticesDistancesInfo[targetId].Distance = _verticesDistancesInfo[sourceId].Distance.Value + weight;
                _verticesDistancesInfo[targetId].ParentId = sourceId;
                result = true;
            }

            return result;
        }

        protected class VertexInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int? ParentId { get; set; }
            public int? Distance { get; set; }
        }

        protected readonly GraphBase _graph;
        protected readonly Dictionary<int, VertexInfo> _verticesDistancesInfo = new();
        protected int _sourceId;
    }
}