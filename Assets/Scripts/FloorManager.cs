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

        public void loadFloor2()
        {
            GetComponent<SpriteRenderer>().sprite = floor2;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            floorNumber.text = "Floor 2";
        }

        void Start()
        {
            
        }

        public void loadFloor3()
        {
            GetComponent<SpriteRenderer>().sprite = floor3;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            floorNumber.text = "Floor 3";
        }

       
    }
}
