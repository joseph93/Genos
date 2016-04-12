using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Language;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Driver
{
    public class StorylineDriver : MonoBehaviour
    {
        public GameObject iBeaconHandler;

        private float minSwipeDistX = 300;
        private Vector2 startPos;

        private UI_Manager ui_Manager;
        public Animator anim;

        private Map map;
        private MapController mc;

        public GameObject nodePrefabPOI;
        public GameObject nodePrefabPOT;
        public Sprite[] nodeSprites;

        private GameObject floorManager;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(startStoryline());
        }

        // Update is called once per frame
        void Update()
        {
            //StartCoroutine(map.GetStorylines()[0].searchForStorypointBeacon(1.00f));
            swipePanelLeft();
        }

        public Map getMap()
        {
            return map;
        }

        public IEnumerator startStoryline()
        {
            yield return new WaitForSeconds(0.005f);

            ui_Manager = FindObjectOfType<UI_Manager>();
            floorManager = GameObject.Find("FloorManager");

            //Storyline demo = new Storyline(0, 1, "Demo Storyline", "Let's see how good is your mobile app!");
            
            mc = FindObjectOfType<MapController>();

            map = mc.getMap();

            iBeaconHandler bh = iBeaconHandler.GetComponent<iBeaconHandler>();
            List<Beacon> beacons = bh.getBeacons();
            
            //List<Node> orderedPath = map.orderedPath(0);
            
            //map.setStorypointList(orderedPath);
            map.startStoryline(0 /*PlayerPrefs.GetInt("storylineID")*/);
            DisplayFloor(2, 0); //this should be the first floor
            

        }

        public void DisplayFloor(int floorId, int storylineId)
        {
            foreach (var f in map.getFloors())
            {
                if (f.floorNumber.Equals(floorId.ToString()))
                {
                    var tex = Resources.Load(f.imagePath) as Texture2D;
                    floorManager.GetComponent<RawImage>().texture = tex;
                    if (tex != null)
                        floorManager.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                    Camera.main.orthographicSize = floorManager.GetComponent<Renderer>().bounds.max.y;
                    List<Node> floorNodes = new List<Node>();
                    foreach (var floorNode in map.getStorypointNodes())
                    {
                        if (floorNode.GetType() == typeof (POS))
                        {
                            if (floorNode.getFloorNumber() == int.Parse(f.floorNumber))
                            {
                                POS sp = (POS) floorNode;
                                if (sp.storylineID == storylineId)
                                {
                                    floorNodes.Add(sp);
                                }
                            }
                        }
                        else
                        {
                            if (floorNode.getFloorNumber() == int.Parse(f.floorNumber))
                            {
                                floorNodes.Add(floorNode);
                            }
                        }
                        
                    }
                    DisplayNodes(floorNodes, f);
                    break;
                }
            }


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


        public void DisplayNodes(List<Node> storyPointList, FloorPlan floorPlan)
        {
            int blue = 0;
            int green = 1;
            int red = 2;
            
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
                        newNode.transform.localScale = new Vector3(7f, 7f, 7f);
                        newNode.transform.parent = floorManager.transform;
                        newNode.SetActive(true);
                        nodeSprite = nodeSprites[red];
                        nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                        newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                        newNode.GetComponent<Node>().x = (XCoordinatesConversion(n.x, floorPlan.getImageWidth()));
                        newNode.GetComponent<Node>().y = (YCoordinatesConversion(n.y, floorPlan.getImageHeight()));
                        newNode.GetComponent<Node>().id = (n.getID());
                        newNode.GetComponent<Node>().floorNumber = int.Parse(floorPlan.floorNumber);
                        newNode.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                    
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

                        }*/
                            
                        
                    }
                }
                else if (n.GetType() == typeof(PointOfTransition)) //check poi or pot at runtime type
                {
                    float x = XCoordinatesConversion(n.x, floorPlan.getImageWidth());
                    float y = YCoordinatesConversion(n.y, floorPlan.getImageHeight());
                    newNode = GameObject.Instantiate(nodePrefabPOT, new Vector3(x, y, -7), Quaternion.identity) as GameObject; //not exist in this current object must add as

                    if (newNode != null)
                    {
                        newNode.transform.localScale = new Vector3(7f, 7f, 7f);
                        newNode.transform.parent = floorManager.transform;
                        newNode.SetActive(true);
                        nodeSprite = nodeSprites[red];
                        nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                        newNode.name = nodeColorEditor; //print color name for specific sprite (optional)
                        newNode.GetComponent<Node>().x = (XCoordinatesConversion(n.x, floorPlan.getImageWidth()));
                        newNode.GetComponent<Node>().y = (YCoordinatesConversion(n.y, floorPlan.getImageHeight()));
                        newNode.GetComponent<Node>().id = (n.getID());
                        newNode.GetComponent<Node>().floorNumber = int.Parse(floorPlan.floorNumber);
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
                        }*/
                            
                        
                    }
                }


            } //foreach
        }

        
       
    }
}
