using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Observer_Pattern;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class PointOfInterest : Node
    {
        private string description;
        public string poiName;

        public iBeaconServer beacon { get; set; }
        public GameObject BeaconGameObject;
        private AudioSource[] sounds;
        private AudioSource popUp;
        private AudioSource beforeSound;

        private bool visited;
        private bool detected;

        private List<Observer> observers;
        
        private PopUpWindow popUpWindow;
        private UnityAction myViewAction;

        private SummaryWindow summaryWindow;
        private UnityAction myCloseAction;

        public Sprite nipperPopUp;
        public Sprite poiImage;

        void Awake()
        {
            beacon = BeaconGameObject.GetComponent<iBeaconServer>();
            sounds = GetComponents<AudioSource>();
            popUp = sounds[0];
            beforeSound = sounds[1];
            observers = new List<Observer>();
            visited = false;
            detected = false;

            popUpWindow = PopUpWindow.Instance();
            summaryWindow = SummaryWindow.Instance();

            myViewAction = new UnityAction(displaySummary);
            myCloseAction = new UnityAction(popUpWindow.closePanel);
        }
        

        public PointOfInterest(int id, int x, int y, int floorNumber, string poiName) : base(id, x, y, floorNumber)
        {
            this.poiName = poiName;
            description = "";
            visited = false;
            detected = false;
        }

        public void setDescription(string descr)
        {
            description = descr;
        }

        public void setVisited(bool visited)
        {
            this.visited = visited;
            notify();
        }

        public bool isVisited()
        {
            return visited;
        }

        public void changeIconScale()
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }

        public void displayPopUpWindow()
        {
            popUpWindow.PopUp(poiName, nipperPopUp, myViewAction);
        }

        public void displaySummary()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
            summaryWindow.SummaryOneImage(poiName, description, poiImage, myCloseAction);
        }

        public void setDetected(bool detected)
        {
            this.detected = detected;
        }

        public void popUpSound()
        {
            popUp.Play();
        }

        public void playBeforeSound()
        {
            beforeSound.Play();
        }

        public bool isDetected()
        {
            return detected;
        }

        public iBeaconServer getBeacon()
        {
            return beacon;
        }

        public void attachObserver(Observer obs)
        {
            observers.Add(obs);
        }

        public void notify()
        {
            foreach (Observer obs in observers)
            {
                obs.update();
            }
        }
    }
}
