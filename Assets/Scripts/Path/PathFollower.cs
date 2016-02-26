using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {

    public Transform[] path;
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public int currentPoint = 0;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	    if (currentPoint < 5)
	    {
	        float dist = Vector3.Distance(path[currentPoint].position, transform.position);

	        transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, Time.deltaTime*speed);

	        //Nipper goes to next point
	        if (dist <= reachDist)
	            currentPoint++;
	    }
	    /*      
        //Nipper goes back to initial point
                if (currentPoint >= path.Length)
                    currentPoint = 0;
        */  
    }

    void OnDrawGizmos() {

        //Draw gizmos lines
        for (int i = 0; i < path.Length; i++) {
            Vector3 pos = path[i].position; //current point PointB
            if (i > 0) {
                Vector3 prev = path[i - 1].position; //previous point PointA
                Gizmos.DrawLine(prev, pos);
            }
        }

        //Draw the gizmos spheres
        if (path.Length > 0) { 
            for(int i = 0; i < path.Length; i++) {
                if(path[i] != null) {
                    Gizmos.DrawSphere(path[i].position, reachDist);				
				}
            }
        }


    }
}
