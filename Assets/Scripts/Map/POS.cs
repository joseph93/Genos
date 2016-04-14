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

        [SerializeField] private string[] videoPaths;
        [SerializeField] private string[] titles;
        [SerializeField] private string[] descriptions;

        void Awake()
        {
            /*visited = false;
            detected = false;
            warned = false;
            contents = new List<ExhibitionContent>();
            descriptionList = new List<Description>();
            observers = new List<Observer>();
            popUpWindow = PopUpWindow.Instance();
            myViewAction = new UnityAction(playContent);
            //modalWindow = ModalWindow.Instance();
            warningPanel = WarningPanel.Instance();
            yesAction = new UnityAction(warningPanel.closePanel);
            noAction = new UnityAction(warningPanel.closePanel);*/
        }

        void Start()
        {
            visited = false;
            detected = false;
            warned = false;
            contents = new List<ExhibitionContent>();
            descriptionList = new List<Description>();
            observers = new List<Observer>();
            popUpWindow = PopUpWindow.Instance();
            myViewAction = new UnityAction(playContent);
            //modalWindow = ModalWindow.Instance();
            warningPanel = WarningPanel.Instance();
            yesAction = new UnityAction(warningPanel.closePanel);
            noAction = new UnityAction(warningPanel.closePanel);

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
            myViewAction = new UnityAction(playContent);
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

        public void setPopUpInstance()
        {
            popUpWindow = PopUpWindow.Instance();
        }

        public void setViewAction(UnityAction action)
        {
            myViewAction = action;
        }

        public UnityAction getViewAction()
        {
            return myViewAction;
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
            //playAudio();
        }


        public IEnumerator playVideo()
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
                        yield return new WaitForEndOfFrame();
                        yield return new WaitForEndOfFrame();
                    }
                }

            }
            Screen.orientation = ScreenOrientation.Portrait;
        }

        public IEnumerator CoroutinePlayVideo()
        {
            string lg = PlayerPrefs.GetString("language");
            
                if (lg.Equals("EN"))
                {
                    Screen.orientation = ScreenOrientation.Landscape;
                    Handheld.PlayFullScreenMovie(videoPaths[0], Color.black, FullScreenMovieControlMode.Full);
                    yield return new WaitForEndOfFrame();
                    yield return new WaitForEndOfFrame();
                    
                }

            else if (lg.Equals("FR"))
            {
                Screen.orientation = ScreenOrientation.Landscape;
                Handheld.PlayFullScreenMovie(videoPaths[1], Color.black, FullScreenMovieControlMode.Full);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }

            Screen.orientation = ScreenOrientation.Portrait;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color32(140, 115, 115, 255);
            transform.localScale = new Vector3(0.05f, 0.05f, 1);
        }
        
        public void displayStorylinePopUpWindow()
        {
            
            string lg = PlayerPrefs.GetString("language");
            
                if(lg.Equals("EN"))
                {
                    popUpWindow.PopUp(titles[0], myViewAction);
                }

                else if (lg.Equals("FR"))
                {
                    popUpWindow.PopUp(titles[1], myViewAction);
                }
        }

        public void displayWarning(string description)
        {
            warningPanel.Warn(description, yesAction, noAction);
        }
    }
}
