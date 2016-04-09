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

        void Start()
        {
            StartCoroutine(decode());
            
        }

        public IEnumerator decode()
        {
            WWW request = new WWW("http://localhost/Map.json");

            yield return request;

            if (string.IsNullOrEmpty(request.error))
            {

                JSONObject jobject = new JSONObject(request.text);
                accessData(jobject);
            }
        }

        public void accessData(JSONObject obj)
        {
            floors = obj.list[0];
            nodes = obj.list[1];
            edges = obj.list[2];
            storylines = obj.list[3];

            floorList = populateFloors();
            edgeList = PopulateEdges();
            storylineList = PopulateStorylines();
            nodeList = populateNodes();
            
            createStartAndEndNode();

            foreach (Node n in nodeList) // for each node in the nodelist
            {
                foreach (Edge e in edgeList) // and for each edge in the edgelist
                {
                    if (n == e.startingNode)
                    {
                        foreach (Node adjacentNode in nodeList)
                        {
                            if (adjacentNode == e.endingNode)
                            {
                                n.addAdjacentNode(adjacentNode, e.edgeWeight); 
                                Debug.Log(n.getID() + " to " + adjacentNode.getID() + " with edge weight " + e.edgeWeight);  
                            }
                             
                        }
                        
                        //e.endingNode.addAdjacentNode(n, e.edgeWeight);
                        //Debug.Log(e.endingNode.getID() + " to " + n.getID());
                    }
                    
                }
            }

            //Debug.Log(nodeList[0] == nodeList[9] ? "8 and 9 are equal." : "8 and 9 are not equal.");
            

            Map map = new Map();
            map.addNodeList(nodeList);
            map.initializeGraph();
  

            foreach (Node n in nodeList)
            {
                Debug.Log("For node " + n.getID() + ", the number of adjacent nodes are: " + n.getAdjacentNodes().Keys.Count);
                foreach (Node adjacentNode in n.getAdjacentNodes().Keys)
                {
                    Debug.Log("For node " + n.getID() + ", his adjacent nodes are: " + adjacentNode.getID());
                }
            }
            

            print("Node list count: " + nodeList.Count);
            print("Edge list count: " + edgeList.Count);

            
            List<Node> path = map.getGraph().shortest_path(nodeList[0], nodeList[3]);
            /*foreach (Node n in path)
            {
                Debug.Log("Im in path loop.");
                Debug.Log(n.getID());
            }*/
            print(path == null ? "Path is null" : "Path is not null.");
        }

        public void createStartAndEndNode()
        {
            foreach (Node n in nodeList)
            {
                foreach (Edge e in edgeList)
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
                        Node p = new PointOfInterest((int)poi.list[0].n, (int)poi.list[2].n, (int)poi.list[3].n,(string)poi.list[4].str, int.Parse(poi.list[6].str));
                       
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
                        foreach (JSONObject t in poi.list[1].list)
                        {
                            //Debug.Log(t.list[0].ToString());
                            p.setLanguage(t.list[0].str);
                            p.setTitle(t.list[1].str);
                        }
                        foreach (JSONObject d in poi.list[5].list)
                        {
                            p.setDescription(d.list[1].str);
                        }
                        tmpNodeList.Add(p);
                    }

                    foreach (JSONObject pot in n.list[1].list) //3rd degree : list of pot
                    {
                        Node pointOfTransition = new PointOfTransition((int)pot.list[0].n, (int)pot.list[2].n, (int)pot.list[3].n, 
                            pot.list[5].str, int.Parse(pot.list[6].str), pot.list[4].str,  pot.list[1].str);
                        tmpNodeList.Add(pointOfTransition);
                        //Debug.Log(pot.ToString() + "\n");
                    }
                
            }

            return tmpNodeList;
        }  
        
    }
}
