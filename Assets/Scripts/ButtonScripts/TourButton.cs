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
        }

        public void loadHelp()
        {
            SceneManager.LoadScene("Help");
        }

        public void loadLang()
        {
            SceneManager.LoadScene("Lang");
        }

        public void loadMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void startFreeRoaming()
        {
            SceneManager.LoadScene("FreeRoaming");
        }

    }
}
