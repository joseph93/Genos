using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Assets.Scripts.Observer_Pattern;
using Assets.Scripts.Path;
using UnityEngine.Events;

namespace Assets.Scripts {
    public class NipperTour : MonoBehaviour
    {
        //private iBeaconHandler beaconHandler;
        private List<Beacon> myBeacons;
        private Map map;
        public Node[] ArrayOfNodes;
        private List<Node> nodeList;
        private List<Node> path;
        private List<PointOfInterest> visitedPointOfInterests;
        private List<PointOfInterest> pointsOfInterest;  
        
        public Camera mainCam;
        public GameObject pathCreator;

        private bool touched;

        private ModalWindow modalWindow;

        public Sprite iconImage;
        private UnityAction yesAction;
        private UnityAction noAction;

        private iBeaconHandler bh;

        public float minSwipeDistX;
        private Vector2 startPos;

        private UI_Manager ui_Manager;
        public Animator anim;

        //JOSEPH: Initialize the node list.
        void Awake()
        {
            
        }
        // Use this for initialization
        void Start()
        {
            ui_Manager = FindObjectOfType<UI_Manager>();
            bh = FindObjectOfType<iBeaconHandler>();
            
            path = new List<Node>();
            nodeList = new List<Node>();
            
            modalWindow = ModalWindow.Instance();
            yesAction = new UnityAction(modalWindow.closePanel);
            noAction = new UnityAction(modalWindow.closePanel);

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

            map.addNodeList(nipperTour.GetNodes());
            nodeList = map.GetNodes();
            //Add all the nodes that are in the Nipper Tour storyline in the map.
            map.initializeGraph();

            pointsOfInterest = new List<PointOfInterest>();
            foreach (PointOfInterest p in nodeList)
            {
                pointsOfInterest.Add(p);
                print("Adding poi with sequential ID : " + p.getSequentialID());
            }

            visitedPointOfInterests = new List<PointOfInterest>();

        }
        

        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        void Update()
        {
            if(bh != null)
                myBeacons = bh.getBeacons();

            StartCoroutine(searchForDistanceOfBeacon(0.05f));

            if (Input.touchCount > 0)

            {

                Touch touch = Input.touches[0];
                
                switch (touch.phase)

                {

                    case TouchPhase.Began:

                        startPos = touch.position;

                        break;



                    case TouchPhase.Ended:
                        
                        float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                        if (swipeDistHorizontal > minSwipeDistX)

                        {

                            float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                            if (swipeValue < 0) //left swipe
                            {
                               ui_Manager.disableBoolAnimator(anim); 
                            }

                            //MoveLeft ();

                        }
                        break;
                }
            }

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

                        ShortestPathCreator.currentPoint = 0;
                        path = map.getGraph().shortest_path(nodeList[0], touchedNode);
                        path.Reverse();
                        touched = true;
                    
                }

            }
        } 

        public void ResetTrails()
        {
            TrailRenderer trail = pathCreator.GetComponent<TrailRenderer>();
            StartCoroutine("DisableTrail", trail);
            if (trail.time < 0)
                trail.time = -trail.time;
            pathCreator.transform.position = new Vector3(nodeList[0].x, nodeList[0].y, -7);
        }

        public List<Node> returnPathWithTouch()
        {
            return path;
        }

        IEnumerator DisableTrail(TrailRenderer trail)
        {
            if (trail.time < 0)
                yield break;

            yield return new WaitForSeconds(0.01f);

            trail.time = -trail.time;
        }

        public bool isInOrder(PointOfInterest poi)
        {
            int currentPoi = pointsOfInterest.IndexOf(poi);

            PointOfInterest[] pois = pointsOfInterest.ToArray();
            
            for (int i = 0; i < currentPoi; i++)
            {
                if (!pois[i].isVisited())
                {
                    print("I'm checking its order");
                    return false;
                }
            }

            return true;
        }

        public PointOfInterest findLastUnvisitedPoi()
        {
            //JOSEPH: find the last object of the visited list.
            int lastIndex = visitedPointOfInterests.Count;
            

            PointOfInterest[] pois = pointsOfInterest.ToArray();

            return pois.ElementAt(lastIndex);
        }

        public IEnumerator searchForDistanceOfBeacon(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            foreach (Beacon b in myBeacons)
            {
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
                    foreach (PointOfInterest poi in pointsOfInterest)
                    {
                        if (!poi.isVisited())
                        {
                            if (poi.beacon.Equals(b))
                            {
                                if (isInOrder(poi))
                                {
                                    //attach the observer to the poi.
                                    BeaconView bv = new BeaconView(poi);
                                    poi.setDescription(
                                        "Stop at the end of the corridor before the bridge to building 18. The door to the " +
                                        "right was the old president’s office.The door is closed, Nipper barks and on the screen " +
                                        "appears a mental image from Nipper with the image of the old office, " +
                                        "as suggested in three drawings by thearchitects Ross and MacDonalds.");
                                    poi.setVisited(true);
                                    visitedPointOfInterests.Add(poi);
                                    //JOSEPH: When you're 2 meters or less away from a beacon, make the icon on the map bigger, center the camera on the icon, vibration and the given sound and text.
                                    mainCam.transform.position = new Vector3(poi.x, poi.y, -10);
                                    
                                }
                                else
                                {
                                    if (!poi.warned)
                                    {
                                        PointOfInterest lastUnvisitedPoi = findLastUnvisitedPoi();
                                        //Pop up, notify the user that he missed a poi
                                        string description = "You have missed point of interest " +
                                                             lastUnvisitedPoi.getSequentialID() +
                                                             ". Please go back and visit it before proceeding.";
                                        modalWindow.Choice(description, iconImage, yesAction, noAction);
                                        poi.warned = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
}
