using UnityEngine;
using System.Collections;

namespace Assets.Scripts {
    public class Edge : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private Node startingNode { get; set; }
        private Node endNode { get; set; }
        private double edgeWeight { get; set; }
        private int floorNumber { get; set; }


        public Edge(Node n1, Node n2, int fN, double weight)
        {
            startingNode = n1;
            endNode = n2;
            floorNumber = fN;
            edgeWeight = weight;
        }

    }
}
