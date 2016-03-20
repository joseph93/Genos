using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class FloorManager : MonoBehaviour
    {
        public Sprite floor2;
        public Sprite floor3;
        public Text floorNumber;

        private NipperTour nipperTour;
        private List<Node> nodes;

        void Start()
        {
            nipperTour = FindObjectOfType<NipperTour>();
        }

        void Update()
        {
            nodes = nipperTour.getNodeList();
        }

        public void loadFloor2()
        {
            GetComponent<SpriteRenderer>().sprite = floor2;
            floorNumber.text = "Floor 2";
            foreach (var n in nodes)
            {
                if (n.GetFloorNumber() == 2)
                {
                    n.gameObject.SetActive(true);
                }
                else
                {
                    n.gameObject.SetActive(false);
                }
            }
        }

        public void loadFloor3()
        {
            GetComponent<SpriteRenderer>().sprite = floor3;
            floorNumber.text = "Floor 3";
            foreach (var n in nodes)
            {
                if (n.GetFloorNumber() == 3)
                {
                    n.gameObject.SetActive(true);
                }
                else
                {
                    n.gameObject.SetActive(false);
                }
            }
        }

        
    }
}
