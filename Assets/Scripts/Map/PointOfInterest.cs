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
        private string name;
        private List<Beacon> myBeacons;
        private bool visited;
        public GameObject poiIBeacon;

        public PointOfInterest(int id, int x, int y, int floorNumber, string name) : base(id, x, y, floorNumber)
        {
            this.name = name;
            description = "";
            visited = false;
        }

        public void DisplayPointOfInterest()
        {
            GameObject nipper = Instantiate(poiIBeacon, new Vector3(x, y, -7), Quaternion.identity) as GameObject;
            // nipper.transform.SetParent(transform);
        }

        public void DisappearIcon()
        {
            Destroy(GameObject.FindGameObjectWithTag("Icon"));
        }

        IEnumerator searchForDistanceOfBeacon()
        {
            yield return new WaitForSeconds(1);
            foreach (Beacon b in myBeacons)
            {
                if (0.00 < b.accuracy && b.accuracy < 2.00)
                {
                    DisplayPointOfInterest();
                }
                if (b.accuracy > 2.00)
                {
                    DisappearIcon();
                }
            }
        }

        private void OnBeaconRangeChanged(List<Beacon> beacons)
        {
            // 
            foreach (Beacon b in beacons)
            {
                if (myBeacons.Contains(b))
                {
                    myBeacons[myBeacons.IndexOf(b)] = b;
                }
                else
                {
                    // this beacon was not in the list before
                    // this would be the place where the BeaconArrivedEvent would have been spawned in the the earlier versions
                    myBeacons.Add(b);
                }
            }
            foreach (Beacon b in myBeacons)
            {
                if (b.lastSeen.AddSeconds(10) < DateTime.Now)
                {
                    // we delete the beacon if it was last seen more than 10 seconds ago
                    // this would be the place where the BeaconOutOfRangeEvent would have been spawned in the earlier versions
                    myBeacons.Remove(b);
                }
            }
        }
    }
}
