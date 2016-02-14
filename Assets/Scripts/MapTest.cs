using System;
using System.Collections.Generic;
using LibGit2Sharp.Core.Compat;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapTest : MonoBehaviour
    {
        public GameObject NipperBeacon;

        void Start()
        {
        }

        void Update()
        {
<<<<<<< HEAD
            
        }
        public void testGraph()
        {
            //iBeaconReceiverExample bc = new iBeaconReceiverExample();

            Node n1 = new Node(5, 5);
            Node n2 = new Node(10, 10);
            Edge edge1 = new Edge(n1, n2, 5);
=======

>>>>>>> 6ab2d662f68cd329c1f999fb179d3fd170b3911a
        }

        public static void Main()
        {
            Node n1 = new Node(5, 6);
            List<Node> nodeList = new List<Node>();
            nodeList.Add(n1);
            /* //var graph = new Graph<Node>(
               // Tuple<Node, Node>.Create(n1, nodeList));

           // var breadthFirstSearch = new BreadthFirstSearch<int>(graph);

            foreach (var vertex in breadthFirstSearch.GetAllReachableVertices(4))
            {
                Console.WriteLine("Next vertex is {0}", vertex);
            }*/
        }
    }
}
