using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using Assets.Scripts.Exhibition_Content;
using Assets.Scripts.Language;
using Assets.Scripts.Observer_Pattern;
using Assets.Scripts.Path;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace Assets.Scripts.Driver
{
    public class StorylineDriver : MonoBehaviour
    {
        private float minSwipeDistX = 300;
        private Vector2 startPos;
        private UI_Manager ui_Manager;
        public Animator anim;
        private Map map;
        private MapController mc;

        public GameObject iBeaconHandler;
        
        public PointOfTransition nodePrefabPOT;
        public POS POSprefab;
        public Sprite[] nodeSprites;

        private GameObject floorManager;
        private readonly List<Node> instantiatedNodes = new List<Node>(); 

        public GameObject shortestPathCreator;

        private static bool changedFloor;

        public Node[] arrayOfNodes;

        //Added to display buttonTitle, panelTitle, and panelDescription
        private SummaryWindow summaryWindow;
        private UnityAction myCloseAction;
        UnityEngine.UI.Text yourButtonText;
        //Testing
        private string buttonTitle="buttontitle";
        private string panelTitle = "paneltitle";
        private string panelDescription = "paneldescription";
        //public Button b;

        public Button[] cercles;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(startStoryline());
            //Added
            // assignContent(poiList);
            //displayButtonTitle();
        }

        // Update is called once per frame
        void Update()
        {
            StartCoroutine(map.GetStoryline(PlayerPrefs.GetInt("storylineID")).searchForStorypointBeacon(0.05f));
            swipePanelLeft();
            
        }

        public Map getMap()
        {
            return map;
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
            shortestPathCreator.transform.position = new Vector3(instantiatedNodes[0].transform.position.x, instantiatedNodes[0].transform.position.y, -7);
        }

        IEnumerator DisableTrail(TrailRenderer trail)
        {
            if (trail.time < 0)
                yield break;

            yield return new WaitForSeconds(0.01f);

            trail.time = -trail.time;
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
            int slID = PlayerPrefs.GetInt("storylineID");


            DisplayFloor(2); //this should be the first floor

            map.initializeLists(arrayOfNodes);
            
            print("Storyline id is: " + slID);
			map.GetStoryline(slID).setBeaconList(beacons);
            
            map.startStoryline(map.getStorypointNodes(), slID);

            foreach (var sp in map.GetStoryline(slID).getStorypointList())
            {
                print("Storypoint " + sp.id + " and is in order " + map.GetStoryline(slID).isInOrder(sp));
            }

            /*List<Node> orderedPath = map.orderedPath(instantiatedNodes, slID);

            //map.setStorypointList(orderedPath);

            map.startStoryline(orderedPath, slID);
            
			map.GetStoryline(slID).getStorypointList()[0].setBeacon(new iBeaconServer("B9407F30-F5F8-466E-AFF9-25556B57FE6D", 38714, 26839));

            POS sp = map.GetStoryline(slID).getStorypointList()[0];

            sp.displayStorylinePopUpWindow();

            /*print(map.GetStoryline(slID).getStorypointList().Count);
            foreach (var sp in map.GetStoryline(slID).getStorypointList())
            {
                print(sp.storylineID);
            }
            
            /*
            foreach (var d in sp.GetPoiDescriptionList())
            {
                print("Title: " + d.title + ", language: " + d.language + ", description: " + d.summary);
            }*/

            //map.GetStoryline(slID).getStorypointList()[0].setBeacon(new iBeaconServer("B9407F30-F5F8-466E-AFF9-25556B57FE6D", 38714, 26839));

            //shortestPathCreator.transform.position = new Vector3(instantiatedNodes[0].transform.position.x, instantiatedNodes[0].transform.position.y, -7);

        }

        public List<Node> GetNodeGameObjects()
        {
            return instantiatedNodes;
        } 

        public void DisplayFloorPlan(int floorId)
        {
            DisplayFloor(floorId);
        }

        public void DisplayFloor(int floorId)
        {
            if (instantiatedNodes.Count > 0)
            {
                foreach (var obj in instantiatedNodes)
                {
                    Destroy(obj);
                }
            }

            for (int i = 0; i < cercles.Length; i++)
            {
                cercles[i].image.color = i == (floorId - 1) ? new Color32(255, 0, 0, 94) : new Color32(0, 0, 0, 0);
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
                    /*List<Node> floorNodes = new List<Node>();
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
                        
                    }*/
                    DisplayNodes(floorId);
                  
                    //Added
                    //DisplayButtons(map.GetStorylines()[0].getStorypointList());
                    //Added
                    Debug.Log("Will go in assignContent()");
                    //assignContent(floorNodes); //get from map the list of storypoint / poi
                    //List<Node> poiList
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


        public void DisplayNodes(int floorId)
        {
            foreach (var n in arrayOfNodes)
            {
                n.gameObject.SetActive(n.floorNumber == floorId);
            }


           /* int blue = 0;
            int green = 1;
            int red = 2;
            int stairs = 3;
            int elevator = 4;
            
            foreach (Node n in storyPointList)
            {
                Sprite nodeSprite;
                string nodeColorEditor; // show name of color of the sprite in editor (optional)
               
                if (n.GetType() == typeof(POS)) //check if poi or pot at runtime type
                {
                    POS pos = (POS) n;
                    float x = XCoordinatesConversion(n.x, floorPlan.getImageWidth());
                    float y = YCoordinatesConversion(n.y, floorPlan.getImageHeight());
                    //newNode = GameObject.Instantiate(nodePrefabPOI, new Vector3(x, y, -7), Quaternion.identity) as GameObject; //not exist in this current object must add as
                    pos = (POS)Instantiate(POSprefab, new Vector3(x, y, -7), Quaternion.identity);

                    if (pos != null)
                    {
                        pos.transform.localScale = new Vector3(5f, 5f, 5f);
                        pos.transform.parent = floorManager.transform;
                        pos.gameObject.SetActive(true);
                        nodeSprite = nodeSprites[blue];
                        nodeColorEditor = nodeSprite.name; //get sprite color name (optional)

                        pos.name = nodeColorEditor; //print color name for specific sprite (optional)
                        /*pos.GetComponent<Node>().x = (XCoordinatesConversion(n.x, floorPlan.getImageWidth()));
                        pos.GetComponent<Node>().y = (YCoordinatesConversion(n.y, floorPlan.getImageHeight()));
                        pos.GetComponent<Node>().id = (n.getID());
                        pos.GetComponent<Node>().floorNumber = int.Parse(floorPlan.floorNumber);
                        pos.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                        POS sp = (POS) n;
                        pos.id = sp.id;
                        pos.x = sp.x;
                        pos.y = sp.y;
                        pos.floorNumber = sp.floorNumber;
                        pos.storylineID = sp.storylineID;
                        pos.setContents(sp.getContents());
                        pos.setDescriptionList(sp.GetPoiDescriptionList());
                        pos.setBeacon(sp.getBeacon());
                        pos.setPopUpInstance();

                        instantiatedNodes.Add(pos);
                        print(pos.storylineID);

                       /* //Added
                        if(storyPointList.Count >= 3) { 
                            b.enabled = false;
                            b.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
                            b.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.clear;
                        }
                        
                    }
                }
                else if (n.GetType() == typeof(PointOfTransition)) //check poi or pot at runtime type
                {
                    PointOfTransition pot = (PointOfTransition) n;
                    float x = XCoordinatesConversion(n.x, floorPlan.getImageWidth());
                    float y = YCoordinatesConversion(n.y, floorPlan.getImageHeight());
                    pot = (PointOfTransition)Instantiate(nodePrefabPOT, new Vector3(x, y, -7), Quaternion.identity); //not exist in this current object must add as
                    
                    if (pot != null)
                    {
                        PointOfTransition pointOfTransition = (PointOfTransition) n;
                        pot.transform.localScale = new Vector3(5f, 5f, 5f);
                        pot.transform.parent = floorManager.transform;
                        pot.gameObject.SetActive(true);
                        pot.id = pointOfTransition.id;
                        pot.x = pointOfTransition.x;
                        pot.y = pointOfTransition.y;
                        pot.label = pointOfTransition.label;
                        pot.floorNumber = pointOfTransition.floorNumber;

                            if (pot.label == PointOfTransition.Label.STAIRS) //stairs
                            {
                                nodeSprite = nodeSprites[stairs];
                                nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                                pot.name = nodeColorEditor; //print color name for specific sprite (optional)

                                pot.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                                instantiatedNodes.Add(pot);
                            }

                            else if (pot.label == PointOfTransition.Label.ELEVATOR) //elevator
                            {
                                nodeSprite = nodeSprites[elevator];
                                nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                                pot.name = nodeColorEditor; //print color name for specific sprite (optional)
                                pot.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                                instantiatedNodes.Add(pot);
                            }
                            else //none=green
                            {
                                nodeSprite = nodeSprites[green];
                                nodeColorEditor = nodeSprite.name; //get sprite color name (optional)
                                pot.name = nodeColorEditor; //print color name for specific sprite (optional)
                                pot.GetComponent<SpriteRenderer>().sprite = nodeSprite;
                                instantiatedNodes.Add(pot);
                            }
                        }
                    }

                }//foreach

            */
            } //end of display nodes
        
        /*
     * Assign the parsed titleButton, titlePanel, and descriptionPanel from the list of storyline/pos/poi
     */
        public void assignContent(List<Node> poiList)
        {
            Debug.Log("assignContent() has been started.");
            //Check inside specific storyline for each existing point of interest
            foreach (var n in poiList)
            {
                Debug.Log("foreach entered");
                //Check if poi at runtime
                if (n.GetType() == typeof(PointOfInterest) || n.GetType() == typeof(POS))
                {
                    Debug.Log("if getType entered");


                    POS pos = (POS)n;
                    string lg = PlayerPrefs.GetString("language");

                    Language.Language lang = Description.convertStringToLang(lg);

                    foreach (var d in pos.GetPoiDescriptionList())
                    {
                        if (lang == d.language)
                        {
                            
                            break;
                        }
                    }

                    //this.buttonTitle = n.GetComponent<Description>().getTitle(); //TODO
                    Debug.Log("buttonTitle has been entered");

                    //this.panelTitle = n.GetComponent<Description>().getTitle();
                    Debug.Log("panelTitle has been entered");

                    //this.panelDescription = n.GetComponent<Description>().getDescription();
                    Debug.Log("panelDescription has been entered");

                    Debug.Log("finish assigning");
                    displayButtonTitle();
                    Debug.Log("displayButtonTitle() has been reached.");
                }
            }


        }

        /*
         * Display the title of the button
         */
        public void displayButtonTitle()
        {
            Debug.Log("displayButtonTitle has been started.");
            yourButtonText = transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
            yourButtonText.text = buttonTitle;

            Debug.Log("buttonTitle has been set.");
            summaryWindow = SummaryWindow.Instance();
            myCloseAction = new UnityAction(summaryWindow.closePanel);
            Debug.Log("displayButtonTitle has been finished.");
        }

        /*
         * Display the title and the description of the summary panel
         */
        public void displayPanelSummary(int id)
        {
            Debug.Log("displayPanelSummary has been started.");

            List<POS> storypoints = map.GetStoryline(PlayerPrefs.GetInt("storylineID")).getStorypointList();
            string lg = PlayerPrefs.GetString("language");

            Language.Language lang = Description.convertStringToLang(lg);

            foreach (var sp in storypoints)
            {
                if(sp.id == id)
                {
                    foreach(var d in sp.GetPoiDescriptionList())
                    {
                        if(d.language == lang)
                        {
                            summaryWindow.SummaryOneButton(d.title, d.summary, myCloseAction);
                        }
                    }
                }
            }
            
            //summaryWindow.SummaryNoImage(panelTitle, panelDescription, myCloseAction);
            //summaryWindow.SummaryOneImage(titlePanel, descriptionPanel, image1, myCloseAction);
            Debug.Log("displayPanelSummary has been finished.");
        }

    } 
} 
  

