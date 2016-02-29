using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Path
{
    class ShortestPathScript : MonoBehaviour
    {
        public float speed = 5.0f;
        public float reachDist = 0.2f;
        public int currentPoint;
        private List<Node> path;

        void Update()
        {

            if (currentPoint < path.Count)
            {
                float dist = Vector3.Distance(path[currentPoint].getPosition(), transform.position);
                transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].getPosition(),
                    Time.deltaTime*speed);

                if (dist <= reachDist)
                    currentPoint++;
            }
        }

        public void setPath(List<Node> path)
        {
            this.path = path;
        }
        
    }
}
