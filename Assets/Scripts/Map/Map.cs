﻿using System;
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

        //For demo
        public void initializeLists(Node[] arrayOfNodes)
        {
            foreach (var n in arrayOfNodes)
            {
                addNode(n);
            }
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
            //mapGraph.InsertNewVertex(n);
            poiList.Add(n);
            storypointList.Add(n);
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

		public Storyline GetStoryline(int slId)
		{
			foreach (var s in storylines) 
			{
				if (s.id == slId)
					return s;
			}
			return null;
		}

        public List<Node> GetPoiNodes()
        {
            return poiList;
        }

        public List<Node> getStorypointNodes()
        {
            return storypointList;
        }

        public void startStoryline(List<Node> storypointList, int slID)
        {
            //set the correct nodes for the corresponding storyline
            //storylines[slID].initializeLists(storypointList);

            foreach (var sl in storylines)
            {
                if (sl.id == slID)
                {
                    sl.initializeLists(storypointList);
                    break;
                }
            }
        }

        /*
         * Set camera zoom height and width
         * Display floorPlan image according to their x-y scaled
         */
        public List<Node> orderedPath(List<Node> unorderedPath, int slID)
        {
            
            List<Node> orderedPath = new List<Node>();

            foreach (Storyline s in storylines)
            {
                if (s.id == slID)
                { 
                    foreach (var id in s.getPath())
                    {
                        foreach (var n in unorderedPath)
                        {
                            if (n.getID() == id)
                            {
                                orderedPath.Add(n);
                                break;
                            }
                        }

                     }
                 }
             }
             return orderedPath;
        }
            
        }

    }

