using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Language;
using Assets.Scripts.Observer_Pattern;
using Assets.Scripts.Path;
using UnityEngine;

namespace Assets.Scripts
{

    //Ihcene : StoryLine will only be available with the NipperTour
    public class Storyline : MonoBehaviour
    {
        private int id;
        private StorylineDescription stDescription;
        public int floorsCovered { get; set; }
        private List<StoryPoint> visitedStoryPoints;
        private List<StoryPoint> storyPoints;
        
        private List<Beacon> myBeacons;
        private readonly List<Node> nodeList;

        public Camera mainCam;

        public Storyline(int id, StorylineDescription stDescription, int fc)
        {
            this.id = id;
            this.stDescription = stDescription;
            floorsCovered = fc;
            nodeList = new List<Node>();
            storyPoints = new List<StoryPoint>();
            visitedStoryPoints = new List<StoryPoint>();
            myBeacons = new List<Beacon>();
        }

        // Use this for initialization
        void Awake()
        {
            
        }

        void Start()
        {
        }

        void Update()
        {
            
        }

        public void setBeaconList(List<Beacon> beacons)
        {
            myBeacons = beacons;
        }

        public List<Node> getNodeList()
        {
            return nodeList;
        }

        public StorylineDescription GetStorylineDescription()
        {
            return stDescription;
        }

        public void setStorylineDescription(string title, string descr)
        {
            stDescription.title = title;
            stDescription.description = descr;
        }



        public void initializeLists(Node[] arrayOfNodes)
        {
            foreach (var n in arrayOfNodes)
            {
                nodeList.Add(n);

                n.gameObject.SetActive(n.GetFloorNumber() == 2);

                if (n is StoryPoint)
                {
                    storyPoints.Add((StoryPoint)n);
                    print("Added storypoint " + n.getID());
                }
            }
            //JOSEPH: sorts the storypoints list by sequential ID (from 1 to ...)
            storyPoints.Sort();
        }


        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        

        //Check if this storypoint follows the sequence
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

            //JOSEPH: return the first missed storypoint
            return storyPoints.ElementAt(lastIndex);
        }

        //JOSEPH: for unit testing
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
                    print("Im near the beacon.");
                    foreach (StoryPoint sp in storyPoints)
                    {
                        print("I'm in the for loop.");
                        if (!sp.isVisited())
                        {
                            print("Storypoint is not visited.");
                            if (sp.getBeacon().Equals(b))
                            {
                                print("Its the same beacon");
                                if (isInOrder(sp))
                                {
                                    print("It's in order.");
                                    sp.setTitleAndSummary("test", "test");
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
