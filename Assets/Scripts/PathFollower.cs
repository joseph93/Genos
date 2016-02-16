using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour {

    public Transform[] path;
    public float speed = 5.0f;
    public float reachDist = 1.0f;
    public int currentPoint = 0;

	
	private LineRenderer lineRenderer;
	private float counter;
	private float dist;

	public Transform origin;
	public Transform destination;
	public Transform middle; //cube


	public float lineDrawSpeed = 6f;
	
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		
	//	lineRenderer.SetPosition (0, origin.position);
		lineRenderer.SetPosition (0, path[currentPoint].position);
		
	
		lineRenderer.SetWidth (.45f, .45f);

	//	dist = Vector3.Distance (origin.position, destination.position);
		dist = Vector3.Distance (path[currentPoint].position, transform.position);
		
		
	}
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(path[currentPoint].position, transform.position);


        transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, Time.deltaTime*speed);

        // slows down the object when it reaches the point
        // transform.position = Vector3.Lerp(transform.position, path[currentPoint].position, Time.deltaTime * speed);

        //Goes to next point
        if (dist <= reachDist)
            currentPoint++;

        //Goes back to initial point
        if (currentPoint >= path.Length)
            currentPoint = 0;


    }

    void OnDrawGizmos() {
        if(path.Length > 0) { 
            for(int i = 0; i < path.Length; i++) {
                if(path[i] != null) {
                    Gizmos.DrawSphere(path[i].position, reachDist);
					
					
					if (counter < dist) {
						counter += .1f / lineDrawSpeed;

						float x = Mathf.Lerp (0, dist, counter);

					//	Vector3 pointA = origin.position;
					//	Vector3 pointB = destination.position;
						Vector3 pointA = path[currentPoint].position;
						Vector3 pointB = transform.position;
					
					
						Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;


						lineRenderer.SetPosition (1, pointAlongLine);


					}
					
					
				}
            }
        }
    }



}
