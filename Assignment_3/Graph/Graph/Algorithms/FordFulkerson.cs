using System.Linq;
using Graph.Models;

namespace Graph.Algorithms
{
    public class FordFulkerson
    {
        public FordFulkerson( GraphBase graph, int sourceId, int targetId )
        {
            _graph = graph;
            _sourceId = sourceId;
            _targetId = targetId;
        }

        public GraphBase GetMaximumFlowNetwork()
        {
            //init
            AdjacencySetGraph flowNetwork = new(_graph.Vertices, _graph.IsDirected);
            AdjacencySetGraph residualNetwork = new(_graph.Vertices, _graph.IsDirected);
            ModifyResidualNetwork( flowNetwork, residualNetwork );

            //search path 
            BreadthFirstSearch bfs = new(residualNetwork, _sourceId);
            while( bfs.IsPath( _sourceId, _targetId ) )
            {
                //find augmenting flow
                int prevId = _targetId;
                int? parentId = bfs.GetParentIdInPath( prevId );
                int? min = null;
                while( parentId.HasValue )
                {
                    int tmpWeight = residualNetwork.GetEdgeWeight( parentId.Value, prevId );
                    if( !min.HasValue || tmpWeight <= min.Value )
                        min = tmpWeight;
                    prevId = parentId.Value;
                    parentId = bfs.GetParentIdInPath( prevId );
                }

                //augment/cancel
                prevId = _targetId;
                parentId = bfs.GetParentIdInPath( prevId );
                while( parentId.HasValue && min.HasValue )
                {
                    //flowNetwork.RemoveEdge( parentId.Value, prevId );
                    if( _graph.GetAdjacentVertices( parentId.Value ).Contains( prevId ) )
                    {
                        int flow = flowNetwork.GetEdgeWeight( parentId.Value, prevId );
                        flowNetwork.RemoveEdge( parentId.Value, prevId );
                        flowNetwork.AddEdge( parentId.Value, prevId, flow + min.Value );
                    }
                    else if( _graph.GetAdjacentVertices( prevId ).Contains( parentId.Value ) )
                    {
                        int flow = flowNetwork.GetEdgeWeight( prevId, parentId.Value );
                        flowNetwork.RemoveEdge( prevId, parentId.Value );
                        if( flow - min.Value > 0 )
                            flowNetwork.AddEdge( prevId, parentId.Value, flow - min.Value );
                    }

                    prevId = parentId.Value;
                    parentId = bfs.GetParentIdInPath( prevId );
                }

                ModifyResidualNetwork( flowNetwork, residualNetwork );
                bfs.BFS( _sourceId );
            }

            return flowNetwork;
        }

        private void ModifyResidualNetwork( AdjacencySetGraph flowNetwork, AdjacencySetGraph residualNetwork )
        {
            foreach( VertexBase vertex in _graph.Vertices )
            {
                foreach( int adjacentVertexId in _graph.GetAdjacentVertices( vertex.Id ) )
                {
                    int capacity = _graph.GetEdgeWeight( vertex.Id, adjacentVertexId );
                    int flow = flowNetwork.GetEdgeWeight( vertex.Id, adjacentVertexId );
                    residualNetwork.RemoveEdge( vertex.Id, adjacentVertexId );
                    if( capacity - flow > 0 )
                        residualNetwork.AddEdge( vertex.Id, adjacentVertexId, capacity - flow );

                    residualNetwork.RemoveEdge( adjacentVertexId, vertex.Id );
                    if( flow > 0 )
                        residualNetwork.AddEdge( adjacentVertexId, vertex.Id, flow );
                }
            }
        }

        private readonly GraphBase _graph;
        private readonly int _sourceId;
        private readonly int _targetId;
    }
}