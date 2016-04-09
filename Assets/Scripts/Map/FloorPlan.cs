using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FloorPlan : MonoBehaviour
    {
        private string prefix = "http://localhost";
        public string floorNumber { get; set; }
        public string imagePath { get; set; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }
        private Texture2D tex;

        public FloorPlan(string fn, string ip, int iw, int ih)
        {
            floorNumber = fn;
            imagePath = prefix + ip;
            imageWidth = iw;
            imageHeight = ih;
        }

        void Start()
        {
            StartCoroutine(LoadImgIntoTxture());
        }

        public IEnumerator LoadImgIntoTxture()
        {
            yield return 0;
            WWW www = new WWW("http://localhost/floor/3/floor3.png");
            yield return www;
            tex = www.texture;
            GetComponent<RawImage>().texture = tex;
            //GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0,0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));
        }


    }
}
