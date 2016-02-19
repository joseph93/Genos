
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
            foreach (KeyValuePair<int, Node> entry in Vertices)
            {
                entry.Value.setState(State.UnVisited);
            }
        }

        //JOSEPH: Contains the id of the node?
        public bool Contains(int vertexKey)
        {
            if (Vertices.ContainsKey(vertexKey))
                return true;

            return false;
        }


    public Node GetFirstElementOfTheList(int findKey)
    {
        return (from entry in Vertices where entry.Key == findKey select entry.Value).FirstOrDefault();
    }


    public void BFS(Node startVertex)
        {

            if (Vertices.Count == 0)
                return;

            Queue<Node> nodes = new Queue<Node>();
            Console.WriteLine("Starting at: {0}", startVertex.getID());
            //JOSEPH: Put the first element in the queue.
            startVertex.setState(State.Visited);
            nodes.Enqueue(startVertex);

            while (nodes.Count != 0)
            {
            //JOSEPH: put the adjacent nodes of the element in the queue in a dictionary.
            //        Then, take out the element out of the queue.
            Dictionary<Node, double> adjacentNode = nodes.Dequeue().getAdjacentNodes();

                foreach (Node v in adjacentNode.Keys)
                {
                    if (!v.IsVisited())
                    {

                        Console.WriteLine("Passed to {0}", v.getID());
                        Vertices[v.getID()].setState(State.Visited);
                        nodes.Enqueue(v);
                    }
                }
            }

        }

        public Node InitializeBFS(Node vertexToFind)
        {
            InitializeVertices();
            return BFS(Vertices.First().Value, vertexToFind);
        }


        /*private List<Node> GetChildrenOfVertex(Node headVertex)
        {
            List<Node> vertices = new List<Node>();

           
            Dictionary<Node, Edge> adjacentNodes = headVertex.getAdjacentNodes();

            foreach (Node key in adjacentNodes.Keys)
            {
              vertices.Add(key);
            }
            
            Node v = headVertex.getNext();

            while (v != null)
            {
                vertices.Add(v);
                v = v.getNext();
            }
            return vertices;
        }*/



        private Node BFS(Node startVertex, Node vertexToFind)
        {
            if (Vertices.Count == 0)
                return null;

            //Starting from the first element
            Queue<Node> nodes = new Queue<Node>();
            Console.WriteLine("Starting at: {0}", startVertex.getID());
            startVertex.setState(State.Visited);
            nodes.Enqueue(startVertex);

            while (nodes.Count != 0)
            {
                //JOSEPH: put the adjacent nodes of the element in the queue in a dictionary.
                //        Then, take out the element out of the queue.
                Dictionary<Node, double> adjacentNodes = nodes.Dequeue().getAdjacentNodes();

                foreach (Node v in adjacentNodes.Keys)
                {
                    if (!v.IsVisited())
                    {
                        Console.WriteLine("Passed to {0}", v.getID());
                        //if you found the node that you wanted to find, return it and its path. 
                        if (v.getID() == vertexToFind.getID())
                            return v;
                        Vertices[v.getID()].setState(State.Visited);
                        nodes.Enqueue(v);
                    }
                }
            }

            return null;
        }

        
       

    //JOSEPH: Find a node by giving its id
        public Node FindByKey(int nodeID)
        {
            foreach (KeyValuePair<int, Node> entry in Vertices)
            {
                if (entry.Key == nodeID)
                    return entry.Value;
            }
            return null;
        }

        //JOSEPH: does the node exist in the vertices dictionary?
        public bool ExistKey(int v)
        {
            if (FindByKey(v) == null)
                return false;
            else
                return true;
        }

        //JOSEPH: I'm not sure how edges work here.
        /*public void InsertUndirectedEdge(Node source, Node target, double weight)
        {
            if(!source.existsInAdjacentNodes(target))
                source.addAdjacentNode(target, weight);
        }*/

        public void InsertNewVertex(Node vertex)
        {
            if (!ExistKey(vertex.getID()))
            {
                Vertices.Add(vertex.getID(), vertex);
            }
        }

        //JOSEPH: I'm not sure how edges work here.
       /* public Edge InsertDirectEdge(Node startingNode, Node endNode, int weightEdge)
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
        }*/




    
}
