using UnityEngine;
using System.Collections;

public class PathFollowerOverview : MonoBehaviour
{

    public Transform[] path;
    public float speed = 5.0f;
    public float reachDist = 0.2f;
    public int currentPoint;
    public int sizeOfPath = Length;

    public static int Length { get; private set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (currentPoint < path.Length)
        {
            float dist = Vector3.Distance(path[currentPoint].position, transform.position);

            transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, Time.deltaTime * speed);

            //Nipper goes to next point
            if (dist <= reachDist)
                currentPoint++;
        }
    }
/*
    void OnDrawGizmos()
    {

        //Draw gizmos lines
        for (int i = 0; i < path.Length; i++)
        {
            Vector3 pos = path[i].position; //current point PointB
            if (i > 0)
            {
                Vector3 prev = path[i - 1].position; //previous point PointA
                Gizmos.DrawLine(prev, pos);
            }
        }

        //Draw gizmos spheres
        if (path.Length > 0)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                {
                    float radius = 0.2f;
                    Gizmos.DrawWireSphere(path[i].position, radius);
                    //Gizmos.DrawSphere(path[i].position, reachDist);				
                }
            }
        }


    }
*/
}
