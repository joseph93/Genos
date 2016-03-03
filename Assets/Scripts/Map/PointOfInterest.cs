using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class PointOfInterest : Node
    {
        private string description;
        public string poiName;
        public iBeaconServer beacon { get; set; }
        public GameObject BeaconGameObject;
        private bool visited;
        private bool detected;

        void Awake()
        {
            beacon = BeaconGameObject.GetComponent<iBeaconServer>();
        }
        public PointOfInterest(int id, int x, int y, int floorNumber, string poiName) : base(id, x, y, floorNumber)
        {
            this.poiName = poiName;
            description = "";
            visited = false;
            detected = false;
        }

        public void makeIconBigger()
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
            detected = true;
        }

        public bool isDetected()
        {
            return detected;
        }

        public iBeaconServer getBeacon()
        {
            return beacon;
        }
    }
}
