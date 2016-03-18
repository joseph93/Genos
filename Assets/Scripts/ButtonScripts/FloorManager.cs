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

        public GameObject nodeContainer2;
        public GameObject nodeContainer3;

        //to save last floor scene
        private static string lastFloorScene;

        public void loadFloor2()
        {
            GetComponent<SpriteRenderer>().sprite = floor2;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            floorNumber.text = "Floor 2";
            nodeContainer2.SetActive(true);
            nodeContainer3.SetActive(false);
        }

        public void loadFloor3()
        {
            GetComponent<SpriteRenderer>().sprite = floor3;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            floorNumber.text = "Floor 3";
            nodeContainer2.SetActive(false);
            nodeContainer3.SetActive(true);
        }

        
    }
}
