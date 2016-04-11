// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class TouchCamera : MonoBehaviour
{


    public Transform target;
    public float distance = 10.0f;
    public float xSpeed = 60.0f;
    public float ySpeed = 60.0f;
    public float yMinLimit = 10f;
    public float yMaxLimit = 60f;
    public float distanceMin = 10f;
    public float distanceMax = 20f;
    float x = 0.0f;
    float y = 0.0f;
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

    }

    void Update()

    {

        if (target && Input.touchCount == 1 && Input.GetTouch(0).position.x > Screen.width / 2 && Input.GetTouch(0).phase == TouchPhase.Moved) //Just orbit touch without movement

        {

            Debug.Log("Orbiting! 1 touch");


            Orbit(Input.GetTouch(0));

        }


        else if (Input.touchCount == 2)

        {


            if (Input.GetTouch(0).position.x > Screen.width / 2 && Input.GetTouch(0).phase == TouchPhase.Moved)


                Orbit(Input.GetTouch(0)); //Movement was touched second


            else if (Input.GetTouch(1).position.x > Screen.width / 2 && Input.GetTouch(1).phase == TouchPhase.Moved)


                Orbit(Input.GetTouch(1)); //Movement was touched first



        }

    }



    /**
    * Method:    Orbit
    * FullName:  Orbit
    * Access:    public
    * Qualifier:
    * @param   Touch touch
    * @return   void
*/

    void Orbit(Touch touch)



    {

        // rotates around the building

      
        x += touch.deltaPosition.x * xSpeed * 0.02f /* * distance*/;
            
        // rotates up and down (roof top)

        y -= touch.deltaPosition.y * ySpeed * 0.02f /* * distance*/;



        y = ClampAngle(y, yMinLimit, yMaxLimit);


        Quaternion rotation = Quaternion.Euler(y, x, 0);


        RaycastHit hit;



        if (Physics.Linecast(target.position, transform.position, out hit))



        {


              // distance -= hit.distance;



        }



        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);



        Vector3 position = rotation * negDistance + target.position;



        transform.rotation = rotation;



        transform.position = position;



    }


    /**
    * Method:    ClampAngle
    * FullName:  ClampAngle
    * Access:    public
    * Qualifier:
    * @param    float angle, min, max
    * @return   float
*/
    public static float ClampAngle(float angle, float min, float max)


    {


        if (angle < -360F)



            angle += 360F;



        if (angle > 360F)



            angle -= 360F;



        return Mathf.Clamp(angle, min, max);



    }

}
