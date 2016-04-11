using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Scripts.Exhibition_Content;
using Assets.Scripts.Language;
using SimpleJSON;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapController : MonoBehaviour
    {
        private JSONObject floors;
        private JSONObject nodes;
        private JSONObject edges;
        private JSONObject storylines;

        private List<FloorPlan> floorList;
        private List<Edge> edgeList;
        private List<Node> nodeList;
        private List<Storyline> storylineList;    
        private List<Node> poiList = new List<Node>();
        private List<Node> storypointList = new List<Node>();
        private Map map;

        void Start()
        {


        }



        public void accessData(JSONObject obj)
        {
            floors = obj.list[0];                           //this is the root for the floors JSON object
            nodes = obj.list[1];                            //this is the root for the nodes JSON object
            edges = obj.list[2];                            //this is the root for the edges JSON object
            storylines = obj.list[3];                       //this is the root for the storylines JSON object

            floorList = populateFloors();                   //parse the floors
            edgeList = PopulateEdges();                     //parse the edges
            storylineList = PopulateStorylines();           //parse the storylines
            nodeList = populateNodes();                     //parse the nodes

            
            initializePoiAndStorypointLists();      //populate the list that contains the points of interest and the other that contains storypoints
            
            createStartAndEndNode(); //transform the id's of the nodes in the edges into Node objects

            setAdjacencies();              //add all the adjacencies to all the nodes
            
            map = new Map();
            map.setPoiList(poiList);
            map.setStorypointList(storypointList);
            map.setFloorplanList(floorList);
            map.setStorylineList(storylineList);
            map.initializeGraph(poiList);

            


        }

        public Map getMap()
        {
            return map;
        }

        public void setAdjacencies()
        {
            foreach (Node n in poiList) // for each node in the nodelist
            {
                foreach (Edge e in edgeList) // and for each edge in the edgelist
                {
                    if (n == e.startingNode) //if n in the node list is equal to the starting node of the edge then go in
                    {
                        foreach (Node adjacentNode in poiList) //look for the end node
                        {
                            if (adjacentNode == e.endingNode)
                            {
                                //add the adjacency both ways
                                n.addAdjacentNode(adjacentNode, e.edgeWeight);
                                adjacentNode.addAdjacentNode(n, e.edgeWeight);
                            }
                        }
                    }
                }
            }
        }

        public void initializePoiAndStorypointLists()
        {
            foreach (var n in nodeList)
            {
                if (n.GetType() == typeof (PointOfInterest))
                {
                    poiList.Add(n);
                }
                if (n.GetType() == typeof (POS))
                {
                    storypointList.Add(n);
                }
                if (n.GetType() == typeof (PointOfTransition))
                {
                    poiList.Add((PointOfTransition) n);
                    storypointList.Add(n);
                }
            }
        }

        public void createStartAndEndNode()
        {
            foreach (var n in poiList)
            {
                foreach (var e in edgeList)
                {
                    if (n.getID() == e.startingNodeID)
                    {
                        e.startingNode = n;
                    }
                    if (n.getID() == e.endNodeID)
                    {
                        e.endingNode = n;
                    }
                }
            }
        }


        public List<FloorPlan> populateFloors()
        {
            List<FloorPlan> floorList = new List<FloorPlan>();

            foreach (JSONObject f in floors.list)
            {
               FloorPlan floor = new FloorPlan(f.list[0].str, f.list[1].str, (int)f.list[2].n, (int)f.list[3].n);
               floorList.Add(floor);
            }

            return floorList;
        }

        public List<Edge> PopulateEdges()
        {
            List<Edge> edgeList = new List<Edge>();

            foreach (JSONObject e in edges.list)
            {
                Edge edge = new Edge((int)e.list[0].n, (int)e.list[1].n, e.list[2].n);
                edgeList.Add(edge);
            }

            return edgeList;
        }

        public List<Storyline> PopulateStorylines()
        {
            List<Storyline> storylineList = new List<Storyline>();

            foreach (JSONObject s in storylines.list)
            {
                Storyline storyline = new Storyline((int)s.list[0].n, (int)s.list[5].n, s.list[1].str, s.list[2].str);
                foreach (JSONObject nodeid in s.list[4].list)
                {
                    storyline.addToPath((int)nodeid.n);
                }
                storylineList.Add(storyline);
            }

            return storylineList;
        }

        public List<Node> populateNodes()
        {
            List<Node> tmpNodeList = new List<Node>();
            foreach (JSONObject n in nodes.list) //1st degree : list of nodes (only 1 element)
            {
                    foreach (JSONObject poi in n.list[0].list) //3rd degree : list of pois
                    { 
                       // Debug.Log(poi.ToString() + "\n");
                        PointOfInterest p = new PointOfInterest((int)poi.list[0].n, (int)poi.list[2].n, (int)poi.list[3].n, poi.list[4].str, int.Parse(poi.list[6].str));
                        
                        foreach (JSONObject b in poi.list[7].list)
                        {
                            //Debug.Log(b.ToString() + "\n");
                            iBeaconServer beacon = new iBeaconServer(poi.list[7].list[0].str, int.Parse(poi.list[7].list[1].str), int.Parse(poi.list[7].list[2].str));
                            p.setBeacon(beacon);
                        }
                        foreach (JSONObject c in poi.list[8].list) //Media list
                        {
                            //Debug.Log(c.ToString() + "\n");
                            foreach (JSONObject i in poi.list[8].list[0].list) // Image list
                            {
                                ExhibitionContent image = new Image(i.list[0].str, i.list[1].str, i.list[2].str);
                                p.addContent(image);
                                //Debug.Log(i.ToString() + "\n");
                            }
                            foreach (JSONObject v in poi.list[8].list[1].list) // Video list
                            {
                                string videoPath = v.list[0].str;
                                string mp4Path = videoPath.Remove(videoPath.Length - 3) + "mp4"; //Converting video .mov to mp4
                                ExhibitionContent video = new Video(mp4Path, v.list[1].str, v.list[2].str);
                                p.addContent(video);
                                //Debug.Log(v.list[0].ToString() + "\n");
                            }
                            
                        }

                        for (int i = 0; i < poi.list[1].Count; i++) //Add all the poi description in the point of interest
                        {
                            PoiDescription poiD = new PoiDescription(poi.list[1].list[i].list[1].str, poi.list[5].list[i].list[1].str, poi.list[1].list[i].list[0].str);
                            p.addPoiDescription(poiD);
                        }

                        foreach (var sp in poi.list[9].list)
                        {
                            POS storyPoint = new POS((int)poi.list[0].n, (int)poi.list[2].n, (int)poi.list[3].n, poi.list[4].str, int.Parse(poi.list[6].str), (int)sp.list[0].n);

                            for (int i = 0; i < sp.list[1].Count; i++) // description and title JSON object
                            {
                                StoryPointDescription spd = new StoryPointDescription(sp.list[1].list[i].list[1].str, sp.list[2].list[i].list[1].str, sp.list[1].list[i].list[0].str);
                                storyPoint.addStorypointDescription(spd);
                            }

                            /*foreach (var c in sp.list[3].list) //storypoint content list
                            {
                                foreach (var i in sp.list[3].list[0].list) //image list
                                {
                                    if (i != null)
                                    {
                                        ExhibitionContent image = new Image(i.list[0].str, i.list[2].str, i.list[1].str);
                                        storyPoint.addContent(image);
                                    }
                                }

                                foreach (var v in sp.list[3].list[1].list) //video list
                                {
                                    if (v != null)
                                    {
                                        ExhibitionContent video = new Video(v.list[1].str, v.list[2].str, v.list[1].str);
                                        storyPoint.addContent(video);
                                    }
                                }
                            }*/
                            tmpNodeList.Add(storyPoint);
                        }



                        tmpNodeList.Add(p);
                    }//end of poi foreach

                    foreach (JSONObject pot in n.list[1].list) //3rd degree : list of pot
                    {
                        PointOfTransition pointOfTransition = new PointOfTransition((int)pot.list[0].n, (int)pot.list[2].n, (int)pot.list[3].n, 
                            pot.list[5].str, int.Parse(pot.list[6].str), pot.list[4].str,  pot.list[1].str);
                        tmpNodeList.Add(pointOfTransition);
                        //Debug.Log(pot.ToString() + "\n");
                    }
                
            }//end of root foreach

            return tmpNodeList;
        }   
        
    }
}
