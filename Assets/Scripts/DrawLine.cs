using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

	private LineRenderer lineRenderer;
	private float counter;
	private float dist;
    //private int index = 0;

	//public Transform origin;
	//public Transform destination;
	//public Transform middle; //cube



    public Transform[] path;
    public float speed = 5.0f;


	public float lineDrawSpeed = 6f;

	// Use this for initialization
	void Start () {
	
		//lineRenderer = GetComponent<LineRenderer> ();
		//lineRenderer.SetPosition (0, origin.position);
        //lineRenderer.SetPosition(0, path[0].position);
        //lineRenderer.SetWidth (.45f, .45f);

        //dist = Vector3.Distance (origin.position, destination.position);
         for (int i = 0; i < path.Length; i++)
         {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, path[0].position);
            lineRenderer.SetWidth(.45f, .45f);
            //dist = Vector3.Distance ( path[0].position, path[path.Length-1].position);
            dist = Vector3.Distance(path[i].position,transform.position);
           // transform.position = Vector3.MoveTowards(transform.position, path[i].position, Time.deltaTime * speed);
        }



    }
	
	// Update is called once per frame
	void Update () {
	
		if (counter < dist) {
			counter += .1f / lineDrawSpeed;
            /*
			float x = Mathf.Lerp (0, dist, counter);

			Vector3 pointA = origin.position;
			Vector3 pointB = destination.position;
			Vector3 pointC = middle.position;

			Vector3 pointAlongLine = x * Vector3.Normalize (pointB - pointA) + pointA;


			lineRenderer.SetPosition (1, pointAlongLine);
            */


           

            for (int i = 0; i < path.Length - 1; i++)
            {
                

                if (path[i] != null)
                {

                    transform.position = Vector3.MoveTowards(transform.position, path[i].position, Time.deltaTime * speed);

                    float x = Mathf.Lerp(0, dist, counter);

                   // Vector3 pointAlongLine = x * Vector3.Normalize(path[i+1].position - path[i].position) + path[i].position;
                    Vector3 pointAlongLine = x * Vector3.Normalize(path[i + 1].position - path[i].position) + path[i].position;


                    lineRenderer.SetPosition(1, pointAlongLine);
                    //lineRenderer.SetPosition(1, transform.position);


                }
            }





            //Vector3[] positionsV3; 
            //positionsV3 = { Vector3 p};
            //lineRenderer.SetPositions(positionsV3);

            //Vector3 pointAlongLine = x * Vector3.Normalize (pointC - pointB) + pointB;

            //lineRenderer.SetPosition (1, pointAlongLine);
        }
	}
}
