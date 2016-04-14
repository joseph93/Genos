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
        private FreeRoamingDriver frDriver;
        private Node[] nodesArray;
        private List<Node> nodesGameObjects;
        private List<GameObject> shortestPathGameObjects;
        public float speed = 5.0f;
        public float reachDist = 0.2f;
        public static int currentPoint;

        private List<Node> shortest_path;
        private Map map;

        public static bool touched;

        void Start()
        {
            shortest_path = new List<Node>();
            shortestPathGameObjects = new List<GameObject>();
            StartCoroutine(getMapController());
        }

        public IEnumerator getMapController()
        {
            yield return new WaitForSeconds(0.5f);

            slDriver = FindObjectOfType<StorylineDriver>();
            frDriver = FindObjectOfType<FreeRoamingDriver>();
            if (frDriver != null)
                nodesArray = frDriver.getArrayOfNodes();
            //if (slDriver != null)
              //  nodesGameObjects = slDriver.GetNodeGameObjects();
            //transform.position = new Vector3(nodesGameObjects[1].transform.position.x, nodesGameObjects[1].transform.position.y, -7);
            mc = FindObjectOfType<MapController>();
            map = mc.getMap();
        }

        void Update()
        {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touched = true;

                shortestPathGameObjects.Clear();

                shortest_path = frDriver.getShortestPath();

                foreach (var n in shortest_path)
                {
                    foreach (var g in nodesArray)
                    {
                        if (g.getID() == n.getID())
                        {
                            shortestPathGameObjects.Add(g.gameObject);
                        }
                    }
                }

                
            }

            if (shortest_path != null)
            {
                    if (currentPoint < shortest_path.Count)
                    {
                        float dist = Vector3.Distance(shortestPathGameObjects[currentPoint].transform.position,
                            transform.position);
                        transform.position = Vector3.MoveTowards(transform.position,
                            shortestPathGameObjects[currentPoint].transform.position,
                            Time.deltaTime*speed);

                        if (dist <= reachDist)
                            currentPoint++;
                    }
               
            }
        }//end of update

        

    }

}
