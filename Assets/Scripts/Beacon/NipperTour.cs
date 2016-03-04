using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        
        private List<Beacon> myBeacons = new List<Beacon>();
        private Map map;
        public Node[] ArrayOfNodes;
        private List<Node> path;
        
        public Camera mainCam;

        private ModalWindow modalWindow;

        private UnityAction myOkAction;
        private UnityAction myCancelAction;

        //JOSEPH: Initialize the node list.
        void Awake()
        {
            modalWindow = ModalWindow.Instance();

            myOkAction = new UnityAction(modalWindow.closePanel);
            myCancelAction = new UnityAction(modalWindow.closePanel);
            //added
            //to know last index of last scene loaded
            //FloorManager.setLastScene(Application.loadedLevelName);
            //SceneManager.GetActiveScene(); 
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
            }
            map.addStoryline(nipperTour);
            Node n1 = ArrayOfNodes[0].GetComponentInChildren<Node>();
            Node n2 = ArrayOfNodes[1].GetComponentInChildren<Node>();
            Node n3 = ArrayOfNodes[2].GetComponentInChildren<Node>();
            Node n4 = ArrayOfNodes[3].GetComponentInChildren<Node>();

            n1.addAdjacentNode(new Dictionary<Node, float>() { { n2, 1.0f }, { n3, 0.5f } });
            n2.addAdjacentNode(new Dictionary<Node, float>() { { n3, 2.0f } });
            n3.addAdjacentNode(new Dictionary<Node, float>() { { n4, 3.0f } });
            map.addNode(n1);
            map.addNode(n2);
            map.addNode(n3);
            map.addNode(n4);
            
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
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject recipient = hit.transform.gameObject;
                    Node touchedNode = recipient.GetComponent<Node>();
                    List<Storyline> storylines = map.GetStorylines();
                    List<Node> nodeList = storylines[0].GetNodes();
                    foreach (Node n in nodeList)
                    {
                        if (touchedNode.id == n.id)
                        {
                            touchedNode = n;
                        }

                    }

                    path = map.getGraph().shortest_path(nodeList[0], touchedNode);
                    path.Reverse();
                    foreach (Node n in path)
                    {
                        Debug.Log("Node traversed: " + n.id);
                    }
                }

            }

          
        }


        private void OnBluetoothStateChanged(BluetoothLowEnergyState newstate)
        {
            switch (newstate)
            {
                case BluetoothLowEnergyState.POWERED_ON:
                    //added
                    //SceneManager.UnloadScene("Bluetooth");
                   // FloorManager.setLastScene(Application.loadedLevelName);
                    //SceneManager.LoadScene(FloorManager.getLastScene());

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

        public List<Node> returnPathWithTouch()
        {
            return path;
        } 

        public IEnumerator searchForDistanceOfBeacon(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            foreach (Beacon b in myBeacons)
            {
                List<Storyline> storylines = map.GetStorylines();
                List<Node> nodeList = storylines[0].GetNodes();
                if (b.accuracy > 2.00 && b.accuracy < 6.00)
                {
                    foreach (PointOfInterest poi in nodeList)
                    {
                        if (!poi.isDetected())
                        {
                            if (poi.getBeacon().m_uuid.ToLower().Equals(b.UUID.ToLower()))
                            {
                                poi.playBeforeSound();
                            }
                        }
                    }
                }
                if (b.accuracy < 2.00)
                {
                    
                    foreach (PointOfInterest poi in nodeList)
                    {
                        if (!poi.isDetected())
                        {
                            if (poi.getBeacon().m_uuid.ToLower().Equals(b.UUID.ToLower()))
                            {
                                poi.makeIconBigger();
                                mainCam.transform.position = new Vector3(poi.x, poi.y, -10);
                                Vibration.Vibrate(1000);
                                poi.popUpSound();
                                modalWindow.Choice("Here is the building of 1920, and here are the bathrooms of the 1936 building.", myOkAction, myCancelAction);
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
