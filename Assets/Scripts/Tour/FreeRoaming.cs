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
            //bh = FindObjectOfType<iBeaconHandler>();
            myBeacons = new List<Beacon>();
            nodeList = new List<Node>();
            pointsOfInterest = new List<PointOfInterest>();
        }

        public List<Node> getNodeList()
        {
            return nodeList;
        }

        public List<PointOfInterest> getPoiList()
        {
            return pointsOfInterest;
        }
        public void setBeaconList(List<Beacon> beacons)
        {
            myBeacons = beacons;
        }


        public void initializeLists(List<Node> poiList)
        {
            nodeList = poiList;

            foreach (var n in nodeList)
            {
                if (n.GetType() == typeof(PointOfInterest))
                {
                    pointsOfInterest.Add((PointOfInterest)n);
                    print("Added poi: " + n.getID());
                }
            }
        }



        //Reviewer Ihcene: Update was fixed, for the rendering of the beacon, for each frames
        // Update is called once per frame
        void Update()
        {
            /*if (bh != null)
                myBeacons = bh.getBeacons();

            StartCoroutine(searchForPoiBeacon(0.05f));
            */
        }


        public IEnumerator searchForPoiBeacon(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            foreach (Beacon b in myBeacons)
            {
                if (b.accuracy < 3.00f)
                {
                    foreach (PointOfInterest p in pointsOfInterest)
                    {
                        if (!p.isDetected())
                        {

                            if (p.getBeacon().Equals(b))
                            {
                                PointOfInterestView poiView = new PointOfInterestView(p);
                                p.setDetected(true);
                                Camera.main.transform.position = new Vector3(p.gameObject.transform.position.x, p.gameObject.transform.position.y, -10);
                                print("im inside");
                            }

                        }

                    }

                }

            }
        }//end of searchForDistanceOFBeacon()

    }
}

