using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Language;

namespace Assets.Scripts
{

    //Ihcene : StoryLine will only be available with the NipperTour
    public class Storyline
    {
        private StorylineDescription stDescription;
        public int floorsCovered { get; set; }
        public int walkingTimeInMinutes { get; set; }
        public List<Node> nodes { get; set; }

        public Storyline(StorylineDescription stDescription, int fc)
        {
            this.stDescription = stDescription;
            floorsCovered = fc;
            nodes = new List<Node>();
        }

        public StorylineDescription GetStorylineDescription()
        {
            return stDescription;
        }

        public void setStorylineDescription(string title, string descr)
        {
            stDescription.title = title;
            stDescription.description = descr;
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
