using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Language;
using UnityEngine;

namespace Assets.Scripts.Driver
{
    public class StorylineDriver : MonoBehaviour
    {
        public Node[] ArrayOfNodes;
        public GameObject iBeaconHandler;

        private float minSwipeDistX = 300;
        private Vector2 startPos;

        private UI_Manager ui_Manager;
        public Animator anim;

        private Map map;
        private MapController mc;

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
            yield return new WaitForSeconds(0.05f);

            ui_Manager = FindObjectOfType<UI_Manager>();

            //Storyline demo = new Storyline(0, 1, "Demo Storyline", "Let's see how good is your mobile app!");
            
            mc = FindObjectOfType<MapController>();

            map = mc.getMap();
            
            iBeaconHandler bh = iBeaconHandler.GetComponent<iBeaconHandler>();
            List<Beacon> beacons = bh.getBeacons();
            map.startStoryline(PlayerPrefs.GetInt("storylineID"));
            //demo.setBeaconList(beacons);

            
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
}
