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
    public class FloorPlan
    {
        public string floorNumber { get; set; }
        public string imagePath { get; set; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }
        
<<<<<<< HEAD
        public string getImagePath()
        {
            return imagePath;
        }

        public void setImagePath(string imagePath)
        {
            this.imagePath = imagePath;
        }

=======
        
>>>>>>> 1630dbd41bef7c44a09d16c80b0a613b495e14ec
        public FloorPlan(string fn, string ip, int iw, int ih)
        {
            floorNumber = fn;
            imagePath = ip;
            imageWidth = iw;
            imageHeight = ih;
        }

        void Start()
        {
        }

        public int getImageWidth()
        {
            return imageWidth;
        }

        public int getImageHeight()
        {
            return imageHeight;
        }

<<<<<<< HEAD
        void Start()
        {
            LoadFloor();
        }

        public void LoadFloor()
        {
/*            
            //test
            string floor3 = "floor3"; 
            var tex = Resources.Load(floor3) as Texture2D;
*/
            var tex = Resources.Load(imagePath) as Texture2D;
            GetComponent<RawImage>().texture = tex;
            if (tex != null)
                GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }

=======
>>>>>>> 1630dbd41bef7c44a09d16c80b0a613b495e14ec

    }
}