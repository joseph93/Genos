using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class PointOfTransition : Node
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
        public string color { get; set; }
        public string title { get; set; }

        public PointOfTransition(int id, int x, int y, int floorNumber, string label, string color, string title) : base(id, x, y,color, floorNumber)
        {
            this.label = convertStringToLabel(label);
            this.color = color;
            this.title = title;
        }

        public Label convertStringToLabel(string lbl)
        {
            switch (lbl)
            {
                case "RAMP":
                    return Label.RAMP;

                case "STAIRS" :
                    return Label.STAIRS;

                case "ELEVATOR":
                    return Label.ELEVATOR;

                case "WASHROOM":
                    return Label.WASHROOM;

                case "EXIT":
                    return Label.EXIT;

                case "ENTRANCE":
                    return Label.ENTRANCE;

                case "EMERGENCY":
                    return Label.EMERGENCY;

                case "NONE":
                    return Label.NONE;

            }

            return Label.NONE;
        }
    }
}
