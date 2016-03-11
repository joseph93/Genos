using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Path
{
    class ShortestPathCreator : MonoBehaviour
    {
        public GameObject NipperTour;
        private NipperTour nipper;
        public float speed = 5.0f;
        public float reachDist = 0.2f;
        public static int currentPoint;

        private List<Node> shortest_path;

        void Start()
        {
            nipper = NipperTour.GetComponent<NipperTour>();
        }

        void Update()
        {
            //JOSEPH: Get the path from the shortest_path metod in NipperTour and draw it.
            shortest_path = nipper.returnPathWithTouch();
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
        }

    }

}
