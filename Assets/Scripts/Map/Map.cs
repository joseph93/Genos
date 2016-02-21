using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Map
    {

        private List<Storyline> storylines;
        private Graph mapGraph;

        public Map()
        {
            storylines = new List<Storyline>();
            mapGraph = new Graph();
        }

        public void addStoryline(Storyline sl)
        { 
            storylines.Add(sl);
        }

        public Graph getGraph()
        {
            return mapGraph;
        }

    }
}
