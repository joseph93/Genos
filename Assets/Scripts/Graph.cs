using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using UnityEditorInternal;

namespace Assets.Scripts
{

    public class Graph: IGraph
    {
        private Dictionary<int,Node> Vertices { get; set; }

        //For use on the DFS to "break" the recursion.
        private bool finished;

        public Graph()
        {
            Vertices = new Dictionary<int, Node>();

        }

  
        //Initialize all vertices with Unvisited value.
        private void InitializeVertices()
        {
            foreach (int key in this.Vertices.Keys)
            {
                this.Vertices[key].Status = State.UnVisited;
            }
        }


        public bool Contains(Node vertex)
        {
            throw new NotImplementedException();
        }
        

        public Node GetFirstElementOfTheList(int findKey)
        {
            if (this.Vertices.ContainsKey(findKey))
                return this.Vertices[findKey];

            return null;
        }


        public void BFS(int startVertexKey)
        {

            if (this.Vertices.Count == 0)
                return;

            Queue<Node> Q = new Queue<Node>();
            Console.WriteLine("Starting at: {0}", this.Vertices[startVertexKey].Key);

            this.GetFirstElementOfTheList(startVertexKey).Status = State.Visited;
            Q.Enqueue(this.GetFirstElementOfTheList(startVertexKey));

            while (Q.Count != 0)
            {
                List<Node> children = GetChildrenOfVertex(Q.Dequeue());

                foreach (Node v in children)
                {
                    if (this.Vertices[v.Key].Status == State.UnVisited)
                    {

                        Console.WriteLine("Passed to {0}", v.Key);
                        this.Vertices[v.Key].Status = State.Visited;
                        Q.Enqueue(this.Vertices[v.Key]);
                    }
                }
            }

        }

        public Node InitializeBFS(int vertexKeyToFind)
        {
            InitializeVertices();
            return BFS(this.Vertices.First().Key, vertexKeyToFind);
        }


        private List<Node> GetChildrenOfVertex(Node headVertex)
        {
            List<Node> vertexes = new List<Node>();
            Node v = headVertex.Next;

            while (v != null)
            {
                vertexes.Add(v);
                v = v.Next;
            }
            return vertexes;
        }



        private Node BFS(int startVertexKey, int vertexKeyToFind)
        {
            if (this.Vertices.Count == 0)
                return null;

            //Starting from the first element
            Queue<Node> Q = new Queue<Node>();
            Console.WriteLine("Starting at: {0}", this.Vertices[startVertexKey].Key);
            this.GetFirstElementOfTheList(startVertexKey).Status = State.Visited;
            Q.Enqueue(this.GetFirstElementOfTheList(startVertexKey));

            while (Q.Count != 0)
            {
                List<Node> children = GetChildrenOfVertex(Q.Dequeue());

                foreach (Node v in children)
                {
                    if (this.Vertices[v.Key].Status == State.UnVisited)
                    {
                        Console.WriteLine("Passed to {0}", v.Key);
                        if (v.Key == vertexKeyToFind)
                            return v;
                        this.Vertices[v.Key].Status = State.Visited;
                        Q.Enqueue(this.Vertices[v.Key]);
                    }
                }
            }

            return null;
        }


        public bool IsVisited(Node v)
        {
            if (v == null)
                return false;
            return this.Vertices[v.Key].Status == State.Visited;
        }
    

        public Node FindByKey(int vertexKey)
        {
            if (this.Vertices.ContainsKey(vertexKey))
                return this.Vertices[vertexKey];

            return null;
        }
        public bool ExistKey(int vertexKey)
        {
            if (this.FindByKey(vertexKey) == null)
                return false;
            else
                return true;
        }


        public void InsertUndirectedEdge(int vertexAKey, int vertexBKey, int Weight = 0)
        {
            this.InsertDirectEdge(vertexAKey, vertexBKey, Weight);
            this.InsertDirectEdge(vertexBKey, vertexAKey, Weight);
        }

        public void InsertNewVertex(int vertexKey)
        {
            if (!this.ExistKey(vertexKey))
            {
                this.Vertices.Add(vertexKey, new Node(vertexKey));
            }
        }

        public override string ToString()
        {
            return "Heyhey. I am a Graph";
        }

        public void InsertDirectEdge(int vertexAKey, int vertexBKey, int weightEdge = 0)
        {
            //Create the vertex A on the vertex list
            if (!this.ExistKey(vertexAKey))
            {
                this.InsertNewVertex(vertexAKey);
            }
            //Create the vertex B on the vertex list
            if (!this.ExistKey(vertexBKey))
            {
                this.InsertNewVertex(vertexBKey);
            }

            //Add the vertex B on the vertex A position on the Dictionary, as the second element of the list
            Node vertexB = new Node(vertexBKey);
            vertexB.Weight = weightEdge;
            vertexB.Next = this.Vertices[vertexAKey].Next;
            this.Vertices[vertexAKey].Next = vertexB;

        }
    }

=======
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
>>>>>>> 6ab2d662f68cd329c1f999fb179d3fd170b3911a
}
