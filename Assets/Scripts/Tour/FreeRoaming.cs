﻿using Assets.Scripts.Observer_Pattern;
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
        protected List<Node> nodeList;
        protected List<PointOfInterest> pointsOfInterest;

        public Camera mainCam;

        protected iBeaconHandler bh;
        
        //JOSEPH: Initialize the node list.
        void Awake()
        {
            
        }
        // Use this for initialization
        void Start()
        {
            bh = FindObjectOfType<iBeaconHandler>();
            myBeacons = new List<Beacon>();
            nodeList = new List<Node>();
            pointsOfInterest = new List<PointOfInterest>();
        }

        public List<Node> getNodeList()
        {
            return nodeList;
        }


        public void initializeLists(Node[] arrayOfNodes)
        {
            foreach (var n in arrayOfNodes)
            {
                nodeList.Add(n);

                n.gameObject.SetActive(n.GetFloorNumber() == 2);

                if (n is PointOfInterest)
                {
                    pointsOfInterest.Add((PointOfInterest) n);
                }
            }
        }

        public void initializePoi()
        {
            foreach (StoryPoint poi in pointsOfInterest)
            {
                poi.setTitleAndSummary("Test", "test");
            }
        }


        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        void Update()
        {
            if (bh != null)
                myBeacons = bh.getBeacons();

            StartCoroutine(searchForPoiBeacon(0.05f));

        }


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
                                p.setTitleAndSummary("Test", "test");
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





