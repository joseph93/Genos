using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Map : MonoBehaviour
    {

        private List<Storyline> storylines;
        private Graph mapGraph;

        public GameObject[] gameObjects;
        private Transform[] pathRenderer;


        void Awake()
        {
            pathRenderer = new Transform[gameObjects.Length];
            for (int i = 0; i < gameObjects.Length; i++)
            {
                pathRenderer[i] = gameObjects[i].transform;
            }
        }

        void Update()
        {
            
        }

        public Map()
        {
            storylines = new List<Storyline>();
            mapGraph = new Graph();
        }

        public void addStoryline(Storyline sl)
        { 
            storylines.Add(sl);
        }

        public void initializeGraph()
        {
            List<Node> nodeList = storylines[0].GetNodes();
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
