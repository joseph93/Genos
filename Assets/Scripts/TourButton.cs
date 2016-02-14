using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
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
        }

        public void changeSceneToFloor()
        {
            SceneManager.LoadScene("F2");
        }
    }
}
