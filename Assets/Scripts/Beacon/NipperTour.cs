using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        private List<Beacon> myBeacons = new List<Beacon>();
<<<<<<< HEAD
        private Map map;
=======
        public PointOfInterest[] PointOfInterests;
        private List<PointOfInterest> nodeList = new List<PointOfInterest>();
        private Vector2 scrolldistance;
>>>>>>> 42cb9eef07718cc2400897a67846fc0af9903aef

        public Node[] ArrayOfNodes;
        private List<Node> path = new List<Node>();

       

        public Camera mainCam;
<<<<<<< HEAD
        private bool detected;
=======
        private bool detected = false;
>>>>>>> 42cb9eef07718cc2400897a67846fc0af9903aef

        //JOSEPH: Initialize the node list.
        void Awake()
        {
            map = new Map();
            Storyline nipperTour = new Storyline("Nipper Tour", 4);
            
            for (int i = 0; i < ArrayOfNodes.Length; i++)
            {
                nipperTour.addNode(ArrayOfNodes[i].GetComponentInChildren<Node>());
                // Debug.Log("Beacon id for poi " + nodeList[i].id + " is " + nodeList[i].beacon.m_uuid);
            }
            map.addStoryline(nipperTour);

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
<<<<<<< HEAD
            StartCoroutine(searchForDistanceOfBeacon(0.3f));
=======
            //StartCoroutine(searchForDistanceOfBeacon());

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


            StartCoroutine(searchForDistanceOfBeacon());


            
>>>>>>> 42cb9eef07718cc2400897a67846fc0af9903aef
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

       

        public IEnumerator searchForDistanceOfBeacon(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            foreach (Beacon b in myBeacons)
            {
                if (b.accuracy < 2.00)
                {
                    List<Storyline> storylines = map.GetStorylines();
                    List<Node> nodeList = storylines[0].GetNodes();
                    foreach (PointOfInterest poi in nodeList)
                    {
                        if (!detected)
                        {
                            poi.enableSpriteRenderer();
                            mainCam.transform.position = new Vector3(poi.x, poi.y, -10);
                            Vibration.Vibrate(1000);
                            detected = true;
                    }
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
