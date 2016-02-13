using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGit2Sharp.Core.Compat;

namespace Assets.Scripts
{
    class Graph<T> : IGraph<T>
    {
        private readonly Dictionary<T, List<T>> edges;

        public Graph(params Tuple<T, T>[] directEdges)
        {
            //grouped by <Node and list of Nodes> (Item1 -> Node, Item2 -> NodeList)
            edges = directEdges.GroupBy(tuple => tuple.Item1, tuple => tuple.Item2)
                .ToDictionary(g => g.Key, g => g.ToList());

            //For every vertex that has no edge, add a list to it. 
            foreach (var missingVertex in directEdges.Where(tuple => !edges.ContainsKey(tuple.Item2))
                .Select(tuple => tuple.Item2))
            {
                edges[missingVertex] = new List<T>();
            }
        } 

        public bool Contains(T vertex)
        {
            return edges.ContainsKey(vertex);
        }

        public IEnumerable<T> GetAdjacent(T vertex)
        {
            List<T> adjacentVertices;
            return edges.TryGetValue(vertex, out adjacentVertices) ? adjacentVertices : Enumerable.Empty<T>();
        }
    }
}
