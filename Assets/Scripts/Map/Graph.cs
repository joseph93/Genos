﻿using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Graph : MonoBehaviour
{
    // Use this for initialization

    private readonly Dictionary<int, Node> Vertices;
    

    public Graph()
    {
        Vertices = new Dictionary<int, Node>();
    }

    public Dictionary<int, Node> getVertices()
    {
        return Vertices;
    }


    //Initialize all vertices with Unvisited value.
/**
* Method:    InitializeVertices
* FullName:  InitializeVertices
* Access:    private
* Qualifier:
* @param    
* @return   void
*/
    public void InitializeVertices()
    {
        foreach (KeyValuePair<int, Node> entry in Vertices)
        {
            entry.Value.setState(State.UnVisited);
        }
    }

    //JOSEPH: Contains the id of the node

    /**
    * Method:    Contains
    * FullName:  Contains
    * Access:    public
    * Qualifier:
    * @param    int vertexKey
    * @return   bool
    */
    public bool Contains(int vertexKey)
    {
        if (Vertices.ContainsKey(vertexKey))
            return true;

        return false;
    }


    /**
    * Method:    GetFirstElementOfTheList
    * FullName:  GetFirstElementOfTheList
    * Access:    public
    * Qualifier:
    * @param    int findKey
    * @return   Node
    */

    public Node GetFirstElementOfTheList(int findKey)
    {
        return (from entry in Vertices where entry.Key == findKey select entry.Value).FirstOrDefault();
    }

    /**
    * Method:    BFS
    * FullName:  BFS
    * Access:    public
    * Qualifier:
    * @param    Node startVertex
    * @return   void
    */
    public void BFS(Node startVertex)
    {

        if (Vertices.Count == 0)
            return;

        Queue<Node> nodes = new Queue<Node>();
        Debug.Log("Starting at: " + startVertex.getID());
        //JOSEPH: Put the first element in the queue.
        startVertex.setState(State.Visited);
        nodes.Enqueue(startVertex);

        while (nodes.Count != 0)
        {
            //JOSEPH: put the adjacent nodes of the element in the queue in a dictionary.
            //        Then, take out the element out of the queue.
            Dictionary<Node, float> adjacentNode = nodes.Dequeue().getAdjacentNodes();

            foreach (Node v in adjacentNode.Keys)
            {
                if (!v.IsVisited())
                {

                    Debug.Log("Passed to " + v.getID());
                    Vertices[v.getID()].setState(State.Visited);
                    nodes.Enqueue(v);
                }
            }
        }

    }


        /**
    * Method:    InitializeBFS
    * FullName:  InitializeBFS
    * Access:    public
    * Qualifier:
    * @param    Node vertextToFind
    * @return   Node
    */
    public Node InitializeBFS(Node vertexToFind)
    {
        InitializeVertices();
        return BFS(Vertices.First().Value, vertexToFind);
    }
   

    /**
    * Method:    BFS
    * FullName:  BFS
    * Access:    public
    * Qualifier:
    * @param    Node startVetex, vertexToFind
    * @return   Node
    */

    public Node BFS(Node startVertex, Node vertexToFind)
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
            Dictionary<Node, float> adjacentNodes = nodes.Dequeue().getAdjacentNodes();
            
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


    /**
    * Method:    FindByKey
    * FullName:  FindByKey
    * Access:    public
    * Qualifier:
    * @param    int nodeID
    * @return   Node
    */
    public Node FindByKey(int nodeID)
    {
        foreach (KeyValuePair<int, Node> entry in Vertices)
        {
            if (entry.Key == nodeID)
                return entry.Value;
        }
        return null;
    }

    //JOSEPH: does the node exist in the vertices dictionary

    /**
    * Method:    ExistKey
    * FullName:  ExistKey
    * Access:    public
    * Qualifier:
    * @param    int v
    * @return   bool
    */
    public bool ExistKey(int v)
    {
        if (FindByKey(v) == null)
            return false;
        else
            return true;
    }



    /**
    * Method:    InsertNewVertex
    * FullName:  InsertNewVertex
    * Access:    public 
    * Qualifier:
    * @param   Node vertex
    * @return   void
    */

    public void InsertNewVertex(Node vertex)
    {
        if (!Vertices.ContainsKey(vertex.getID()))
        {
            Vertices.Add(vertex.getID(), vertex);
        }
    }


    // REVIEWED by Joseph Atallah
    // IHCENE: The shortest path algorithm


    /**
    * Method:    shortest_path
    * FullName:  shortest_path
    * Access:    public
    * Qualifier:
    * @param    Node start, finish
    * @return   List<Node>
    */
    public List<Node> shortest_path(Node start, Node finish)
    {
        // previous Dictionary: helps us to get the previous node
        var previous = new Dictionary<Node, Node>();
        // distances Dictionary: helps us to get the distances registered for each paths
        var distances = new Dictionary<Node, float>();
        // the nodes list : helps us to keep track of all the nodes in the graph, so that 
        // they can be easily sorted later.
        var nodes = new List<Node>();

        // the path list : represents our shortest path between 2 nodes (final result)
        List<Node> path = null;
        Debug.Log("Let's start.");
        // JOSEPH-REVIEW: KeyValuePari<int,Node> can be replaced by Node value in Vertices.Values.
        foreach (Node entry in Vertices.Values)
        {
            // the distance between the first node and itself is equal to 0
            if (entry.getID() == start.getID())
            {
                Debug.Log("Start node is" + entry.getID());
                distances[entry] = 0;
            }
            // the distance between the first node and the other nodes is unknown, thus set to "Infinity"
            else
            {
                distances[entry] = int.MaxValue;
            }

            nodes.Add(entry);
            Debug.Log("Added node " + entry.getID());
        }

        while (nodes.Count != 0)
        {
            // Then sorting the list of nodes from smallest distance to the biggest
            // JOSEPH-REVIEW: Here the casting of int will lead to an error when comparing two values
            // that are different by a few decimals.
            nodes.Sort((x, y) => (int)distances[x] - (int)distances[y]);

            var smallest = nodes[0];
            nodes.Remove(smallest);

            if (smallest == finish)
            {
                path = new List<Node>();
                while (previous.ContainsKey(smallest))
                {
                    Debug.Log("Im at the last node");
                    // insert the exact shortest path by retracing the values of each nodes inside 
                    // the previous Dictionary
                    path.Add(smallest);
                    smallest = previous[smallest];
                }

                break;
            }

            if (distances[smallest] == int.MaxValue)
            {
                break;
            }

            Dictionary<Node, float> neighbors = smallest.getAdjacentNodes();
            foreach (KeyValuePair<Node, float> neighbor in neighbors)
            {
                // distance of the current node + adjacent node
                float alt = distances[smallest] + neighbor.Value;
                Debug.Log("Neighbor edge weight: " + neighbor.Value);
                // then compare with the distance of the other adjacent node
                if (alt < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = alt;
                    previous[neighbor.Key] = smallest;
                }
            }
        }

        return path;
    }
}
    


    

