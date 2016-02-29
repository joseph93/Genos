using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    //Ihcene : StoryLine will only be available with the NipperTour
    public class Storyline
    {
        public string name { get; set; }
        public string description { get; set; }
        public int floorsCovered { get; set; }
        public int walkingTimeInMinutes { get; set; }
        public List<Node> nodes { get; set; }

        public Storyline(string name, int fc)
        {
            this.name = name;
            floorsCovered = fc;
            nodes = new List<Node>();
        }

        public void addNode(Node poi)
        {
            nodes.Add(poi);
        }

        public void removeNode(Node poi)
        {
            nodes.Remove(poi);
        }

        public List<Node> GetNodes()
        {
            return nodes;
        }

        
    }
}
