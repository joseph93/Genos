using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class FloorManager : MonoBehaviour
    {
        public Sprite floor2;
        public Sprite floor3;
        public Text floorNumber;

        //to save last floor scene
        private static string lastFloorScene;


        void Start()
        {

        }

        public void loadFloor2()
        {
            GetComponent<SpriteRenderer>().sprite = floor2;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            floorNumber.text = "Floor 2";
        }

        public void loadFloor3()
        {
            GetComponent<SpriteRenderer>().sprite = floor3;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            floorNumber.text = "Floor 3";
        }

        

        public static void setLastScene(string scene)
        {
            lastFloorScene = scene;
        }

        public static string getLastScene()
        {
            return lastFloorScene;
        }

        public static void changeToPreviousScene()
        {
            Application.LoadLevel(lastFloorScene);
        }
    }
}
