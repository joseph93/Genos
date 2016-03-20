using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Map
    {

        private List<Storyline> storylines;
        private Graph mapGraph;
        private List<Node> nodes; 

        public Map()
        {
            storylines = new List<Storyline>();
            nodes = new List<Node>();
            mapGraph = new Graph();
        }

        public void addStoryline(Storyline sl)
        { 
            storylines.Add(sl);
        }

        public void initializeGraph()
        {
            //JOSEPH: Populate the graph with all the nodes in the storyline.
            foreach (Node n in nodes)
            {
                mapGraph.InsertNewVertex(n);
            }
            
        }

        public void addNode(Node n)
        {
            mapGraph.InsertNewVertex(n);
            nodes.Add(n);
        }

        public void addNodeList(List<Node> nodeList)
        {
            nodes = nodeList;
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
