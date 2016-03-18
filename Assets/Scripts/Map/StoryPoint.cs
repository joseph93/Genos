using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Language;
using Assets.Scripts.Observer_Pattern;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class StoryPoint : PointOfInterest
    {
        [SerializeField]
        private int sequentialID;
        private bool visited;
        public bool detected { get; set; }
        public bool warned { get; set; }
        [SerializeField] private string videoPath;

        private ModalWindow modalWindow;

        public Sprite iconImage;
        private UnityAction yesAction;
        private UnityAction noAction;

        private UnityAction myViewAction;

        void Awake()
        {
            beacon = BeaconGameObject.GetComponent<iBeaconServer>();
            sounds = GetComponents<AudioSource>();
            popUp = sounds[0];
            beforeSound = sounds[1];
            observers = new List<Observer>();
            detected = false;

            visited = false;
            detected = false;
            warned = false;

            
        }

        void Start()
        {
            popUpWindow = PopUpWindow.Instance();
            myViewAction = new UnityAction(playVideo);

            modalWindow = ModalWindow.Instance();
            yesAction = new UnityAction(modalWindow.closePanel);
            noAction = new UnityAction(modalWindow.closePanel);
        }
        public StoryPoint(int id, int x, int y, int floorNumber, PoiDescription poiD, string videoPath) : base(id, x, y, floorNumber, poiD)
        {
            this.videoPath = videoPath;
            visited = false;
            detected = false;
            warned = false;
        }

        public int getSequentialID()
        {
            return sequentialID;
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

        public string getVideoPath()
        {
            return videoPath;
        }

        public void playVideo()
        {
            Handheld.PlayFullScreenMovie(videoPath, Color.black, FullScreenMovieControlMode.Full);
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
        }

        public void displayPopUpWindow()
        {
            popUpWindow.PopUp(poiDescription.title, nipperPopUp, myViewAction);
        }

        public void notify()
        {
            foreach (Observer obs in observers)
            {
                obs.update();
            }
        }

        public void displayWarning(string description)
        {
            modalWindow.Choice(description, iconImage, yesAction, noAction);
        }
    }
}
