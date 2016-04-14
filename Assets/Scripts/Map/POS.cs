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
        public int storylineID { get; set; }
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
            //beacon = BeaconGameObject.GetComponent<iBeaconServer>();
            sounds = GetComponents<AudioSource>();
            //popUp = sounds[0];
            //beforeSound = sounds[1];
            observers = new List<Observer>();

            visited = false;
            detected = false;
            warned = false;

            
        }

        void Start()
        {
            popUpWindow = PopUpWindow.Instance();
            //myViewAction = poiImage != null ? new UnityAction(POSdisplayImageWithCaption) : new UnityAction(playVideo);
            //viewVideoAction = new UnityAction(playVideo);

            modalWindow = ModalWindow.Instance();
            yesAction = new UnityAction(modalWindow.closePanel);
            noAction = new UnityAction(modalWindow.closePanel);

            warningPanel = WarningPanel.Instance();

        }
        public POS(int id, int x, int y, int floorNumber, int stID) : base(id, x, y, floorNumber)
        {
            storylineID = stID;
            visited = false;
            detected = false;
            warned = false;
            contents = new List<ExhibitionContent>();
            descriptionList = new List<Description>();
            observers = new List<Observer>();
            popUpWindow = PopUpWindow.Instance();
            myViewAction = new UnityAction(playVideo);
            //modalWindow = ModalWindow.Instance();
            warningPanel = WarningPanel.Instance();
          //  yesAction = new UnityAction(warningPanel.closePanel);
         //   noAction = new UnityAction(warningPanel.closePanel);
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

        public void addContent(ExhibitionContent c)
        {
            contents.Add(c);
        }

        public List<ExhibitionContent> getContents()
        {
            return contents;
        }

        public void setContents(List<ExhibitionContent> contents)
        {
            this.contents = contents;
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

        public void playAudio()
        {
            foreach (var audio in contents)
            {
                if (audio.GetType() == typeof (Audio))
                {
                    AudioClip a = Resources.Load(audio.path) as AudioClip;
                    AudioSource source = GameObject.Find("FloorManager").AddComponent<AudioSource>();
                    source.PlayOneShot(a);
                }
            }
        }

        public void playContent()
        {
            StartCoroutine(CoroutinePlayVideo());
            playAudio();
        }

        public void playVideo()
        {
            string lg = PlayerPrefs.GetString("language");

            Language.Language lang = Description.convertStringToLang(lg);

            foreach (var video in contents)
            {
                if (video.GetType() == typeof(Video))
                {
                    if (video.lg == lang)
                    {
                        print("Storypoint " + id + " is playing video " + video.path);
                        Screen.orientation = ScreenOrientation.Landscape;
                        Handheld.PlayFullScreenMovie(video.path, Color.black, FullScreenMovieControlMode.Full);
                    }
                }

            }
            Screen.orientation = ScreenOrientation.Portrait;
        }

        public IEnumerator CoroutinePlayVideo()
        {
            
            foreach (var video in contents)
            {
                if (video.GetType() == typeof (Video))
                {
                    print("Storypoint " + id + " is playing video " + video.path);
                    Screen.orientation = ScreenOrientation.Landscape;
                    Handheld.PlayFullScreenMovie(video.path, Color.black, FullScreenMovieControlMode.Full);
                    yield return new WaitForEndOfFrame();
                    yield return new WaitForEndOfFrame();
                    
                }
                
            }
            Screen.orientation = ScreenOrientation.Portrait;
            /*SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
            transform.localScale = new Vector3(0.05f, 0.05f, 1);*/
        }

        public void displayStorylinePopUpWindow()
        {
            string lg = PlayerPrefs.GetString("language");

            Language.Language lang = Description.convertStringToLang(lg);

            foreach(var d in descriptionList)
            {
                if(lang == d.language)
                {
                    popUpWindow.PopUp(d.title, myViewAction);
                    break;
                }
            }
            //CHANGE HERE WITH STORYPOINT DESCRIPTION
        }

        public void displayWarning(string description)
        {
            warningPanel.Warn(description, yesAction, noAction);
        }
    }
}
