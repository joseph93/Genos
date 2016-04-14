using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{


    //Ihcene: This will help us load scenes for the NipperTour
    public class TourButton : MonoBehaviour
    {
        

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame

        public void changeSceneToOutdoor()
        {
            SceneManager.LoadScene("3D-outdoor");
            Vibration.Vibrate(100);
        }

        public void changeSceneToTour()
        {
            SceneManager.LoadScene("TourSelection");
            Vibration.Vibrate(100);
        }

        public void loadFloor2Next()
        {
            SceneManager.LoadScene("F2-next");
            Vibration.Vibrate(100);
        }

        public void loadOverview()
        {
            SceneManager.LoadScene("F2");
            Vibration.Vibrate(100);
        }

        public void loadListOfPOI()
        {
            SceneManager.LoadScene("ListOfPointsOfInterest");
            Vibration.Vibrate(100);
        }

        public void changeSceneToQuit()
        {
            SceneManager.LoadScene("Quit");
            Vibration.Vibrate(100);
        }

        public void loadHelp()
        {
            SceneManager.LoadScene("Help");
            Vibration.Vibrate(100);
        }

        public void loadSettings()
        {
            SceneManager.LoadScene("Settings");
            Vibration.Vibrate(100);
        }

        public void loadLang()
        {
            SceneManager.LoadScene("Lang");
            Vibration.Vibrate(100);
        }

        public void loadMenu()
        {
            SceneManager.LoadScene("Menu");
            Vibration.Vibrate(100);
        }

        public void startFreeRoaming()
        {
            SceneManager.LoadScene("FreeRoaming");
            Vibration.Vibrate(100);
        }

    }
}
