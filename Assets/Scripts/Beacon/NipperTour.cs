using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        private List<Beacon> myBeacons = new List<Beacon>();
        private Map map;
        public GameObject[] icons;
        public Node[] ArrayOfNodes;

        public float speed = 5.0f;
        public float reachDist = 0.2f;
        public int currentPoint;
        private List<Node> path;

        public Camera mainCam;
        private bool detected;

        //JOSEPH: Initialize the node list.
        void Awake()
        {
            
        }
        // Use this for initialization
        void Start()
        {
            //DisplayPointOfInterest();
            iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
            iBeaconReceiver.BluetoothStateChangedEvent += OnBluetoothStateChanged;
            iBeaconReceiver.CheckBluetoothLEStatus();
            Debug.Log("Listening for beacons");
            map = new Map();
            Storyline nipperTour = new Storyline("Nipper Tour", 4);

            foreach (Node n in ArrayOfNodes)
            {
                nipperTour.addNode(n);
                Debug.Log("Adding in Nipper Tour node " + n.id);
            }
            map.addStoryline(nipperTour);
            Node n1 = ArrayOfNodes[0].GetComponentInChildren<Node>();
            Node n2 = ArrayOfNodes[1].GetComponentInChildren<Node>();
            Node n3 = ArrayOfNodes[2].GetComponentInChildren<Node>();
            Node n4 = ArrayOfNodes[3].GetComponentInChildren<Node>();

            List<Storyline> storylines = map.GetStorylines();
            List<Node> nodeList = storylines[0].GetNodes();
            foreach (PointOfInterest poi in nodeList)
            {
                Debug.Log("Name of poi1: " + poi.poiName);
            }

            n1.addAdjacentNode(new Dictionary<Node, float>() { { n2, 1.0f }, { n3, 10.0f } });
            n2.addAdjacentNode(new Dictionary<Node, float>() { { n3, 1.0f } });
            n3.addAdjacentNode(new Dictionary<Node, float>() { { n4, 3.0f } });
            map.initializeGraph(0);

            //JOSEPH: Set the path creator to the start node of ShortestPath (n1 in this case, will be changed after)
            transform.position = new Vector3(n1.x, n1.y, 5);
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
                    //Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                    if (hit.collider != null)
                    {
                        Debug.Log("touched it.");
                            //Node endNode = recipient.GetComponent<Node>();
                            //Debug.Log("Node hit: " + endNode.id);
                        
                        /*path = map.getGraph().shortest_path(ArrayOfNodes[0], endNode);
                        path.Reverse();
                        if (currentPoint < path.Count)
                        {
                            float dist = Vector3.Distance(path[currentPoint].getPosition(), transform.position);
                            transform.position = Vector3.MoveTowards(transform.position,
                                path[currentPoint].getPosition(),
                                Time.deltaTime*speed);

                            if (dist <= reachDist)
                                currentPoint++;
                        }*/
                    }
                
            }
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

                    //added
                    SceneManager.LoadScene("Bluetooth");

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
                        if (!poi.isDetected())
                        {
                            if (poi.getBeacon().m_uuid.ToLower().Equals(b.UUID.ToLower()))
                            {
                                poi.makeIconBigger();
                                mainCam.transform.position = new Vector3(poi.x, poi.y, -10);
                                Vibration.Vibrate(1000);
                                //Debug.Log("The beacon in beacon list is beacon " + b.UUID);
                                //Debug.Log("The beacon of the poi is beacon " + poi.getBeacon().m_uuid);
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
