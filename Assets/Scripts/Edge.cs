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
<<<<<<< HEAD:Assets/Scripts/Edge.cs
        }  



=======
        }
>>>>>>> 6ab2d662f68cd329c1f999fb179d3fd170b3911a:Assets/Scripts/MyEdge.cs
    }
}
