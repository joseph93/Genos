using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class BreadthFirstSearch<T>
    {
        private readonly IGraph<T> graph;

        public BreadthFirstSearch(IGraph<T> graph)
        {
            this.graph = graph;
        }

        public IEnumerable<T> GetAllReachableVertices(T startingVertex)
        {
            HashSet<T> visited = new HashSet<T> { startingVertex };
            Queue<T> horizon = new Queue<T>();
            horizon.Enqueue(startingVertex);

            while (horizon.Count > 0)
            {
                var nextVertexToExpand = horizon.Dequeue();
                yield return nextVertexToExpand;

                foreach(var vertex in graph.GetAdjacent(nextVertexToExpand).Where(vertex => !visited.Contains(vertex)))
                {
                    visited.Add(vertex);
                    horizon.Enqueue(vertex);
                }
            }
        } 
    }
}
