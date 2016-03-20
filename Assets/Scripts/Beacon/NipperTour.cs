using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Assets.Scripts.Language;
using Assets.Scripts.Observer_Pattern;
using Assets.Scripts.Path;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class NipperTour : MonoBehaviour
    {
        //private iBeaconHandler beaconHandler;
        private List<Beacon> myBeacons;
        private Map map;
        public Node[] ArrayOfNodes;
        private List<Node> nodeList;
        private List<Node> path;
        private List<StoryPoint> visitedStoryPoints;
        private List<PointOfInterest> pointsOfInterest;
        private List<StoryPoint> storyPoints; 

        public Camera mainCam;
        public GameObject pathCreator;
        private bool touched;
        
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
            
            map = new Map();
            StorylineDescription sd = new StorylineDescription("Nipper Tour", "A guided tour of the Musee des Ondes.");
            Storyline nipperTour = new Storyline(sd, 4);

            storyPoints = new List<StoryPoint>();
            pointsOfInterest = new List<PointOfInterest>();

            foreach (var n in ArrayOfNodes)
            {
                nipperTour.addNode(n);

                if (n.GetFloorNumber() == 2)
                {
                    n.gameObject.SetActive(true);
                }

                if (n is PointOfInterest)
                {
                    pointsOfInterest.Add((PointOfInterest)n);
                }
                    
                

                if (n is StoryPoint)
                {
                    storyPoints.Add((StoryPoint)n);
                }
            }

            storyPoints.Sort();
            

            foreach (StoryPoint sp in storyPoints)
            {
                sp.setTitleAndSummary("Old President's Office", "Stop at the end of the corridor before the bridge to building 18. The door to the " +
                                  "right was the old president’s office.The door is closed, Nipper barks and on the screen " +
                                  "appears a mental image from Nipper with the image of the old office, " +
                                  "as suggested in three drawings by thearchitects Ross and MacDonalds.");
                print("Storypoint with sequential ID: " + sp.getSequentialID());
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

            visitedStoryPoints = new List<StoryPoint>();
            

        }


        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        void Update()
        {
            if (bh != null)
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
        }//end of Update() 

        public List<Node> getNodeList()
        {
            return nodeList;
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

        public bool isInOrder(StoryPoint sp)
        {

            int currentPoi = this.storyPoints.IndexOf(sp);

            StoryPoint[] storyPoints = this.storyPoints.ToArray();

            for (int i = 0; i < currentPoi; i++)
            {
                if (!storyPoints[i].isVisited())
                {
                    return false;
                }
            }

            return true;
        }

        public StoryPoint findLastUnvisitedSp()
        {
            //JOSEPH: find the last object of the visited list.
            int lastIndex = visitedStoryPoints.Count;


            StoryPoint[] storyPoints = this.storyPoints.ToArray();

            return storyPoints.ElementAt(lastIndex);
        }

        public void setStorypointList(List<StoryPoint> spList)
        {
            storyPoints = spList;
        }

        public void setVisitedStorypointList(List<StoryPoint> spList)
        {
            visitedStoryPoints = spList;
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
                    foreach (StoryPoint sp in storyPoints)
                    {
                        if (!sp.isVisited())
                        {
                            if (sp.getBeacon().Equals(b))
                            {
                                if (isInOrder(sp))
                                {
                                    StoryPointView storyPointView = new StoryPointView(sp);
                                    sp.setVisited(true);
                                    visitedStoryPoints.Add(sp);
                                    //JOSEPH: When you're 2 meters or less away from a beacon, make the icon on the map bigger, center the camera on the icon, vibration and the given sound and text.
                                    mainCam.transform.position = new Vector3(sp.x, sp.y, -10);

                                }
                                else
                                {
                                    if (!sp.warned)
                                    {
                                        StoryPoint lastUnvisitedSp = findLastUnvisitedSp();
                                        //Pop up, notify the user that he missed a poi
                                        string description = "You have missed point of interest " +
                                                             lastUnvisitedSp.getSequentialID() +
                                                             ". Please go back and visit it before proceeding.";
                                        sp.displayWarning(description);
                                        sp.warned = true;
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
