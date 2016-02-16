using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Graph : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



        private Dictionary<int, Node> Vertices { get; set; }

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
            Console.WriteLine("Starting at: {0}", this.Vertices[startVertexKey].x);

            this.GetFirstElementOfTheList(startVertexKey).Status = State.Visited;
            Q.Enqueue(this.GetFirstElementOfTheList(startVertexKey));

            while (Q.Count != 0)
            {
                List<Node> children = GetChildrenOfVertex(Q.Dequeue());

                foreach (Node v in children)
                {
                    if (this.Vertices[v.x].Status == State.UnVisited)
                    {

                        Console.WriteLine("Passed to {0}", v.x);
                        this.Vertices[v.x].Status = State.Visited;
                        Q.Enqueue(this.Vertices[v.x]);
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
            Console.WriteLine("Starting at: {0}", this.Vertices[startVertexKey].x);
            this.GetFirstElementOfTheList(startVertexKey).Status = State.Visited;
            Q.Enqueue(this.GetFirstElementOfTheList(startVertexKey));

            while (Q.Count != 0)
            {
                List<Node> children = GetChildrenOfVertex(Q.Dequeue());

                foreach (Node v in children)
                {
                    if (this.Vertices[v.x].Status == State.UnVisited)
                    {
                        Console.WriteLine("Passed to {0}", v.x);
                        if (v.x == vertexKeyToFind)
                            return v;
                        this.Vertices[v.x].Status = State.Visited;
                        Q.Enqueue(this.Vertices[v.x]);
                    }
                }
            }

            return null;
        }


        public bool IsVisited(Node v)
        {
            if (v == null)
                return false;
            return this.Vertices[v.x].Status == State.Visited;
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
