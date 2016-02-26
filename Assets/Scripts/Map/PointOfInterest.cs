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
        public new string name;
        public iBeaconServer beacon { get; set; }
        public GameObject BeaconGameObject;
        private bool visited;

        void Awake()
        {
            beacon = BeaconGameObject.GetComponent<iBeaconServer>();
        }
        public PointOfInterest(int id, int x, int y, int floorNumber, string name) : base(id, x, y, floorNumber)
        {
            this.name = name;
            description = "";
            visited = false;
        }

        public void enableSpriteRenderer()
        {
            GetComponent<Renderer>().enabled = true;
        }

        public iBeaconServer getBeacon()
        {
            return beacon;
        }
    }
}
