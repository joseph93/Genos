using UnityEngine;
using System.Collections;
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
        void Update()
        {

        }

        public void changeSceneToTour()
        {
            SceneManager.LoadScene("TourSelection");
            Vibration.Vibrate(1000);
        }

        public void changeSceneToFloor()
        {
            SceneManager.LoadScene("F2");
            Vibration.Vibrate(1000);
        }

        public void loadFloor2Next()
        {
            SceneManager.LoadScene("F2-next");
<<<<<<< HEAD
=======
            Vibration.Vibrate(1000);
>>>>>>> 42cb9eef07718cc2400897a67846fc0af9903aef
        }

        public void backToOverview()
        {
            SceneManager.LoadScene("F2");
            Vibration.Vibrate(1000);
        }

        public void changeSceneToQuit()
        {
            SceneManager.LoadScene("Quit");
        }
    }
}
