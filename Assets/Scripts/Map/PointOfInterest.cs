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
        protected List<PoiDescription> poiDescriptionList;
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

        public string[] videoPath;
        public string caption;

        private UnityAction viewVideoAction;
        private ModalWindow modalWindow;

        private readonly List<ExhibitionContent> contents; 

        void Awake()
        {
            beacon = BeaconGameObject.GetComponent<iBeaconServer>();
            sounds = GetComponents<AudioSource>();
            //popUp = sounds[0];
            //beforeSound = sounds[1];
            observers = new List<Observer>();
            detected = false;

            popUpWindow = PopUpWindow.Instance();
            summaryWindow = SummaryWindow.Instance();
            viewVideoAction = new UnityAction(POIplayVideo);

            modalWindow = ModalWindow.Instance();

            myViewAction = poiImage != null ? new UnityAction(POIdisplayImageWithCaption) : new UnityAction(POIplayVideo);
            myCloseAction = new UnityAction(popUpWindow.closePanel);
        }


        public PointOfInterest(int id, int x, int y, int floorNumber) : base(id, x, y, floorNumber)
        {
            detected = false;
            contents = new List<ExhibitionContent>();
            poiDescriptionList = new List<PoiDescription>();
        }

        public List<PoiDescription> GetPoiDescriptionList()
        {
            return poiDescriptionList;
        }

        public void addPoiDescription(PoiDescription pd)
        {
            poiDescriptionList.Add(pd);
        }

        public iBeaconServer getBeacon()
        {
            return beacon;
        }

        public void setBeacon(iBeaconServer b)
        {
            beacon = b;
        }

        public List<ExhibitionContent> getContents()
        {
            return contents;
        }

        public void addContent(ExhibitionContent c)
        {
            contents.Add(c);
        }

        /*public void setTitleAndSummary(string title, string summary, string lg)
        {
            if (poiDescriptionList != null)
            {
                poiDescriptionList.title = title;
                poiDescriptionList.summary = summary;
            }
            else
            {
                poiDescriptionList = new PoiDescription(title, summary, lg);
            }
        }*/

        public void changeIconScale()
        {
            transform.localScale = new Vector3(0.08f, 0.08f, 1);
        }

        public void POIdisplayImageWithCaption()
        {
            modalWindow.ChoiceOneButton(caption, poiImage, viewVideoAction);
        }

        public void displayPopUpWindow()
        {
            //popUpWindow.PopUp(poiDescriptionList.title, nipperPopUp, myViewAction);
            //CHANGE HERE WITH LIST OF POIDESCRIPTION
        }

        public void displaySummary()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
            //summaryWindow.SummaryOneImage(poiDescriptionList.title, poiDescriptionList.summary, poiImage, myCloseAction);
            //CHANGE HERE WITH LIST OF POIDESCRIPTION
        }

        public void setDetected(bool detected)
        {
            this.detected = detected;
            notify();
        }

        public void POIplayVideo()
        {
            StartCoroutine(POIcoroutinePlayVideo());
        }

        public IEnumerator POIcoroutinePlayVideo()
        {
            for (int i = 0; i < videoPath.Length; i++)
            {
                Handheld.PlayFullScreenMovie(videoPath[i], Color.black, FullScreenMovieControlMode.Full);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                print("I want to play video.");
            }
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
