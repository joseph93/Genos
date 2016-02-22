using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class StoryPoint : PointOfInterest
    {
        public StoryPoint(int id, int x, int y, int floorNumber, string name) : base(id, x, y, floorNumber, name)
        {
        }
    }
}
