using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Path
{
    class ShortestPathCreator : MonoBehaviour
    {
        private FreeRoamingDriver _freeRoamingDriver;
        public float speed = 5.0f;
        public float reachDist = 0.2f;
        public static int currentPoint;

        private List<Node> shortest_path;
        private Map map;
        private bool touched;

        void Start()
        {
            _freeRoamingDriver = FindObjectOfType<FreeRoamingDriver>();
            
            shortest_path = new List<Node>();
        }

        void Update()
        {
            if(_freeRoamingDriver != null)
                map = _freeRoamingDriver.getMap();

            getShortestPath();

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

        public void getShortestPath()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //JOSEPH: When you touch a point of interest on the map, it shows the shortest path from the first node of the nodeList to the touched node.
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);

                if (hit.collider != null)
                {
                    if (touched)
                        shortest_path.Clear();

                    //JOSEPH: erase the path renderer and recalculate which node you touched
                    ResetTrails();
                    GameObject recipient = hit.transform.gameObject;
                    Node touchedNode = recipient.GetComponent<Node>();

                    currentPoint = 0;
                    shortest_path = map.getGraph().shortest_path(map.GetPoiNodes()[0], touchedNode);
                    shortest_path.Reverse();
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
            transform.position = new Vector3(map.GetPoiNodes()[0].x, map.GetPoiNodes()[0].y, -7);
        }

        IEnumerator DisableTrail(TrailRenderer trail)
        {
            if (trail.time < 0)
                yield break;

            yield return new WaitForSeconds(0.01f);

            trail.time = -trail.time;
        }

    }

}
