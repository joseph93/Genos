using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Path;
using UnityEngine;

namespace Assets.Scripts
{
    public class Map 
    {

        private List<Storyline> storylines;
        private Graph mapGraph;
        private List<Node> nodes;
        private List<FloorPlan> floors; 

        public Map()
        {
            storylines = new List<Storyline>();
            nodes = new List<Node>();
            mapGraph = new Graph();
            floors = new List<FloorPlan>();
        }
        

        public void addStoryline(Storyline sl)
        { 
            storylines.Add(sl);
        }

        public void addStorylineList(List<Storyline> slList)
        {
            storylines = slList;
        }

        public void initializeGraph()
        {
            //JOSEPH: Populate the graph with all the nodes in the storyline.
            foreach (Node n in nodes)
            {
                mapGraph.InsertNewVertex(n);
                Debug.Log("added node: " + n.getID());
            }
            
        }

        public void addNode(Node n)
        {
            mapGraph.InsertNewVertex(n);
            nodes.Add(n);
        }

        //Add nodes to the node list and add them to the graph also.
        public void addNodeList(List<Node> nodeList)
        {
            nodes = nodeList;
        }

        public void addFloorPlans(List<FloorPlan> floorList)
        {
            floors = floorList;
        }

        public Graph getGraph()
        {
            return mapGraph;
        }

        public List<Storyline> GetStorylines()
        {
            return storylines;
        }

        public List<Node> GetNodes()
        {
            return nodes;
        }


    }
}
