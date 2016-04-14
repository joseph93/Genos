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
        protected List<Description> descriptionList;
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
        public string[] titles;
        public string[] descriptions;
        public AudioClip[] audioSources;

        private UnityAction viewVideoAction;
        private ModalWindow modalWindow;

        private readonly List<ExhibitionContent> contents; 

        void Awake()
        {
            beacon = BeaconGameObject.GetComponent<iBeaconServer>();
            sounds = GetComponents<AudioSource>();
           // popUp = sounds[0];
            //beforeSound = sounds[1];
            observers = new List<Observer>();
            detected = false;

            popUpWindow = PopUpWindow.Instance();
            summaryWindow = SummaryWindow.Instance();
            viewVideoAction = new UnityAction(POIplayVideo);

            modalWindow = ModalWindow.Instance();

            myViewAction = new UnityAction(displaySummary);
            myCloseAction = new UnityAction(popUpWindow.closePanel);
        }


        public PointOfInterest(int id, int x, int y, int floorNumber) : base(id, x, y, floorNumber)
        {
            detected = false;
            contents = new List<ExhibitionContent>();
            descriptionList = new List<Description>();
            observers = new List<Observer>();
            popUpWindow = PopUpWindow.Instance();
            modalWindow = ModalWindow.Instance();
        }

        public void setDescriptionList(List<Description> descr)
        {
            descriptionList = descr;
        }

        public List<Description> GetPoiDescriptionList()
        {
            return descriptionList;
        }

        public void addPoiDescription(Description pd)
        {
            descriptionList.Add(pd);
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
            transform.localScale = new Vector3(1f, 1f, 1);
        }

        public void POIdisplayImageWithCaption()
        {
            modalWindow.ChoiceOneButton(caption, poiImage, viewVideoAction);
        }

        public void displayPopUpWindow()
        {
            /*
            foreach (var descr in descriptionList)
            {

                
            }

            string lg = PlayerPrefs.GetString("language");

            Language.Language lang = Description.convertStringToLang(lg);

            foreach (var d in descriptionList)
            {
                if (lang == d.language)
                {
                    popUpWindow.PopUp(d.title, myViewAction);
                    break;
                }
            }
            */


            //popUpWindow.PopUp(poiDescriptionList.title, nipperPopUp, myViewAction);
            //CHANGE HERE WITH LIST OF POIDESCRIPTION


            string lg = PlayerPrefs.GetString("language");

            if (lg.Equals("EN"))
            {
                popUpWindow.PopUp(titles[0], myViewAction);
            }

            else if (lg.Equals("FR"))
            {
                popUpWindow.PopUp(titles[1], myViewAction);
            }
        }

        public void displaySummary()
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 1);
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
            //summaryWindow.SummaryOneButton(poiDescriptionList.title, poiDescriptionList.summary, poiImage, myCloseAction);


            string lg = PlayerPrefs.GetString("language");

            if (lg.Equals("EN"))
            {
                summaryWindow.SummaryOneButton(titles[0], descriptions[0], myCloseAction);
            }

            else if (lg.Equals("FR"))
            {

                summaryWindow.SummaryOneButton(titles[1], descriptions[1], myCloseAction);
            }
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
