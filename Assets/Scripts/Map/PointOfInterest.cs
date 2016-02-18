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

        public PointOfInterest(int id, int x, int y, Floor floor, string name, Beacon bc) : base(id, x, y, floor)
        {
            this.name = name;
            this.beacon = bc;
            description = "";
            visited = false;
        }
    }
}
