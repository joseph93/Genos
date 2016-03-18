using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Language;
using UnityEngine;

namespace Assets.Scripts
{
    public class StoryPoint : PointOfInterest
    {
        [SerializeField]
        private int sequentialID;
        private bool visited;
        public bool detected { get; set; }
        public bool warned { get; set; }
        //[SerializeField] private string videoPath;

        void Awake()
        {
            visited = false;
            detected = false;
            warned = false;
        }
        public StoryPoint(int id, int x, int y, int floorNumber, PoiDescription poiD) : base(id, x, y, floorNumber, poiD)
        {
            //this.videoPath = videoPath;
            visited = false;
            detected = false;
            warned = false;
        }

        public int getSequentialID()
        {
            return sequentialID;
        }

        public void setVisited(bool visited)
        {
            this.visited = visited;
            notify();
        }

        public bool isVisited()
        {
            return visited;
        }

       /* public string getVideoPath()
        {
            return videoPath;
        }

        public void playVideo()
        {
            Handheld.PlayFullScreenMovie(videoPath, Color.black, FullScreenMovieControlMode.Full,
                FullScreenMovieScalingMode.AspectFill);
        }*/
    }
}
