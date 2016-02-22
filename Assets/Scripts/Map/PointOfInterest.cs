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
        private new string name;
        private GameObject beacon;
        private bool visited;
        

        public PointOfInterest(int id, int x, int y, int floorNumber, string name) : base(id, x, y, floorNumber)
        {
            this.name = name;
            description = "";
            visited = false;
        }

        public GameObject getBeacon()
        {
            return beacon;
        }

        
    }
}
