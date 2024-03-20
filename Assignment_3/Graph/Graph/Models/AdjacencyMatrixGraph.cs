using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Models
{
    public class AdjacencyMatrixGraph : GraphBase
    {
        public AdjacencyMatrixGraph( int verticesCount, bool isDirected = false ) : base( verticesCount, isDirected )
        {
            _matrix = new int[verticesCount, verticesCount];
            for( int i = 0; i < verticesCount; i++ )
            {
                for( int j = 0; j < verticesCount; j++ )
                {
                    _matrix[i, j] = 0;
                }
            }
        }

        public AdjacencyMatrixGraph( int[,] matrix, bool isDirected = false ) : base( matrix.GetLength( 0 ), isDirected )
        {
            if( !IsMatrixValid( matrix, isDirected ) )
                throw new ArgumentException( "Invalid ctor parameters" );
            _matrix = matrix;
        }

        public override int TotalWeight
        {
            get
            {
                int totalWeight = 0;
                for( int i = 0; i < _matrix.GetLength( 0 ); i++ )
                {
                    for( int j = 0; j < _matrix.GetLength( 1 ); j++ )
                    {
                        totalWeight += _matrix[i, j];
                    }
                }

                return IsDirected ? totalWeight : totalWeight / 2;
            }
        }

        public override IEnumerable<VertexBase> Vertices
        {
            get
            {
                List<VertexBase> vertices = new();
                for( int i = 0; i < _matrix.GetLength( 0 ); i++ )
                {
                    VertexBase tmpVertex = new(i, $"{i + 1}"); //indexation from 1
                    vertices.Add( tmpVertex );
                }

                //alphabetical order
                return vertices.OrderBy( x => x.Name );
            }
        }

        public override void AddEdge( int idFrom, int idTo, int weight = 1 )
        {
            if( !IsVertexIndexValid( idFrom ) || !IsVertexIndexValid( idTo ) )
                throw new ArgumentException( "Invalid parameters" );

            _matrix[idFrom, idTo] = weight;

            if( !_isDirected )
                _matrix[idTo, idFrom] = weight;
        }

        public override void RemoveEdge( int idFrom, int idTo )
        {
            if( !IsVertexIndexValid( idFrom ) || !IsVertexIndexValid( idTo ) )
                throw new ArgumentException( "Invalid parameters" );

            _matrix[idFrom, idTo] = 0;

            if( !_isDirected )
                _matrix[idTo, idFrom] = 0;
        }

        public override IEnumerable<int> GetAdjacentVertices( int id )
        {
            if( !IsVertexIndexValid( id ) )
                throw new ArgumentException( "Invalid id" );

            List<int> adjacentVertices = new();
            for( int j = 0; j < _matrix.GetLength( 0 ); j++ )
            {
                if( _matrix[id, j] != 0 )
                    adjacentVertices.Add( j );
            }

            return adjacentVertices;
        }

        public override int GetEdgeWeight( int idFrom, int idTo )
        {
            if( !IsVertexIndexValid( idFrom ) || !IsVertexIndexValid( idTo ) )
                throw new ArgumentException( "Invalid parameters" );

            return _matrix[idFrom, idTo];
        }

        public override void Display()
        {
            for( int i = 0; i < _matrix.GetLength( 0 ); i++ )
            {
                for( int j = 0; j < _matrix.GetLength( 1 ); j++ )
                {
                    Console.Write( $"{_matrix[i, j]} " );
                }

                Console.WriteLine();
            }
        }

        private bool IsVertexIndexValid( int id )
        {
            return 0 <= id && id < VerticesCount;
        }

        private bool IsMatrixValid( int[,] matrix, bool isDirected )
        {
            if( matrix.GetLength( 0 ) != matrix.GetLength( 1 ) )
                return false;

            if( !isDirected )
            {
                for( int i = 0; i < matrix.GetLength( 0 ); i++ )
                {
                    for( int j = 0; j < matrix.GetLength( 1 ); j++ )
                    {
                        if( matrix[i, j] != matrix[j, i] )
                            return false;
                    }
                }
            }

            return true;
        }

        private readonly int[,] _matrix;
    }
}