using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Driver;
using UnityEngine;

namespace Assets.Scripts.Path
{
    class ShortestPathCreator : MonoBehaviour
    {
        private MapController mc;
        private StorylineDriver slDriver;
        private List<GameObject> nodesGameObjects; 
        public float speed = 5.0f;
        public float reachDist = 0.2f;
        public static int currentPoint;

        private List<Node> shortest_path;
        private Map map;



        void Start()
        {
            shortest_path = new List<Node>();
            
        }

        public IEnumerator getMapController()
        {
            yield return new WaitForSeconds(0.5f);

            slDriver = FindObjectOfType<StorylineDriver>();
            if (slDriver != null)
                nodesGameObjects = slDriver.GetNodeGameObjects();
            transform.position = new Vector3(nodesGameObjects[1].transform.position.x, nodesGameObjects[1].transform.position.y, -7);
            mc = FindObjectOfType<MapController>();
            map = mc.getMap();

            foreach (var value in map.getGraph().getVertices().Values)
            {
                foreach (var adj in value.getAdjacentNodes().Keys)
                {
                    print("For node " + value.getID() + ", his adjacent nodes are: " + adj.getID());
                }
            }
        }

        void Update()
        {
            
                shortest_path = slDriver.getShortestPath();
            
            

            if (shortest_path != null)
            {
                if (shortest_path.Any())
                {
                    if (currentPoint < shortest_path.Count)
                    {
                        float dist = Vector3.Distance(shortest_path[currentPoint].getPosition(), transform.position);
                        transform.position = Vector3.MoveTowards(transform.position,
                            shortest_path[currentPoint].getPosition(),
                            Time.deltaTime*speed);

                        if (dist <= reachDist)
                            currentPoint++;
                    }
                }
            }
        }//end of update

        /*public void getShortestPath()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //JOSEPH: When you touch a point of interest on the map, it shows the shortest path from the first node of the nodeList to the touched node.
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);

                if (hit.collider != null)
                {
                    //if (touched)
                        //shortest_path.Clear();

                    //JOSEPH: erase the path renderer and recalculate which node you touched
                    ResetTrails();
                    GameObject recipient = hit.transform.gameObject;
                    Node touchedNode = recipient.GetComponent<Node>();
                    currentPoint = 0;
                    Debug.Log("First node: " + map.getStorypointNodes()[1].getID());
                    Debug.Log("Touched node: " + touchedNode.getID());
                    shortest_path = map.getGraph().shortest_path(map.getStorypointNodes()[1], touchedNode);
                    shortest_path.Reverse();
                    
                    if (shortest_path == null)
                        Debug.Log("Shortest path is null");

                    else
                    {
                       foreach (var n in shortest_path)
                        {
                            print(n.getID());
                        } 
                    }
                    
                    touched = true;
                }
            }
        }

        public void ResetTrails()
        {
            TrailRenderer trail = GetComponent<TrailRenderer>();
            StartCoroutine("DisableTrail", trail);
            if (trail.time < 0)
                trail.time = -trail.time;
            transform.position = new Vector3(nodesGameObjects[1].transform.position.x, nodesGameObjects[1].transform.position.y, -7);
        }

        IEnumerator DisableTrail(TrailRenderer trail)
        {
            if (trail.time < 0)
                yield break;

            yield return new WaitForSeconds(0.01f);

            trail.time = -trail.time;
        }*/

    }

}
