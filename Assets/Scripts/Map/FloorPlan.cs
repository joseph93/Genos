using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class FloorPlan : MonoBehaviour
    {
        public int floorNumber { get; set; }
        public string imagePath { get; set; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }

        public FloorPlan(int fn, string ip, int iw, int ih)
        {
            floorNumber = fn;
            imagePath = ip;
            imageWidth = iw;
            imageHeight = ih;
        }

        public Texture2D LoadPNG(string filePath)
        {

            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }
    }
}
