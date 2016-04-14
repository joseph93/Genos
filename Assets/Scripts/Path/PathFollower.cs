using UnityEngine;
using System.Collections;

using Assets.Scripts.Driver;

using Assets.Scripts;
using System.Collections.Generic;

public class PathFollower : MonoBehaviour {

   // public Transform[] path;
    public float speed = 5.0f;
    public float reachDist = 0.2f;
    public int currentPoint;
    public int sizeOfPath = Length;

    private StorylineDriver sd;
    private List<Node> path;
    public static int Length { get; private set; }
    

    // Use this for initialization
    void Start () {
        StartCoroutine(getPath());

    }

	// Update is called once per frame
	void Update () {

        if (path!=null)
        {
            if (currentPoint < path.Count)
            {
                float dist = Vector3.Distance(new Vector3(sd.XCoordinatesConversion(path[currentPoint].x, sd.getMap().getFloors()[0].getImageWidth()), sd.YCoordinatesConversion(path[currentPoint].y, sd.getMap().getFloors()[0].getImageHeight()), -8), transform.position);

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(sd.XCoordinatesConversion(path[currentPoint].x, sd.getMap().getFloors()[0].getImageWidth()), sd.YCoordinatesConversion(path[currentPoint].y, sd.getMap().getFloors()[0].getImageHeight()), -8), Time.deltaTime * speed);

                //Nipper goes to next point
                if (dist <= reachDist)
                    currentPoint++;
            }
        } 
    }
    
    public IEnumerator getPath()
    {
        yield return new WaitForSeconds(1.0f);

        sd = FindObjectOfType<StorylineDriver>();

        path = sd.getMap().orderedPath(sd.getMap().getStorypointNodes(), 0);

		this.transform.GetChild (0).transform.GetComponent<SpriteRenderer> ().enabled = true;
        transform.position = new Vector3(sd.XCoordinatesConversion(path[0].x, sd.getMap().getFloors()[0].getImageWidth()), sd.YCoordinatesConversion(path[0].y, sd.getMap().getFloors()[0].getImageHeight()), -8);
        
    }

    
    /*void OnDrawGizmos() {

        if (path != null)
        {


            //Draw gizmos lines
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 pos = path[i].gameObject.transform.position; //current point PointB
                if (i > 0)
                {
                    Vector3 prev = path[i - 1].gameObject.transform.position; //previous point PointA
                    Gizmos.DrawLine(prev, pos);
                }
            }

            //Draw gizmos spheres
            if (path.Count > 0)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    if (path[i] != null)
                    {
                        float radius = 0.2f;
                        Gizmos.DrawWireSphere(path[i].gameObject.transform.position, radius);
                        //Gizmos.DrawSphere(path[i].position, reachDist);				
                    }
                }
            }
        }
        

    }*/ 
}
