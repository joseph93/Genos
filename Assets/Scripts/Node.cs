using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Node
    {
        private int x { get; set; }
        private int y { get; set; }

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
