using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        private bool scanning = true;
        private List<Beacon> myBeacons = new List<Beacon>();
        public PointOfInterest[] PointOfInterests;
        private List<PointOfInterest> nodeList = new List<PointOfInterest>();
        private Vector2 scrolldistance;
<<<<<<< HEAD
        public Node[] ArrayOfNodes;
        private List<Node> path = new List<Node>();
        private Node n5;
        private Node n2;
        private Node n1;


        //added from pathfollower
        //public Transform[] pathRenderer;
        public float speed = 5.0f;
        public float reachDist = 0.2f; //radius
        public int currentPoint = 0;
        public int sizePath = 7;

        public static GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Node");
        public Transform[] pathRender = new Transform[gameObjects.Length];
     
        public TrailRenderer trail;

=======
        public Camera mainCam;
        private bool detected = false;
>>>>>>> 88769c386d9c464beda79236e006e05ea0826ea5

        //JOSEPH: Initialize the node list.
        void Awake()
        {
            for (int i = 0; i < PointOfInterests.Length; i++)
            {
                nodeList.Add(PointOfInterests[i].GetComponent<PointOfInterest>());
                // Debug.Log("Beacon id for poi " + nodeList[i].id + " is " + nodeList[i].beacon.m_uuid);
}
                //added
                // Initializing Gameobjects
                n1 = ArrayOfNodes[0].GetComponent<Node>();
                n2 = ArrayOfNodes[1].GetComponent<Node>();
                n5 = ArrayOfNodes[2].GetComponent<Node>();
                Graph g = new Graph();
                path = g.shortest_path(n1, n5);


            for (int i = 0; i < gameObjects.Length; i++)
            {
                pathRender[i] = gameObjects[i].transform;
            }


            for (int i = 0; i < path.Count; i++)
            {

                    

                // Debug.Log(path[i].id);
                // ici ca va me retourner le id de tous les Nodes pour le shortest path EXCEPT the first node, exemple : 2,5 ou bien 5. 
                //(you need to consider this when drawing the path)

                // path[i].x = pathRenderer[currentPoint].position.x;
                // path[i].y = pathRenderer[currentPoint].position.y;

            }


        }
        // Use this for initialization
        void Start()
        {
            //DisplayPointOfInterest();
            iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent += OnBluetoothStateChanged;
            iBeaconReceiver.CheckBluetoothLEStatus();
            Debug.Log("Listening for beacons");
        }

        void OnDestroy()
        {
            iBeaconReceiver.BeaconRangeChangedEvent -= OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent -= OnBluetoothStateChanged;
        }

        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        void Update()
        {
            //StartCoroutine(searchForDistanceOfBeacon());
<<<<<<< HEAD
            foreach (Beacon b in myBeacons)
            {
               if (0.00 < b.accuracy && b.accuracy < 2.00)
                {
                    foreach (PointOfInterest poi in nodeList)
                    {
                        poi.enableSpriteRenderer();
                    }
                }
                if (b.accuracy > 2.00)
                {
                   
                }
            }

       
            //added from pathfollower
        //    if (currentPoint < 5)
        //    {
             //   float dist = Vector3.Distance(pathRenderer[currentPoint].position, transform.position); //Vector3.Distance(a,b) is the same as (a-b).magnitude
             //   transform.position = Vector3.MoveTowards(transform.position, pathRenderer[currentPoint].position, Time.deltaTime * speed); //Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta); 

                for (int i = 0; i < path.Count; i++)
                {
                    float dist = Vector3.Distance(path[i].getPosition(), transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, path[i].getPosition(), Time.deltaTime * speed);
               
                //Nipper goes to next point
                //   if (dist <= reachDist)
                //      currentPoint++;
                   
                 }

           


            //    }

=======
            StartCoroutine(searchForDistanceOfBeacon());
>>>>>>> 88769c386d9c464beda79236e006e05ea0826ea5
        }

        private void OnBluetoothStateChanged(BluetoothLowEnergyState newstate)
        {
            switch (newstate)
            {
                case BluetoothLowEnergyState.POWERED_ON:
                    iBeaconReceiver.Init();
                    Debug.Log("It is on, go searching");
                    break;
                case BluetoothLowEnergyState.POWERED_OFF:
                    //iBeaconReceiver.EnableBluetooth();
                    Debug.Log("It is off, switch it on");
                    break;
                case BluetoothLowEnergyState.UNAUTHORIZED:
                    Debug.Log("User doesn't want this app to use Bluetooth, too bad");
                    break;
                case BluetoothLowEnergyState.UNSUPPORTED:
                    Debug.Log("This device doesn't support Bluetooth Low Energy, we should inform the user");
                    break;
                case BluetoothLowEnergyState.UNKNOWN:
                case BluetoothLowEnergyState.RESETTING:
                default:
                    Debug.Log("Nothing to do at the moment");
                    break;
            }
        }

       

        public IEnumerator searchForDistanceOfBeacon()
        {
            yield return new WaitForSeconds(1);
            foreach (Beacon b in myBeacons)
            {
                foreach(PointOfInterest poi in nodeList)
                    {
                        if (!detected)
                        {
                            poi.enableSpriteRenderer();
                            mainCam.transform.position = new Vector3(poi.x, poi.y, -10);
                            detected = true;
                        }
                    }
            }
        }

        private void OnBeaconRangeChanged(List<Beacon> beacons)
        {
            // 
            foreach (Beacon b in beacons)
            {
                if (myBeacons.Contains(b))
                {
                    myBeacons[myBeacons.IndexOf(b)] = b;
                }
                else
                {
                    // this beacon was not in the list before
                    // this would be the place where the BeaconArrivedEvent would have been spawned in the the earlier versions
                    myBeacons.Add(b);
                }
            }
            foreach (Beacon b in myBeacons)
            {
                if (b.lastSeen.AddSeconds(10) < DateTime.Now)
                {
                    // we delete the beacon if it was last seen more than 10 seconds ago
                    // this would be the place where the BeaconOutOfRangeEvent would have been spawned in the earlier versions
                    myBeacons.Remove(b);
                }
            }
        }


    }
}
