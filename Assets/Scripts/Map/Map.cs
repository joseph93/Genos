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

        public Map()
        {
            storylines = new List<Storyline>();
            mapGraph = new Graph();
        }

        public void addStoryline(Storyline sl)
        { 
            storylines.Add(sl);
        }

        public void initializeGraph(int i)
        {
            //JOSEPH: Populate the graph with all the nodes in the storyline.
            List<Node> nodeList = storylines[i].GetNodes();
            foreach (Node n in nodeList)
            {
                mapGraph.InsertNewVertex(n);
            }
            
        }

        public void addNode(Node n)
        {
            mapGraph.InsertNewVertex(n);
        }

        public Graph getGraph()
        {
            return mapGraph;
        }

        public void displayShortestPath()
        {
            
        }

        public List<Storyline> GetStorylines()
        {
            return storylines;
        } 

    }
}
