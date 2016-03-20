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
    public class NipperTour : FreeRoaming
    {
        private List<Node> path;
        private List<StoryPoint> visitedStoryPoints;
        private List<StoryPoint> storyPoints; 
        
        public GameObject pathCreator;
        private bool touched;

        //JOSEPH: Initialize the node list.
        void Awake()
        {

        }
        // Use this for initialization
        void Start()
        {

            path = new List<Node>();
            
            StorylineDescription sd = new StorylineDescription("Nipper Tour", "A guided tour of the Musee des Ondes.");
            Storyline nipperTour = new Storyline(sd, 4);

            storyPoints = new List<StoryPoint>();

            initializeStoryPointList(nipperTour);

            //JOSEPH: sorts the storypoints list by sequential ID (from 1 to ...)
            storyPoints.Sort();
            

            foreach (StoryPoint sp in storyPoints)
            {
                sp.setTitleAndSummary("Old President's Office", "Stop at the end of the corridor before the bridge to building 18. The door to the " +
                                  "right was the old president’s office.The door is closed, Nipper barks and on the screen " +
                                  "appears a mental image from Nipper with the image of the old office, " +
                                  "as suggested in three drawings by thearchitects Ross and MacDonalds.");
                print("Storypoint with sequential ID: " + sp.getSequentialID());
            }

            visitedStoryPoints = new List<StoryPoint>();
        }

        public void initializeStoryPointList(Storyline nipperTour)
        {
            foreach (var n in ArrayOfNodes)
            {
                if (n is StoryPoint)
                {
                    storyPoints.Add((StoryPoint) n);
                }
            }
        }


        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        void Update()
        {
            if (bh != null)
                myBeacons = bh.getBeacons();

            StartCoroutine(searchForStorypointBeacon(0.05f));

            swipePanelLeft();

            getShortestPath();
        }

        public void getShortestPath()
        {
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

//end of Update() 

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

        public IEnumerator searchForStorypointBeacon(float seconds)
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
