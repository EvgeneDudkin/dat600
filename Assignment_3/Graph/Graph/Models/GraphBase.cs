using System.Collections.Generic;

namespace Graph.Models
{
    public abstract class GraphBase
    {
        protected GraphBase( int verticesCount, bool isDirected = false )
        {
            _verticesCount = verticesCount;
            _isDirected = isDirected;
        }

        public abstract IEnumerable<VertexBase> Vertices { get; }
        public abstract void AddEdge( int idFrom, int idTo, int weight = 1 );
        public abstract void RemoveEdge( int idFrom, int idTo );
        public abstract IEnumerable<int> GetAdjacentVertices( int id );
        public abstract int GetEdgeWeight( int idFrom, int idTo );
        public abstract void Display();
        public abstract int TotalWeight { get; }

        public int VerticesCount
        {
            get { return _verticesCount; }
        }

        public bool IsDirected
        {
            get { return _isDirected; }
        }

        protected readonly int _verticesCount;
        protected readonly bool _isDirected;
    }
}