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

        public Label label { get; set; }

        public LabeledNode(int id, int x, int y, Floor floor, Label label) : base(id, x, y, floor)
        {
            this.label = label;
        }
    }
}
