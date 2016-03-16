using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class StoryPoint : PointOfInterest
    {
        [SerializeField] private string videoPath;

        public StoryPoint(int id, int x, int y, int floorNumber, string storyName) : base(id, x, y, floorNumber, storyName)
        {
        }
    }
}
