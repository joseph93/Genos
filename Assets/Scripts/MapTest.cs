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
