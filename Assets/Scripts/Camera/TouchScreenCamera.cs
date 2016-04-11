using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class TouchScreenCamera : MonoBehaviour
{
    public float moveSensitivityX = 1.0f;
    public float moveSensitivityY = 1.0f;
    public bool updateZoomSensitivity = true; //place finger on object
    public float orthoZoomSpeed = 0.05f; //how fast when we zooming
    public float minZoom = 1.0f; //limit for min zooming
    public float maxZoom = 5.0f; //limit for max zooming
    public bool invertMoveX = false;
    public bool invertMoveY = false;
    public float mapWidth = 30.0f;
    public float mapHeight = 20.0f;

    public float inertiaDuration = 1.0f;

    private Camera myCamera;

    private float minX, maxX, minY, maxY;
    private float horizontalExtent, verticalExtent;

    private float scrollVelocity = 0.0f;
    private float timeTouchPhaseEnded;
    private Vector2 scrollDirection = Vector2.zero;

    private GameObject fp;
    private bool cameraSizeChanged;

    void Start()
    {
        fp = GameObject.Find("FloorManager");
        myCamera = Camera.main;
        maxZoom = 0.5f * (mapWidth / myCamera.aspect);

        if (mapWidth > mapHeight)
            maxZoom = 0.5f * mapHeight;

        if (myCamera.orthographicSize > maxZoom)
            myCamera.orthographicSize = maxZoom;

        CalculateLevelBounds();
        
    }

    public IEnumerator getMapWidthAndHeight()
    {
        yield return new WaitForSeconds(0.06f);

        Renderer floorRenderer = fp.GetComponent<Renderer>();
        mapWidth = floorRenderer.bounds.max.x * 2;
        mapHeight = floorRenderer.bounds.max.y * 2;
    }

    void Update()
    {
        StartCoroutine(getMapWidthAndHeight());
        

        if (updateZoomSensitivity)
        {
            moveSensitivityX = myCamera.orthographicSize / 5.0f;
            moveSensitivityY = myCamera.orthographicSize / 5.0f;
        }

        Touch[] touches = Input.touches; //return all the touches currently on screen

        //SPEED EFFECT
        /*      if (touches.Length < 1)  {
                  //if the camera is currently scrolling (before zoom-in/zoom-out)
                  if (scrollVelocity != 0.0f)  {
                      //slow down over time
                      float t = (Time.time - timeTouchPhaseEnded) / inertiaDuration;
                      float frameVelocity = Mathf.Lerp(scrollVelocity, 0.0f, t);
                      myCamera.transform.position += -(Vector3)scrollDirection.normalized * (frameVelocity * 0.05f) * Time.deltaTime;

                      if (t >= 1.0f)
                          scrollVelocity = 0.0f;
                  }
              }
        */
        if (touches.Length > 0)
        {

            //Single touch (move)
            //One finger to move left-right-up-down
            if (touches.Length == 1)
            {
                if (touches[0].phase == TouchPhase.Began)
                {
                    scrollVelocity = 0.0f;
                }
                else if (touches[0].phase == TouchPhase.Moved)
                {
                    Vector2 delta = touches[0].deltaPosition;

                    float positionX = delta.x * moveSensitivityX * Time.deltaTime;
                    positionX = invertMoveX ? positionX : positionX * -1;

                    float positionY = delta.y * moveSensitivityY * Time.deltaTime;
                    positionY = invertMoveY ? positionY : positionY * -1;

                    myCamera.transform.position += new Vector3(positionX, positionY, 0);

                    scrollDirection = touches[0].deltaPosition.normalized;
                    scrollVelocity = touches[0].deltaPosition.magnitude / touches[0].deltaTime;


                    if (scrollVelocity <= 100)
                        scrollVelocity = 0;
                }
                else if (touches[0].phase == TouchPhase.Ended)
                {
                    timeTouchPhaseEnded = Time.time;
                }
            }


            //Double touch (zoom)
            //Two fingers to zoom-in-zoom-out
            if (touches.Length == 2)
            {
                Vector2 cameraViewsize = new Vector2(myCamera.pixelWidth, myCamera.pixelHeight);

                Touch touchOne = touches[0];
                Touch touchTwo = touches[1];

                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

                float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
                float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;

                float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

                myCamera.transform.position += myCamera.transform.TransformDirection((touchOnePrevPos + touchTwoPrevPos - cameraViewsize) * myCamera.orthographicSize / cameraViewsize.y);

                myCamera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
                myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, minZoom, maxZoom) - 0.001f;

                myCamera.transform.position -= myCamera.transform.TransformDirection((touchOne.position + touchTwo.position - cameraViewsize) * myCamera.orthographicSize / cameraViewsize.y);

                CalculateLevelBounds();
            }
        }
    }

    public void CalculateLevelBounds()
    {
        verticalExtent = myCamera.orthographicSize;
        horizontalExtent = myCamera.orthographicSize * Screen.width / Screen.height;
        minX = horizontalExtent - mapWidth / 2.0f;
        maxX = mapWidth / 2.0f - horizontalExtent;
        minY = verticalExtent - mapHeight / 2.0f;
        maxY = mapHeight / 2.0f - verticalExtent;
    }

    void LateUpdate()
    {
        Vector3 limitedCameraPosition = myCamera.transform.position;
        limitedCameraPosition.x = Mathf.Clamp(limitedCameraPosition.x, minX, maxX);
        limitedCameraPosition.y = Mathf.Clamp(limitedCameraPosition.y, minY, maxY);
        myCamera.transform.position = limitedCameraPosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(mapWidth, mapHeight, 0));
    }
}
