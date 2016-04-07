using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Language;

public class FreeRoamingDriver : MonoBehaviour {

    public Node[] ArrayOfNodes;
    private FreeRoaming freeRoamingTour;

    private float minSwipeDistX = 300;
    private Vector2 startPos;

    private UI_Manager ui_Manager;
    public Animator anim;

    private Map map;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(startFreeRoaming());
    }
	
	// Update is called once per frame
	void Update () {
	    swipePanelLeft();
	}

    public Map getMap()
    {
        return map;
    }

    public IEnumerator startFreeRoaming()
    {
        yield return new WaitForSeconds(0.05f);

        ui_Manager = FindObjectOfType<UI_Manager>();
        freeRoamingTour = FindObjectOfType<FreeRoaming>();

        map = new Map();
        freeRoamingTour.initializeLists(ArrayOfNodes);

        map.addNodeList(freeRoamingTour.getNodeList());
        map.initializeGraph();  //adding the list of nodes in the graph
        

    }

    public void swipePanelLeft()
    {
        if (Input.touchCount > 0)

        {
            Touch touch = Input.touches[0];

            switch (touch.phase)

            {
                case TouchPhase.Began:

                    startPos = touch.position;

                    break;


                case TouchPhase.Ended:

                    float swipeDistHorizontal =
                        (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                    if (swipeDistHorizontal > minSwipeDistX)

                    {
                        float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                        if (swipeValue < 0) //left swipe
                        {
                            ui_Manager.disableBoolAnimator(anim);
                        }

                        //MoveLeft ();
                    }
                    break;
            }
        }
    }
}
