using System.Collections.Generic;

namespace Graph.Models
{
    public class VertexBase
    {
        public VertexBase( int id, string name = null )
        {
            Id = id;
            Name = string.IsNullOrEmpty( name ) ? $"{id}" : name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Vertex : VertexBase
    {
        public Vertex( int id, string name = null ) : base( id, name )
        {
            _adjacencySet = new();
        }

        public Vertex( VertexBase vertexBase ) : this( vertexBase.Id, vertexBase.Name )
        {
        }

        public void AddEdge( int v, int weight )
        {
            _adjacencySet[v] = weight;
        }

        public void RemoveEdge( int v )
        {
            _adjacencySet.Remove( v );
        }

        public int GetEdgeWeight( int v )
        {
            return _adjacencySet.ContainsKey( v ) ? _adjacencySet[v] : 0;
        }

        public IEnumerable<int> GetAdjacentVertices()
        {
            return _adjacencySet.Keys;
        }

        private readonly Dictionary<int, int> _adjacencySet;
    }
}