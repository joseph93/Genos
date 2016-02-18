using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {

    public Transform[] path;
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public int currentPoint = 0;
    public Color rayColor = Color.yellow;

    /*
    private LineRenderer lineRenderer;
	private float counter;
	private float dist;
	public float lineDrawSpeed = 6f;
	*/

	// Use this for initialization
	void Start () {
        /*
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPosition (0, path[currentPoint].position);
		lineRenderer.SetWidth (.45f, .45f);
		dist = Vector3.Distance (path[currentPoint].position, transform.position);
		*/
	}
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(path[currentPoint].position, transform.position);

        transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, Time.deltaTime*speed);

        //Goes to next point
        if (dist <= reachDist)
            currentPoint++;

        //Goes back to initial point
        if (currentPoint >= path.Length)
            currentPoint = 0;
    }

    void OnDrawGizmos() {

        Gizmos.color = rayColor;

        for (int i = 0; i < path.Length; i++)
        {
            Vector3 pos = path[i].position;
            if (i > 0)
            {
                Vector3 prev = path[i - 1].position;
                Gizmos.DrawLine(prev, pos);
                //Gizmos.DrawWireSphere(pos, 0.3f); //Draws spheres in wires

                /*
                        if (counter < dist) {
                            counter += .1f / lineDrawSpeed;

                            float x = Mathf.Lerp (0, dist, counter);
                                              
                            Vector3 pointA = path[currentPoint].position;
                            Vector3 pointB = transform.position;
                
                            Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;
                            
                            lineRenderer.SetPosition (1, pointAlongLine);
                        }
            */
            }
        }

        if (path.Length > 0) { 
            for(int i = 0; i < path.Length; i++) {
                if(path[i] != null) {
                    Gizmos.DrawSphere(path[i].position, reachDist);
                //    Gizmos.DrawLine(path[i].position, transform.position); //Lines centered in the middle					
				}
            }
        }


    }
}
