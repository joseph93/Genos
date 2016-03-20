using Assets.Scripts.Observer_Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class FreeRoaming : MonoBehaviour
    {
        protected List<Beacon> myBeacons;
        protected Map map;
        public Node[] ArrayOfNodes;
        protected List<Node> nodeList;
        protected List<PointOfInterest> pointsOfInterest;

        public Camera mainCam;

        protected iBeaconHandler bh;

        public float minSwipeDistX;
        protected Vector2 startPos;

        protected UI_Manager ui_Manager;
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

          
            nodeList = new List<Node>();

        
            map = new Map();
          
            pointsOfInterest = new List<PointOfInterest>();

            initializeLists();
           
            Node n1 = ArrayOfNodes[0].GetComponentInChildren<Node>();
            Node n2 = ArrayOfNodes[1].GetComponentInChildren<Node>();
            Node n3 = ArrayOfNodes[2].GetComponentInChildren<Node>();
            Node n4 = ArrayOfNodes[3].GetComponentInChildren<Node>();


            n1.addListOfAdjacentNodes(new Dictionary<Node, float>() { { n2, 1.0f }, { n3, 6.0f } });
            n2.addListOfAdjacentNodes(new Dictionary<Node, float>() { { n3, 2.0f } });
            n3.addListOfAdjacentNodes(new Dictionary<Node, float>() { { n4, 3.0f } });
            //This should be removed because we need to use the JSON file

            map.addNodeList(nodeList);
            //adding the list of nodes in the graph
            map.initializeGraph();
            

        }

        public void initializeLists()
        {
            foreach (var n in ArrayOfNodes)
            {
                nodeList.Add(n);
                if (n.GetFloorNumber() == 2)
                {
                    n.gameObject.SetActive(true);
                }

                if (n is PointOfInterest)
                {
                    pointsOfInterest.Add((PointOfInterest) n);
                }
            }
        }


        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        void Update()
        {
            if (bh != null)
                myBeacons = bh.getBeacons();

            StartCoroutine(searchForPoiBeacon(0.05f));
            
            swipePanelLeft();

           

            }

        public void swipePanelLeft()
        {
            if (Input.touchCount > 0)

            {
                Touch touch = Input.touches[0];

                switch (touch.phase)

                {
                    case TouchPhase.Began:

                        startPos = touch.position;

                        break;


                    case TouchPhase.Ended:

                        float swipeDistHorizontal =
                            (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

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
        }

//end of Update()
        public IEnumerator searchForPoiBeacon(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            foreach (Beacon b in myBeacons)
            {
                if (b.accuracy < 2.00f)
                {

                    foreach (PointOfInterest p in pointsOfInterest)
                    {
                        if(!p.isDetected())
                        {

                            if (p.getBeacon().Equals(b))
                            {
                                PointOfInterestView poiView = new PointOfInterestView(p);
                                p.setDetected(true);
                                mainCam.transform.position = new Vector3(p.x, p.y, -10);
                            }

                        }

                    }

                }  
                
            }
        }//end of searchForDistanceOFBeacon()
        
        }
    }





