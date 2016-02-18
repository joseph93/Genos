using UnityEngine;
using System.Collections;

public class Edge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

        public Node startingNode { get; set; }
        public Node endNode { get; set; }
        public double edgeWeight { get; set; }


        public Edge(Node n1, Node n2, double weight)
        {
            startingNode = n1;
            endNode = n2;
            edgeWeight = weight;
        }

    
}
