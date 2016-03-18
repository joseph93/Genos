using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Language;
using Assets.Scripts.Observer_Pattern;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class PointOfInterest : Node
    {
        protected PoiDescription poiDescription;
        private bool detected;

        protected iBeaconServer beacon;
        public GameObject BeaconGameObject;
        protected AudioSource[] sounds;
        protected AudioSource popUp;
        protected AudioSource beforeSound;

        protected List<Observer> observers;

        protected PopUpWindow popUpWindow;
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
            detected = false;

            popUpWindow = PopUpWindow.Instance();
            summaryWindow = SummaryWindow.Instance();

            myViewAction = new UnityAction(displaySummary);
            myCloseAction = new UnityAction(popUpWindow.closePanel);
        }


        public PointOfInterest(int id, int x, int y, int floorNumber, PoiDescription poiD) : base(id, x, y, floorNumber)
        {
            poiDescription = poiD;
            detected = false;
        }

        public PoiDescription GetPoiDescription()
        {
            return poiDescription;
        }

        public iBeaconServer getBeacon()
        {
            return beacon;
        }

        public void setTitleAndSummary(string title, string summary)
        {
            if (poiDescription != null)
            {
                poiDescription.title = title;
                poiDescription.summary = summary;
            }
            else
            {
                poiDescription = new PoiDescription(title, summary);
            }
        }

        public void changeIconScale()
        {
            transform.localScale = new Vector3(1.8f, 1.8f, 1);
        }

        public void displayPopUpWindow()
        {
            popUpWindow.PopUp(poiDescription.title, nipperPopUp, myViewAction);
        }

        public void displaySummary()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
            summaryWindow.SummaryOneImage(poiDescription.title, poiDescription.summary, poiImage, myCloseAction);
        }

        public void setDetected(bool detected)
        {
            this.detected = detected;
            notify();
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

        public void attachObserver(Observer obs)
        {
            observers.Add(obs);
        }

        public void detachObserver(Observer obs)
        {
            observers.Remove(obs);
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
