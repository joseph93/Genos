using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Path;
using UnityEngine;

namespace Assets.Scripts
{
<<<<<<< HEAD
    public class Map 
=======
    public class Map : MonoBehaviour
>>>>>>> db331fa2573640c9f08838b5d86878657f6d420a
    {

        private List<Storyline> storylines;
        private readonly Graph mapGraph;
        private List<Node> poiList;
        private List<Node> storypointList;
        private List<FloorPlan> floors;
        
        

        public Map()
        {
            storylines = new List<Storyline>();
            poiList = new List<Node>();
            storypointList = new List<Node>();
            mapGraph = new Graph();
            floors = new List<FloorPlan>();
        }

        public void addStoryline(Storyline sl)
        { 
            storylines.Add(sl);
        }

        public void setStorylineList(List<Storyline> slList)
        {
            storylines = slList;
        }

        public void initializeGraph(List<Node> nl)
        {
            
            //JOSEPH: Populate the graph with all the nodes in the storyline.
            foreach (Node n in nl)
            {  
              mapGraph.InsertNewVertex(n);  
            }

            mapGraph.InitializeVertices();
        }

        public void addNode(Node n)
        {
            mapGraph.InsertNewVertex(n);
            poiList.Add(n);
        }

        //Add nodes to the node list and add them to the graph also.
        public void setPoiList(List<Node> nodeList)
        {
            poiList = nodeList;
        }

        public void setStorypointList(List<Node> spList)
        {
            storypointList = spList;
        }

        public void setFloorplanList(List<FloorPlan> floorList)
        {
            floors = floorList;
        }

        public List<FloorPlan> getFloors()
        {
            return floors;
        }

        public Graph getGraph()
        {
            return mapGraph;
        }

        public List<Storyline> GetStorylines()
        {
            return storylines;
        }

        public List<Node> GetPoiNodes()
        {
            return poiList;
        }

        public List<Node> getStorypointNodes()
        {
            return storypointList;
        }

        public void startStoryline(int slID)
        {
            //display the nodes of the correct storyline
            //display the floors of the correct storyline
            storylines[slID].initializeLists(storypointList);
        }

<<<<<<< HEAD
        
        public List<Node> orderedPath()
        {
=======
        /*
         * Set camera zoom height and width
         * Display floorPlan image according to their x-y scaled
         */
        public List<Node> orderedPath()
        {
            
>>>>>>> db331fa2573640c9f08838b5d86878657f6d420a
            List<Node> orderedPath = new List<Node>();

            foreach (Storyline s in storylines)
            {
<<<<<<< HEAD
                foreach (Node n in s.getNodeList())
                {
                    foreach (var id in s.getPath())
=======
                foreach (var id in s.getPath())
                {
                    foreach (var n in storypointList)
>>>>>>> db331fa2573640c9f08838b5d86878657f6d420a
                    {
                        if (n.getID() == id)
                        {
                            orderedPath.Add(n);
<<<<<<< HEAD
=======
                            break;
>>>>>>> db331fa2573640c9f08838b5d86878657f6d420a
                        }

                    }
                }
<<<<<<< HEAD
=======
                
>>>>>>> db331fa2573640c9f08838b5d86878657f6d420a
            }
            return orderedPath;
        }

    }
}
