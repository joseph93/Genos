using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Storyline
    {
        public string name { get; set; }
        public string description { get; set; }
        public int floorsCovered { get; set; }
        public int walkingTimeInMinutes { get; }
        public List<PointOfInterest> pointOfInterests { get; }

        public Storyline(string name, int fc)
        {
            this.name = name;
            floorsCovered = fc;
            pointOfInterests = new List<PointOfInterest>();
        }

        public void addPointOfInterest(PointOfInterest poi)
        {
            pointOfInterests.Add(poi);
        }

        public void removePointOfInterest(PointOfInterest poi)
        {
            pointOfInterests.Remove(poi);
        }

        
    }
}
