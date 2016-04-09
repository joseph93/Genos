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
        public PointOfTransition(int id, int x, int y, string color, int floorNumber, string label,  string title) : base(id, x, y,color, floorNumber)
        {
            this.label = convertStringToLabel(label);
            this.title = title;
        }

        public PointOfTransition(PointOfTransition copyPOT) : base(copyPOT)
        {
            label = copyPOT.label;
            title = copyPOT.title;
        }
        public Label label { get; set; }

        public string title { get; set; }

        

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
