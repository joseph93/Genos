using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class MapTest : MonoBehaviour
    {
        public GameObject NipperBeacon;
        void Start()
        {
            testGraph();
        }

        void Update()
        {
            
        }
        public void testGraph()
        {
            //iBeaconReceiverExample bc = new iBeaconReceiverExample();

            Node n1 = new Node(5, 5);
            Node n2 = new Node(10, 10);
            Edge edge1 = new Edge(n1, n2, 5);
        }


        public void MakeNipper()
        {
            GameObject nipper = Instantiate(NipperBeacon, new Vector3(1, 0, 0), Quaternion.identity) as GameObject;
            nipper.transform.SetParent(transform);
        }
    }
}
