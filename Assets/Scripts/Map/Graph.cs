
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;

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
            foreach (int key in Vertices.Keys)
            {
                Vertices[key].setState(State.UnVisited);
            }
        }


        public bool Contains(Node vertex)
        {
            if (Vertices.ContainsKey(vertex.getID()))
                return true;

            return false;
        }


        public Node GetFirstElementOfTheList(int findKey)
        {
            if (Vertices.ContainsKey(findKey))
                return Vertices[findKey];

            return null;
        }


        public void BFS(int startVertexKey)
        {

            if (Vertices.Count == 0)
                return;

            Queue<Node> nodes = new Queue<Node>();
            Console.WriteLine("Starting at: {0}", Vertices[startVertexKey].getID());

            GetFirstElementOfTheList(startVertexKey).setState(State.Visited);
            nodes.Enqueue(GetFirstElementOfTheList(startVertexKey));

            while (nodes.Count != 0)
            {
                List<Node> children = GetChildrenOfVertex(nodes.Dequeue());

                foreach (Node v in children)
                {
                    if (Vertices[v.getID()].getState() == State.UnVisited)
                    {

                        Console.WriteLine("Passed to {0}", v.getID());
                        Vertices[v.getID()].setState(State.Visited);
                        nodes.Enqueue(Vertices[v.getID()]);
                    }
                }
            }

        }

        public Node InitializeBFS(int vertexKeyToFind)
        {
            InitializeVertices();
            return BFS(Vertices.First().Key, vertexKeyToFind);
        }


        private List<Node> GetChildrenOfVertex(Node headVertex)
        {
            List<Node> vertexes = new List<Node>();
            Node v = headVertex.getNext();

            while (v != null)
            {
                vertexes.Add(v);
                v = v.getNext();
            }
            return vertexes;
        }



        private Node BFS(int startVertexKey, int vertexKeyToFind)
        {
            if (Vertices.Count == 0)
                return null;

            //Starting from the first element
            Queue<Node> nodes = new Queue<Node>();
            Console.WriteLine("Starting at: {0}", Vertices[startVertexKey].getID());
            Node firstNode = GetFirstElementOfTheList(startVertexKey);
            firstNode.setState(State.Visited);
            nodes.Enqueue(firstNode);

            while (nodes.Count != 0)
            {
                List<Node> children = GetChildrenOfVertex(nodes.Dequeue());

                foreach (Node v in children)
                {
                    if (Vertices[v.getID()].getState() == State.UnVisited)
                    {
                        Console.WriteLine("Passed to {0}", v.getID());
                        if (v.getID() == vertexKeyToFind)
                            return v;
                        Vertices[v.getID()].setState(State.Visited);
                        nodes.Enqueue(Vertices[v.getID()]);
                    }
                }
            }

            return null;
        }


        public bool IsVisited(Node v)
        {
            if (v == null)
                return false;
            return Vertices[v.getID()].getState() == State.Visited;
        }


        public Node FindByKey(int vertexKey)
        {
            if (Vertices.ContainsKey(vertexKey))
                return Vertices[vertexKey];

            return null;
        }
        public bool ExistKey(int vertexKey)
        {
            if (FindByKey(vertexKey) == null)
                return false;
            else
                return true;
        }

        //JOSEPH: I'm not sure how edges work here.
        public Edge InsertUndirectedEdge(Node startingNode, Node endNode, int weight)
        {
            InsertDirectEdge(startingNode, endNode, weight);
            InsertDirectEdge(endNode, startingNode, weight);

            return new Edge(startingNode, endNode, weight);
        }

        public void InsertNewVertex(Node vertex)
        {
            if (!ExistKey(vertex.getID()))
            {
                Vertices.Add(vertex.getID(), vertex);
            }
        }

        //JOSEPH: I'm not sure how edges work here.
        public Edge InsertDirectEdge(Node startingNode, Node endNode, int weightEdge)
        {
            //Create the vertex A on the vertex list
            if (!ExistKey(startingNode.getID()))
            {
                InsertNewVertex(startingNode);
            }
            //Create the vertex B on the vertex list
            if (!ExistKey(endNode.getID()))
            {
               InsertNewVertex(endNode);
            }

            //Add the vertex B on the vertex A position on the Dictionary, as the second element of the list
            Node vertexB = new Node(endNode);
           // vertexB.Weight = weightEdge;
            vertexB.setNext(Vertices[startingNode.getID()].getNext());
            Vertices[startingNode.getID()].setNext(vertexB);
            Edge edge = new Edge(startingNode, endNode, weightEdge);

            return edge;
        }




    
}
