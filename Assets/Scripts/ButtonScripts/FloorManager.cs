using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class FloorManager : MonoBehaviour
    {
        /*public Sprite floor2;
        public Sprite floor3;
        public Text floorNumber;

        private FreeRoamingDriver _freeRoamingDriver;
        private List<Node> nodes;*/

        void Start()
        {
            
        }

        void Update()
        {
            Renderer rend = GetComponent<Renderer>();
            print("Bounds max x: " + rend.bounds.max.x);
            print("Bounds max y: " + rend.bounds.max.y);
        }
        /*
        public IEnumerator getMap()
        {
            yield return new WaitForSeconds(1.5f);
            nodes = _freeRoamingDriver.getMap().GetPoiNodes();
        }

        public void loadFloor2()
        {
            GetComponent<SpriteRenderer>().sprite = floor2;
            floorNumber.text = "Floor 2";
            displayFloor2Nodes();
        }

        private void displayFloor2Nodes()
        {
            foreach (var n in nodes)
            {
                n.gameObject.SetActive(n.getFloorNumber() == 2);
            }
        }

        public void loadFloor3()
        {
            GetComponent<SpriteRenderer>().sprite = floor3;
            floorNumber.text = "Floor 3";
            displayFloor3Nodes();
        }

        private void displayFloor3Nodes()
        {
            foreach (var n in nodes)
            {
                n.gameObject.SetActive(n.getFloorNumber() == 3);
            }
        }*/
    }
}
