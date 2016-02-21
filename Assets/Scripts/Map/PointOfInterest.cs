using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class PointOfInterest : Node
    {
        private string description;
        private string name;
        private Beacon beacon;
        private bool visited;

        public PointOfInterest(int id, int x, int y, int floorNumber, string name, Beacon bc) : base(id, x, y, floorNumber)
        {
            this.name = name;
            this.beacon = bc;
            description = "";
            visited = false;
        }
    }
}
