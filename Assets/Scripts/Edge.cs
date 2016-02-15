using UnityEngine;
using System.Collections;

public class Edge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

        public Node startingNode;
        public Node endNode;
        public double edgeWeight;


        public Edge(Node n1, Node n2, double weight)
        {
            startingNode = n1;
            endNode = n2;
            edgeWeight = weight;

        }

    
}
