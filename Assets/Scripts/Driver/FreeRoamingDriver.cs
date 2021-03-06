﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Language;
using System;
using System.Linq;
using System.Text;
using Assets.Scripts.Exhibition_Content;
using Assets.Scripts.Path;
using UnityEngine.UI;


public class FreeRoamingDriver : MonoBehaviour
{
    private FreeRoaming freeRoamingTour;

    private float minSwipeDistX = 300;
    private Vector2 startPos;

    private UI_Manager ui_Manager;
    public Animator anim;

    private Map map;
    private MapController mc;

    public GameObject iBeaconHandler;
    public GameObject shortestPathCreator;

    public GameObject nodePrefabPOI;
    public GameObject nodePrefabPOT;
    public Sprite[] nodeSprites;

    private GameObject floorManager;
    private readonly List<GameObject> gameObjectNodesList = new List<GameObject>();


    public Node[] arrayOfNodes;

    public Button[] cercles;

    private static bool changedFloor;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(startFreeRoaming());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(freeRoamingTour.searchForPoiBeacon(.05f));
        swipePanelLeft();
    }

    public Map getMap()
    {
        return map;
    }

    public Node[] getArrayOfNodes()
    {
        return arrayOfNodes;
    }

    public List<Node> getShortestPath()
    {
        List<Node> shortest_path = new List<Node>();

        //JOSEPH: When you touch a point of interest on the map, it shows the shortest path from the first node of the nodeList to the touched node.
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);

        if (hit.collider != null)
        {
            //if (touched)
            //shortest_path.Clear();

            //JOSEPH: erase the path renderer and recalculate which node you touched
            ResetTrails();
            GameObject recipient = hit.transform.gameObject;
            Node touchedNode = recipient.GetComponent<Node>();
            ShortestPathCreator.currentPoint = 0;
            Debug.Log("First node: " + map.getGraph().getVertices()[0].getID());
            Debug.Log("Touched node floor number: " + touchedNode.getFloorNumber());
            shortest_path = map.getGraph().shortest_path(map.getGraph().getVertices()[0], touchedNode);
            shortest_path.Reverse();

            foreach (var n in shortest_path)
            {
                print(n.getID());
            }

        }

        return shortest_path;
    }

    public void ResetTrails()
    {
        TrailRenderer trail = shortestPathCreator.GetComponent<TrailRenderer>();
        StartCoroutine("DisableTrail", trail);
        if (trail.time < 0)
            trail.time = -trail.time;
        shortestPathCreator.transform.position = new Vector3(arrayOfNodes[0].transform.position.x, arrayOfNodes[0].transform.position.y, -7);
    }

    IEnumerator DisableTrail(TrailRenderer trail)
    {
        if (trail.time < 0)
            yield break;

        yield return new WaitForSeconds(0.01f);

        trail.time = -trail.time;
    }

    public IEnumerator startFreeRoaming()
    {
        yield return new WaitForSeconds(0.005f);

        ui_Manager = FindObjectOfType<UI_Manager>();

        floorManager = GameObject.Find("FloorManager");

        freeRoamingTour = FindObjectOfType<FreeRoaming>();
        mc = FindObjectOfType<MapController>();

        map = mc.getMap();

        iBeaconHandler bh = iBeaconHandler.GetComponent<iBeaconHandler>();
        List<Beacon> beacons = bh.getBeacons();

        DisplayFloor(2);
        

        map.initializeLists(arrayOfNodes);

        freeRoamingTour.setBeaconList(beacons);

        freeRoamingTour.initializeLists(map.GetPoiNodes());


        shortestPathCreator.transform.position = new Vector3(arrayOfNodes[0].transform.position.x, arrayOfNodes[0].transform.position.y, -7);
        /*public IEnumerator startStoryline()
        {
            yield return new WaitForSeconds(0.005f);

            ui_Manager = FindObjectOfType<UI_Manager>();
            floorManager = GameObject.Find("FloorManager");

            //Storyline demo = new Storyline(0, 1, "Demo Storyline", "Let's see how good is your mobile app!");
            
            mc = FindObjectOfType<MapController>();

            map = mc.getMap();

            iBeaconHandler bh = iBeaconHandler.GetComponent<iBeaconHandler>();
            List<Beacon> beacons = bh.getBeacons();
            int slID = PlayerPrefs.GetInt("storylineID");

            print("Storyline id is: " + slID);
			map.GetStoryline(slID).setBeaconList(beacons);
            
            List<Node> orderedPath = map.orderedPath(slID);

            //map.setStorypointList(orderedPath);
            map.startStoryline(orderedPath, slID);
            DisplayFloor(2, slID); //this should be the first floor

			map.GetStoryline(slID).getStorypointList()[0].setBeacon(new iBeaconServer("B9407F30-F5F8-466E-AFF9-25556B57FE6D", 38714, 26839));
            
            shortestPathCreator.transform.position = new Vector3(gameObjectNodesList[0].transform.position.x, gameObjectNodesList[0].transform.position.y, -7);
            */

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


    /*
         * Convert width and height of x-coordinate to match the scale in unity
         */
    public float XCoordinatesConversion(float x, float mapWidth)
    {
        Renderer rend = floorManager.GetComponent<Renderer>();

        float xScaled = rend.bounds.max.x; //(2 * rend.bounds.max.x * mapWidth) / 2313f;
        float xConverted = x - mapWidth / 2;
        float xConvertedScaled = (xConverted * (2 * xScaled)) / mapWidth;
        return xConvertedScaled;
    }

    /*
     * Convert width and height of y-coordinate to match the scale in unity
     */
    public float YCoordinatesConversion(float y, float mapHeight)
    {
        Renderer rend = floorManager.GetComponent<Renderer>();

        float yScaled = rend.bounds.max.y; //(2 * rend.bounds.max.y * mapHeight) / 2328f;
        float yConverted = -(y) + mapHeight / 2;
        float yConvertedScaled = (yConverted * (2 * yScaled)) / mapHeight;
        return yConvertedScaled;
    }


    public void DisplayFloor(int floorId)
    {
        if (gameObjectNodesList.Count > 0)
        {
            foreach (var obj in gameObjectNodesList)
            {
                Destroy(obj);

            }
        }

        for (int i=0; i < cercles.Length; i++)
        {
            if (i == (floorId - 1))
            {
                cercles[i].image.color = new Color32(255, 0, 0, 94);
            }
            else
                cercles[i].image.color = new Color32(0, 0, 0, 0);
        }

        if (changedFloor && ShortestPathCreator.touched)
        {
            TrailRenderer trail = shortestPathCreator.GetComponent<TrailRenderer>();
            StartCoroutine("DisableTrail", trail);
            ShortestPathCreator.touched = false;
        }


        changedFloor = true;


        foreach (var f in map.getFloors())
        {
            if (f.floorNumber.Equals(floorId.ToString()))
            {
                var tex = Resources.Load(f.imagePath) as Texture2D;
                floorManager.GetComponent<RawImage>().texture = tex;
                if (tex != null)
                    floorManager.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                Camera.main.orthographicSize = floorManager.GetComponent<Renderer>().bounds.max.y;

                /* List<Node> floorNodes = new List<Node>();
                foreach (var floorNode in map.GetPoiNodes())
                {

                    if (floorNode.getFloorNumber() == int.Parse(f.floorNumber))
                    {
                        floorNodes.Add(floorNode);
                    }
                }
                */

                     DisplayNodes(floorId);
                     break;

            }
        }
            
            
        }

    public void DisplayNodes(/*List<Node> storyPointList, FloorPlan floorPlan*/ int floorId)
    {

        foreach (var n in arrayOfNodes)
        {
            n.gameObject.SetActive(n.floorNumber == floorId);
        }

        /*
        int blue = 0;
        int green = 1;
        int red = 2;
        int stairs = 3;
        int elevator = 4;

        foreach (Node n in storyPointList)
        {
            Sprite nodeSprite;
            string nodeColorEditor; // show name of color of the sprite in editor (optional)
            GameObject newNode;

            if (n.GetType() == typeof(PointOfInterest) || n.GetType() == typeof(POS)) //check if poi or pot at runtime type
            {
                float x = XCoordinatesConversion(n.x, floorPlan.getImageWidth());
                float y = YCoordinatesConversion(n.y, floorPlan.getImageHeight());
                newNode = GameObject.Instantiate(nodePrefabPOI, new Vector3(x, y, -7), Quaternion.identity) as GameObject; //not exist in this current object must add as


                if (newNode != null)
                {
                    newNode.transform.localScale = new Vector3(5f, 5f, 5f);
                    newNode.transform.parent = floorManager.transform;
                    newNode.SetActive(true);
                    nodeSprite = nodeSprites[blue];
                    nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                    newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                    newNode.GetComponent<Node>().x = (XCoordinatesConversion(n.x, floorPlan.getImageWidth()));
                    newNode.GetComponent<Node>().y = (YCoordinatesConversion(n.y, floorPlan.getImageHeight()));
                    newNode.GetComponent<Node>().id = (n.getID());
                    newNode.GetComponent<Node>().floorNumber = int.Parse(floorPlan.floorNumber);
                    newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                    gameObjectNodesList.Add(newNode);
                    //if id

                    //TODO: need to take specific type of node depending on their type
                    /*if (n.color.Equals("Blue"))
                    {
                        nodeSprite = nodeSprites[green];
                        nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                        newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                        newNode.GetComponent<Node>().x = XCoordinatesConversion(n.x, floorPlan.getImageWidth());
                        newNode.GetComponent<Node>().y = YCoordinatesConversion(n.y, floorPlan.getImageHeight());
                        newNode.GetComponent<Node>().id = n.getID();
                        newNode.GetComponent<Node>().floorNumber = int.Parse(floorPlan.floorNumber);
                        newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;

                    }
                }
            }
            else if (n.GetType() == typeof(PointOfTransition)) //check poi or pot at runtime type
            {
                float x = XCoordinatesConversion(n.x, floorPlan.getImageWidth());
                float y = YCoordinatesConversion(n.y, floorPlan.getImageHeight());
                newNode = GameObject.Instantiate(nodePrefabPOT, new Vector3(x, y, -7), Quaternion.identity) as GameObject; //not exist in this current object must add as

                if (newNode != null)
                {

                    PointOfTransition pot = (PointOfTransition)n;
                    newNode.transform.localScale = new Vector3(5f, 5f, 5f);
                    newNode.transform.parent = floorManager.transform;
                    newNode.SetActive(true);
                    newNode.GetComponent<Node>().x = (XCoordinatesConversion(n.x, floorPlan.getImageWidth()));
                    newNode.GetComponent<Node>().y = (YCoordinatesConversion(n.y, floorPlan.getImageHeight()));
                    newNode.GetComponent<Node>().id = (n.getID());
                    newNode.GetComponent<Node>().floorNumber = int.Parse(floorPlan.floorNumber);

                    nodeSprite = nodeSprites[green];
                    nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                    newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                    newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                    /*if (n.color.Equals("Green"))
                    {
                        nodeSprite = nodeSprites[blue];
                        nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                        newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                        newNode.GetComponent<Node>().x = (XCoordinatesConversion(n.x, floorPlan.getImageWidth()));
                        newNode.GetComponent<Node>().y = (YCoordinatesConversion(n.y, floorPlan.getImageHeight()));
                        newNode.GetComponent<Node>().id = (n.getID());
                        newNode.GetComponent<Node>().floorNumber = int.Parse(floorPlan.floorNumber);
                        newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                    }

                    //Added foreach loop 

                    if (pot.label == PointOfTransition.Label.STAIRS) //stairs
                    {
                        nodeSprite = nodeSprites[stairs];
                        nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                        newNode.name = nodeColorEditor; //print color name for specific sprite (optional)

                        newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                        gameObjectNodesList.Add(newNode);
                    }

                    else if (pot.label == PointOfTransition.Label.ELEVATOR) //elevator
                    {
                        nodeSprite = nodeSprites[elevator];
                        nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                        newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                        newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                        gameObjectNodesList.Add(newNode);
                    }
                    else //none=green
                    {
                        nodeSprite = nodeSprites[green];
                        nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                        newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                        newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                        gameObjectNodesList.Add(newNode);
                    }
                }
            }

        }
        */

    } 
} 
    

