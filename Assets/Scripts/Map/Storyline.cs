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
        private string title;
        private string description;
        public int floorsCovered { get; set; }
        private List<POS> visitedStoryPoints;
        private List<POS> storyPoints;
        private List<int> path;   
        
        private List<Beacon> myBeacons;
        private List<Node> nodeList;

        public Camera mainCam;

        public Storyline(int id, int fc, string title1, string description1)
        {
            this.id = id;
            this.title = title1;
            description = description1;
            floorsCovered = fc;
            nodeList = new List<Node>();
            storyPoints = new List<POS>();
            visitedStoryPoints = new List<POS>();
            myBeacons = new List<Beacon>();
            path = new List<int>();
        }

        // Use this for initialization
        void Awake()
        {
            
        }

        void Start()
        {
            nodeList = new List<Node>();
            storyPoints = new List<POS>();
            visitedStoryPoints = new List<POS>();
            myBeacons = new List<Beacon>();
            path = new List<int>();
        }

        void Update()
        {
            
        }

        public void setBeaconList(List<Beacon> beacons)
        {
            myBeacons = beacons;
        }

        public void addToPath(int nodeID)
        {
            path.Add(nodeID);
        }

        public List<int> getPath()
        {
            return path;
        } 

        public List<Node> getNodeList()
        {
            return nodeList;
        }

        public List<POS> getStorypointList()
        {
            return storyPoints;
        } 
        
        public void initializeLists(List<Node> spList)
        {
            nodeList = spList;

            foreach (var sp in nodeList)
            {
                if (sp.GetType() == typeof (POS))
                {
                    storyPoints.Add((POS)sp);
                }
            }

        }


        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        

        //Check if this storypoint follows the sequence
        public bool isInOrder(POS sp)
        {

            int currentPoi = this.storyPoints.IndexOf(sp);

            POS[] poss = this.storyPoints.ToArray();

            for (int i = 0; i < currentPoi; i++)
            {
                if (!poss[i].isVisited())
                {
                    return false;
                }
            }

            return true;
        }

        public POS findLastUnvisitedSp()
        {
            //JOSEPH: find the last object of the visited list.
            int lastIndex = visitedStoryPoints.Count;


            POS[] poss = this.storyPoints.ToArray();

            //JOSEPH: return the first missed storypoint
            return poss.ElementAt(lastIndex);
        }

        //JOSEPH: for unit testing
        public void setStorypointList(List<POS> spList)
        {
            storyPoints = spList;
        }

        public void setVisitedStorypointList(List<POS> spList)
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
                   // print("Im near the beacon.");
                    foreach (POS sp in storyPoints)
                    {
                        //print("I'm in the for loop.");
                        if (!sp.isVisited())
                        {
                            //print("Storypoint is not visited.");
                            if (sp.getBeacon().Equals(b))
                            {
                                //print("Its the same beacon");
                                if (isInOrder(sp))
                                {
                                    //print("It's in order.");
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
                                        POS lastUnvisitedSp = findLastUnvisitedSp();
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
