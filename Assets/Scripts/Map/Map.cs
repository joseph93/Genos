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

        /*
         * Convert width and height of x-coordinate to match the scale in unity
         */
        public float XCoordinatesConversion(float x, float mapWidth)
        {
            /*
                x-> xFromJSON - widthMAPFromJSON/2
                y-> -(yFromJSON) + heightMAPFromJSON/2
            */

            // float xMapUnity = 5.76f;
            // float xMapPixel = 2313f;
            float xScaled = (2 * 5.76f * mapWidth) / 2313f;
            float xConverted = x - mapWidth / 2;
            float xConvertedScaled = (xConverted * (2 * xScaled)) / mapWidth;
            return xConvertedScaled;
        }

        /*
         * Convert width and height of y-coordinate to match the scale in unity
         */
        public float YCoordinatesConversion(float y, float mapHeight)
        {
            // float yMapUnity = 5.74f;
            // float yMapPixel = 2328f;
            float yScaled = (2 * 5.74f * mapHeight) / 2328f;
            float yConverted = -(y) + mapHeight / 2;
            float yConvertedScaled = (yConverted * (2 * yScaled)) / mapHeight;
            return yConvertedScaled;
        }

        /*
         * Display poi or pot according to their attributes
         */
        public void DisplayNode()
        {

            int blue = 0;
            int green = 3;

            Sprite nodeSprite;
            string nodeColorEditor; // show name of color of the sprite in editor (optional)
            GameObject newNode;

            foreach (Node n in poiList)
            {


                if (n.GetType() == typeof(PointOfInterest)) //check if poi or pot at runtime type
                {
                    newNode = GameObject.Instantiate(nodePrefabPOI) as GameObject; //not exist in this current object must add as
                    newNode.transform.parent = GameObject.Find("FloorManager").transform;
                    newNode.SetActive(true);

                    //if id

                    //TODO: need to take specific type of node depending on their type
                    switch (n.color)
                    {
                        case "green":
                            nodeSprite = nodeSprites[green];
                            nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                            newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                            newNode.GetComponent<Node>().setX(XCoordinatesConversion(n.x, floors[0].getImageWidth()));
                            newNode.GetComponent<Node>().setY(YCoordinatesConversion(n.y, floors[0].getImageHeight()));
                            //                          newNode.GetComponent<Node>().setID(n.getID());
                            //                          newNode.GetComponent<Node>().setFloorNumber(floor.getFloorNumber());
                            newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                            break;

                        default:
                            break;
                    }
                }
                else if (n.GetType() == typeof(PointOfTransition)) //check poi or pot at runtime type
                {
                    newNode = GameObject.Instantiate(nodePrefabPOT) as GameObject; //not exist in this current object must add as
                    newNode.transform.parent = GameObject.Find("FloorManager").transform;
                    newNode.SetActive(true);

                    switch (n.color)
                    {
                        case "blue":
                            nodeSprite = nodeSprites[blue];
                            nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                            newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                            newNode.GetComponent<Node>().setX(XCoordinatesConversion(n.x, floors[0].getImageWidth()));
                            newNode.GetComponent<Node>().setY(YCoordinatesConversion(n.y, floors[0].getImageHeight()));
                            //                          newNode.GetComponent<Node>().setID(n.getID());
                            //                          newNode.GetComponent<Node>().setFloorNumber(floor.getFloorNumber());
                            newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                            break;
                        default:
                            break;
                    }
                }


            } //foreach
        }

=======
>>>>>>> 9c3942faed3322c0f083373e423ed2a89136442f
        /*
         * Set camera zoom height and width
         * Display floorPlan image according to their x-y scaled
         */
        public void DisplayMap(FloorPlan floorPlan)
        {
            //floorplan.cs
            
        }


    }
}
