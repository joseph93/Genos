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
        public PointOfTransition(int id, int x, int y, int floorNumber, string label) : base(id, x, y, floorNumber)
        {
            this.label = convertStringToLabel(label);
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
                case "ramp":
                    return Label.RAMP;

                case "stairs" :
                    return Label.STAIRS;

                case "elevator":
                    return Label.ELEVATOR;

                case "washroom":
                    return Label.WASHROOM;

                case "exit":
                    return Label.EXIT;

                case "entrance":
                    return Label.ENTRANCE;

                case "emergency":
                    return Label.EMERGENCY;

                case "none":
                    return Label.NONE;

            }

            return Label.NONE;
        }
    }
}
