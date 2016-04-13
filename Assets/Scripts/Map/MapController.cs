using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        private readonly List<Node> poiList = new List<Node>();
        private readonly List<Node> storypointList = new List<Node>();
        private Map map;


        void Start()
        {
            decode();

        }

        public void decode()
        {
            TextAsset request = Resources.Load<TextAsset>("mapData");

            JSONObject jobject = new JSONObject(request.text);
            accessData(jobject);
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

            /*foreach (Node n in storypointList) // for each node in the nodelist
            {
                foreach (Edge e in edgeList) // and for each edge in the edgelist
                {
                    if (n == e.startingNode) //if n in the node list is equal to the starting node of the edge then go in
                    {
                        foreach (Node adjacentNode in storypointList) //look for the end node
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
            }*/
        }

        public void initializePoiAndStorypointLists()
        {
            foreach (var n in nodeList)
            {
                if (n.GetType() == typeof(PointOfInterest))
                {
                    poiList.Add(n);
                }
                if (n.GetType() == typeof(POS))
                {
                    storypointList.Add(n);
                }
                if (n.GetType() == typeof(PointOfTransition))
                {
                    poiList.Add((PointOfTransition)n);
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
                string imagePath = f.list[1].str.Substring(13, 6);
                FloorPlan floor = new FloorPlan(f.list[0].n.ToString(), imagePath, (int)f.list[2].n, (int)f.list[3].n);
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
                Storyline storyline = new Storyline((int)s.list[0].n, s.list[6].Count);
                foreach (JSONObject nodeid in s.list[3].list)
                {
                    storyline.addToPath((int)nodeid.n);
                }

                for (int i = 0; i < s.list[1].list.Count; i++)
                {
                    Description storylineDescription = new Description(s.list[1].list[i].list[1].str,
                        s.list[2].list[i].list[1].str, s.list[1].list[i].list[0].str);
                    storyline.addStorylineDescription(storylineDescription);
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
                //Debug.Log(n.ToString());
                foreach (JSONObject poi in n.list[0].list) //3rd degree : list of pois
                {
                    //print(poi.ToString());
                    PointOfInterest p = new PointOfInterest((int)poi.list[0].n, (int)poi.list[3].n, (int)poi.list[4].n, (int)poi.list[5].n);

                    foreach (JSONObject b in poi.list[6].list)
                    {
                        //Debug.Log(b.ToString() + "\n");
                        iBeaconServer beacon = new iBeaconServer(poi.list[6].list[0].str, int.Parse(poi.list[6].list[1].str), int.Parse(poi.list[6].list[2].str));
                        p.setBeacon(beacon);
                    }
                    //foreach (JSONObject c in poi.list[7].list) //Media list
                    //{
                        //Debug.Log(c.ToString() + "\n");
                        /*foreach (JSONObject i in poi.list[8].list[0].list) // Image list
                        {
                            ExhibitionContent image = new Image(i.list[0].str, i.list[1].str, i.list[2].str);
                            p.addContent(image);
                            //Debug.Log(i.ToString() + "\n");
                        }*/
                        if (poi.list[7].list[1].list.Count > 0)
                        {
                            foreach (JSONObject v in poi.list[7].list[1].list) // Video list
                            {
                                string videoPath = v.list[0].str.Remove(0, 13);
                                //Remove extension type of the file
                                //print(videoPath);
                                Video video = new Video(videoPath, v.list[1].str, v.list[2].str);
                                p.addContent(video);
                                //Debug.Log(v.list[0].ToString() + "\n");
                            }
                        }

                        if (poi.list[7].list[2].list.Count > 0)
                        {
                            foreach (var a in poi.list[7].list[2].list) // Audio list
                            {
                                string audioPath = a.list[0].str.Remove(0, 13);
                                string mp3Path = audioPath.Remove(audioPath.Length - 4);
                                //print(mp3Path);
                                Audio audio = new Audio(mp3Path, a.list[1].str, a.list[2].str);
                                p.addContent(audio);
                            }
                        }
                    //}

                    for (int i = 0; i < poi.list[1].Count; i++) //Add all the poi description in the point of interest
                    {
                        string title = RemoveHtml.RemoveHtmlTags(poi.list[1].list[i].list[1].str);
                        string descr = RemoveHtml.RemoveHtmlTags(poi.list[2].list[i].list[1].str);
                        Description poiD = new Description(title, descr, poi.list[1].list[i].list[0].str);
                        p.addPoiDescription(poiD);
                    }

                    foreach (var sp in poi.list[8].list)
                    {
                        POS storyPoint = new POS((int)poi.list[0].n, (int)poi.list[3].n, (int)poi.list[4].n, (int)poi.list[5].n, (int)sp.list[1].n);
                        
                        for (int i = 0; i < sp.list[2].Count; i++) // description and title JSON object
                        {
                            string title = RemoveHtml.RemoveHtmlTags(sp.list[2].list[i].list[1].str);
                            string descr = RemoveHtml.RemoveHtmlTags(sp.list[3].list[i].list[1].str);
                            Description spd = new Description(title, descr, sp.list[2].list[i].list[0].str);
                            storyPoint.addPoiDescription(spd);
                        }

                        //foreach (var c in sp.list[4].list) //storypoint content list
                        //{
                            /*foreach (var i in sp.list[4].list[0].list) //image list
                            {
                                if (i != null)
                                {
                                    ExhibitionContent image = new Image(i.list[0].str, i.list[2].str, i.list[1].str);
                                    storyPoint.addContent(image);
                                }
                            }*/

                            if (sp.list[4].list[1].list.Count > 0)
                            {
                                foreach (var v in sp.list[4].list[1].list) //video list
                                {
                                    string videoPath = v.list[0].str.Remove(0, 13);
                                    //Remove extension type of the file
                                    //print(videoPath);
                                    Video video = new Video(videoPath, v.list[1].str, v.list[2].str);
                                    storyPoint.addContent(video);
                                }
                            }

                            if (sp.list[4].list[2].list.Count > 0)
                            {
                                foreach (var a in sp.list[4].list[2].list) //audio list
                                {
                                    string audioPath = a.list[0].str.Remove(0, 13);
                                    string mp3Path = audioPath.Remove(audioPath.Length - 4);
                                    //print(mp3Path);
                                    Audio audio = new Audio(mp3Path, a.list[1].str, a.list[2].str);
                                    storyPoint.addContent(audio);
                                }
                            }
                        //}
                        tmpNodeList.Add(storyPoint);
                    }



                    tmpNodeList.Add(p);
                }//end of poi foreach

                foreach (JSONObject pot in n.list[1].list) //3rd degree : list of pot
                {
                    PointOfTransition pointOfTransition = new PointOfTransition((int)pot.list[0].n, (int)pot.list[2].n, (int)pot.list[3].n, (int)pot.list[4].n, pot.list[1].str);
                    tmpNodeList.Add(pointOfTransition);
                    //Debug.Log(pot.ToString() + "\n");
                }

            }//end of root foreach

            return tmpNodeList;
        }

    }
}