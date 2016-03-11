using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Scripts.Observer_Pattern;
using UnityEngine.Events;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        private Vector2 scrolldistance;
        private List<Beacon> myBeacons = new List<Beacon>();
        private Map map;
        public Node[] ArrayOfNodes;
        private List<Node> path;
        private PointOfInterest lastVisitedPoi;
        
        public Camera mainCam;
        public GameObject pathCreator;

        private RaycastHit hit;
        private LayerMask touchInputMask;

        private bool touched;

        //JOSEPH: Initialize the node list.
        void Awake()
        {
            
        }
        // Use this for initialization
        void Start()
        {
            iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent += OnBluetoothStateChanged;
            iBeaconReceiver.CheckBluetoothLEStatus();
            Debug.Log("Listening for beacons");


            map = new Map();
            Storyline nipperTour = new Storyline("Nipper Tour", 4);

            foreach (Node n in ArrayOfNodes)
            {
                nipperTour.addNode(n);
            }

            map.addStoryline(nipperTour);
            Node n1 = ArrayOfNodes[0].GetComponentInChildren<Node>();
            Node n2 = ArrayOfNodes[1].GetComponentInChildren<Node>();
            Node n3 = ArrayOfNodes[2].GetComponentInChildren<Node>();
            Node n4 = ArrayOfNodes[3].GetComponentInChildren<Node>();


            n1.addListOfAdjacentNodes(new Dictionary<Node, float>() { { n2, 1.0f }, { n3, 6.0f } });
            n2.addListOfAdjacentNodes(new Dictionary<Node, float>() { { n3, 2.0f } });
            n3.addListOfAdjacentNodes(new Dictionary<Node, float>() { { n4, 3.0f } });

            //Add all the nodes that are in the Nipper Tour storyline in the map.
            map.initializeGraph(0);
            
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
            StartCoroutine(searchForDistanceOfBeacon(0.05f));

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                
                //JOSEPH: When you touch a point of interest on the map, it shows the shortest path from the first node of the nodeList to the touched node.
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                
                if (hit.collider != null)
                {
                    if (touched)
                        path.Clear();
                    
                    
                        ResetTrails();
                        GameObject recipient = hit.transform.gameObject;
                        Node touchedNode = recipient.GetComponent<Node>();
                        List<Storyline> storylines = map.GetStorylines();
                        List<Node> nodeList = storylines[0].GetNodes(); 
                            
                        path = map.getGraph().shortest_path(nodeList[0], touchedNode);
                        path.Reverse();
                        touched = true;
                    
                }

            }

          
        }

        public void ResetTrails()
        {
            List<Storyline> storylines = map.GetStorylines();
            List<Node> nodeList = storylines[0].GetNodes();
            pathCreator.transform.position = new Vector3(nodeList[0].x, nodeList[0].y, -7);
            Debug.Log("Trail Renderer time: " + pathCreator.GetComponent<TrailRenderer>().time);
            Invoke("DisableTrail", 0.01f);
            Debug.Log("Trail Renderer time: " + pathCreator.GetComponent<TrailRenderer>().time);
            if (pathCreator.GetComponent<TrailRenderer>().time < 0f)
                pathCreator.GetComponent<TrailRenderer>().time = 100.0f;
            Debug.Log("Trail Renderer time: " + pathCreator.GetComponent<TrailRenderer>().time);
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

        public List<Node> returnPathWithTouch()
        {
            return path;
        }

        public void DisableTrail()
        {
           pathCreator.GetComponent<TrailRenderer>().time = -5.0f;
        }

        public IEnumerator searchForDistanceOfBeacon(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            foreach (Beacon b in myBeacons)
            {
                List<Storyline> storylines = map.GetStorylines();
                List<Node> nodeList = storylines[0].GetNodes();
                /*if (b.accuracy > 2.00 && b.accuracy < 6.00)
                {
                    foreach (PointOfInterest poi in nodeList)
                    {
                        if (!poi.isDetected())
                        {
                            if (poi.getBeacon().m_uuid.ToLower().Equals(b.UUID.ToLower()))
                            {
                                //JOSEPH: When you approach a beacon and you're 2 to 6 meters away (typewrite sound)
                                poi.playBeforeSound();
                            }
                        }
                    }
                }*/
                if (b.accuracy < 2.00)
                {
                    foreach (PointOfInterest poi in nodeList)
                    {
                        if (!poi.isVisited())
                        {
                            if (poi.getBeacon().m_uuid.ToLower().Equals(b.UUID.ToLower()))
                            {
                                BeaconView bv = new BeaconView(poi);
                                poi.setDescription("Stop at the end of the corridor before the bridge to building 18. The door to the " +
                                                   "right was the old president’s office.The door is closed, Nipper barks and on the screen " +
                                                   "appears a mental image from Nipper with the image of the old office, " +
                                                   "as suggested in three drawings by thearchitects Ross and MacDonalds.");
                                poi.setVisited(true);
                                //JOSEPH: When you're 2 meters or less away from a beacon, make the icon on the map bigger, center the camera on the icon, vibration and the given sound and text.
                                mainCam.transform.position = new Vector3(poi.x, poi.y, -10);
                                lastVisitedPoi = poi;
                            }
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
