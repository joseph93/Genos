using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class LabeledNode : Node
    {
        public enum Label
        {
            RAMP,
            STAIRS,
            ELEVATOR,
            NONE,
            WASHROOM,
            EXIT,
            ENTRANCE,
            EMERGENCY
        }

        private Label label { get; set; }

        public LabeledNode(int id, int x, int y, int floorNumber, Label label) : base(id, x, y, floorNumber)
        {
            this.label = label;
        }
    }
}
