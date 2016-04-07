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
    public class POS : PointOfInterest, IComparable<POS>
    {
        private int storylineID;
        [SerializeField]
        private int sequentialID; //JOSEPH: the order of the storypoint
        private bool visited;
        public bool detected { get; set; }
        public bool warned { get; set; }

        private ModalWindow modalWindow;
        private WarningPanel warningPanel;

        public Sprite iconImage;
        private UnityAction yesAction;
        private UnityAction noAction;

        private UnityAction myViewAction;
        private UnityAction viewVideoAction;

        public GameObject checkmarkIcon;

        private List<ExhibitionContent> contents;

        void Awake()
        {
            beacon = BeaconGameObject.GetComponent<iBeaconServer>();
            sounds = GetComponents<AudioSource>();
            popUp = sounds[0];
            beforeSound = sounds[1];
            observers = new List<Observer>();

            visited = false;
            detected = false;
            warned = false;

            
        }

        void Start()
        {
            popUpWindow = PopUpWindow.Instance();
            myViewAction = poiImage != null ? new UnityAction(POSdisplayImageWithCaption) : new UnityAction(playVideo);
            viewVideoAction = new UnityAction(playVideo);

            modalWindow = ModalWindow.Instance();
            yesAction = new UnityAction(modalWindow.closePanel);
            noAction = new UnityAction(modalWindow.closePanel);

            warningPanel = WarningPanel.Instance();

        }
        public POS(int id, int x, int y, string color, int floorNumber, PoiDescription poiD, int stID) : base(id, x, y, color, floorNumber)
        {
            storylineID = stID;
            visited = false;
            detected = false;
            warned = false;
            contents = new List<ExhibitionContent>();
        }

        public int getSequentialID()
        {
            return sequentialID;
        }

        //JOSEPH: sort the storypoints by sequential ID (from smallest to greatest)
        public int CompareTo(POS comparePos)
        {
            if (comparePos == null)
                return -1;

            else
                return this.sequentialID.CompareTo(comparePos.sequentialID);
        }

        public void setVisited(bool visited)
        {
            this.visited = visited;
            print("Storypoint is visited");
            notify();
        }

        public bool isVisited()
        {
            return visited;
        }

        public void storyIsVisited()
        {
            checkmarkIcon.SetActive(true);
        }

        public void POSdisplayImageWithCaption()
        {
            modalWindow.ChoiceOneButton(caption, poiImage, viewVideoAction);
        }

        public void playVideo()
        {
            StartCoroutine(CoroutinePlayVideo());
        }

        public IEnumerator CoroutinePlayVideo()
        {
            for (int i = 0; i < videoPath.Length; i++)
            {
                Handheld.PlayFullScreenMovie(videoPath[i], Color.black, FullScreenMovieControlMode.Full);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                print("I want to play video.");
            }

            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
            transform.localScale = new Vector3(0.05f, 0.05f, 1);
        }

        public void displayStorylinePopUpWindow()
        {
            popUpWindow.PopUp(poiDescription.title, nipperPopUp, myViewAction);
        }

        public void displayWarning(string description)
        {
            warningPanel.Warn(description, iconImage, yesAction, noAction);
        }
    }
}
