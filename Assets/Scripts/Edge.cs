using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Edge
    {
        private int edgeWeight { get; set; }

        private Node _startNode;
        private Node _endNode;
         
        public Edge(Node startNode, Node endNode, int eW)
        {
            this._startNode = startNode;
            this._endNode = endNode;
            edgeWeight = eW;
        }  



    }
}
