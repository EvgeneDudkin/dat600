using System;
using System.Collections.Generic;
using System.Linq;
using Graph.Auxiliary;

namespace Graph.Models
{
    public class AdjacencySetGraph : GraphBase
    {
        public AdjacencySetGraph( int verticesCount, bool isDirected = false ) : base( verticesCount, isDirected )
        {
            _vertices = new();
            for( int i = 0; i < verticesCount; i++ )
            {
                _vertices[i] = new Vertex( i );
            }
        }

        public AdjacencySetGraph( IEnumerable<VertexBase> vertices, bool isDirected = false ) : base( vertices.Count(), isDirected )
        {
            _vertices = new();
            foreach( VertexBase vertexBase in vertices )
            {
                Vertex vertex = new(vertexBase);
                _vertices[vertex.Id] = vertex;
            }
        }

        //from another graph (matrix f.ex.)
        public AdjacencySetGraph( GraphBase graphBase ) : this( graphBase.Vertices, graphBase.IsDirected )
        {
            foreach( KeyValuePair<int, Vertex> pair in _vertices )
            {
                foreach( int adjacentVertex in graphBase.GetAdjacentVertices( pair.Key ) )
                {
                    int weight = graphBase.GetEdgeWeight( pair.Key, adjacentVertex );
                    AddEdge( pair.Key, adjacentVertex, weight );
                }
            }
        }

        public override int TotalWeight
        {
            get
            {
                int totalWeight = 0;
                foreach( KeyValuePair<int, Vertex> pair in _vertices )
                {
                    foreach( int adjacentVertexId in pair.Value.GetAdjacentVertices() )
                    {
                        totalWeight += pair.Value.GetEdgeWeight( adjacentVertexId );
                    }
                }

                return IsDirected ? totalWeight : totalWeight / 2;
            }
        }

        public override IEnumerable<VertexBase> Vertices
        {
            get
            {
                //alphabetical order
                return _vertices.Values.OrderBy( x => x.Name ).Select( x => (VertexBase)x );
            }
        }

        public sealed override void AddEdge( int idFrom, int idTo, int weight = 1 )
        {
            if( !_vertices.ContainsKey( idFrom ) || !_vertices.ContainsKey( idTo ) )
                throw new ArgumentException( "Invalid parameters" );

            _vertices[idFrom].AddEdge( idTo, weight );
            if( !_isDirected )
                _vertices[idTo].AddEdge( idFrom, weight );
        }

        public sealed override void RemoveEdge( int idFrom, int idTo )
        {
            if( !_vertices.ContainsKey( idFrom ) || !_vertices.ContainsKey( idTo ) )
                throw new ArgumentException( "Invalid parameters" );

            _vertices[idFrom].RemoveEdge( idTo );
            if( !_isDirected )
                _vertices[idTo].RemoveEdge( idFrom );
        }

        public override IEnumerable<int> GetAdjacentVertices( int id )
        {
            if( !_vertices.ContainsKey( id ) )
                throw new ArgumentException( "Invalid id" );
            List<Vertex> adjVertices = new();
            //alphabetical order
            foreach( int adjVertexId in _vertices[id].GetAdjacentVertices() )
            {
                if( _vertices.ContainsKey( adjVertexId ) )
                    adjVertices.Add( _vertices[adjVertexId] );
            }

            return adjVertices.OrderBy( x => x.Name ).Select( x => x.Id );
        }

        public override int GetEdgeWeight( int idFrom, int idTo )
        {
            if( !_vertices.ContainsKey( idFrom ) || !_vertices.ContainsKey( idTo ) )
                throw new ArgumentException( "Invalid parameters" );

            return _vertices[idFrom].GetEdgeWeight( idTo );
        }

        public override void Display()
        {
            //init lib
            REngineInstance.REngine.Evaluate( "library(igraph)" );

            //vertices
            string vertexInitRCode =
                $"data.frame(c({string.Join( ",", _vertices.Keys.Select( x => $"{x}" ) )}), Label = c({string.Join( ",", _vertices.Values.Select( x => $"\"{x.Name}\"" ) )}))";
            REngineInstance.REngine.Evaluate( "v <- " + vertexInitRCode );

            //edges
            List<int> fromIds = new();
            List<int> toIds = new();
            List<int> weights = new();

            foreach( KeyValuePair<int, Vertex> pair in _vertices )
            {
                Vertex tmpVertex = pair.Value;
                foreach( int adjVertexId in tmpVertex.GetAdjacentVertices() )
                {
                    fromIds.Add( tmpVertex.Id );
                    toIds.Add( adjVertexId );
                    weights.Add( tmpVertex.GetEdgeWeight( adjVertexId ) );
                }
            }

            string edgesInitRCode =
                $"data.frame(from = c({string.Join( ",", fromIds.Select( x => $"{x}" ) )}), " +
                $"to = c({string.Join( ",", toIds.Select( x => x ) )}), " +
                $"Weights = c({string.Join( ",", weights.Select( x => x ) )}))";
            REngineInstance.REngine.Evaluate( "e <- " + edgesInitRCode );

            //graph init code
            string direction = _isDirected ? "TRUE" : "FALSE";
            REngineInstance.REngine.Evaluate( $"graph <- graph_from_data_frame(d = e, vertices = v, directed = {direction})" );
            REngineInstance.REngine.Evaluate( "windows()" );

            //plot
            string edgesWeightsCode = weights.Count( x => x != 1 ) > 0 ? "edge.label = E(graph)$Weights," : String.Empty;
            string plotGraphRCode = "plot(graph, edge.arrow.size=.5, vertex.color=\"gold\", vertex.size=15, " +
                                    "vertex.frame.color=\"gray\", vertex.label.color=\"black\"," +
                                    "vertex.label = V(graph)$Label," +
                                    $"{edgesWeightsCode}" + "vertex.label.cex=0.8, vertex.label.dist=2, edge.curved=0.25)";

            REngineInstance.REngine.Evaluate( plotGraphRCode );
        }

        private readonly Dictionary<int, Vertex> _vertices;
    }
}