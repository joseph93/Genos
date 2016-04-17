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
}
