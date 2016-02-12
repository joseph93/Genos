using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class MyEdge
    {
        private int edgeWeight { get; set; }
        private Node source;
        private Node target;
         
        public MyEdge(Node source, Node target, int eW)
        {
            this.source = source;
            this.target = target;
            edgeWeight = eW;
        }  


    }
}
