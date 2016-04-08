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

        public int startingNodeID { get; set; }
        public int endNodeID { get; set; }
        public float edgeWeight { get; set; }
        public int floorNumber { get; set; }

        public Node startingNode { get; set; }
        public Node endingNode { get; set; }

        public Edge(int n1, int n2, float weight)
        {
            startingNodeID = n1;
            endNodeID = n2;
            edgeWeight = weight;
        }

    }
}
