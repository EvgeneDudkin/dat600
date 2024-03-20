namespace Graph.Models
{
    public abstract class GraphBase
    {
        protected GraphBase( int verticesCount, bool isDirected = false )
        {
            _verticesCount = verticesCount;
            _isDirected = isDirected;
        }

        public abstract void AddEdge( int idFrom, int idTo, int weight );
        public abstract IEnumerable<int> GetAdjacentVertices( int id );

        protected readonly int _verticesCount;
        protected readonly bool _isDirected;
    }
}