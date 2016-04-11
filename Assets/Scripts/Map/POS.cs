using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Exhibition_Content;
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

        private List<StoryPointDescription> storypointDescrList; 
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
        public POS(int id, int x, int y, string color, int floorNumber, int stID) : base(id, x, y, color, floorNumber)
        {
            storylineID = stID;
            visited = false;
            detected = false;
            warned = false;
            contents = new List<ExhibitionContent>();
            storypointDescrList = new List<StoryPointDescription>();
        }

        public void addStorypointDescription(StoryPointDescription sd)
        {
            storypointDescrList.Add(sd);
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
            foreach (var image in contents)
            {
                if (image.GetType() == typeof (Image))
                {
                    modalWindow.ChoiceOneButton(caption, poiImage, viewVideoAction);
                }
                
            }
            
        }

        public void playVideo()
        {
            StartCoroutine(CoroutinePlayVideo());
        }

        public IEnumerator CoroutinePlayVideo()
        {
            foreach (var video in contents)
            {
                if (video.GetType() == typeof (Video))
                {
                    Screen.orientation = ScreenOrientation.Landscape;
                    Handheld.PlayFullScreenMovie(video.path, Color.black, FullScreenMovieControlMode.Full);
                    yield return new WaitForEndOfFrame();
                    yield return new WaitForEndOfFrame();
                    
                }
                
            }
            Screen.orientation = ScreenOrientation.Portrait;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
            transform.localScale = new Vector3(0.05f, 0.05f, 1);
        }

        public void displayStorylinePopUpWindow()
        {
            //CHANGE HERE WITH STORYPOINT DESCRIPTION
            //popUpWindow.PopUp(poiDescriptionList.title, nipperPopUp, myViewAction);
        }

        public void displayWarning(string description)
        {
            warningPanel.Warn(description, iconImage, yesAction, noAction);
        }
    }
}
