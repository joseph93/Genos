using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditorInternal;

namespace Assets.Scripts
{

    public enum State { Visited = 0, UnVisited = 1, Processed = 2 }
    public class Node
    {
        public State Status = State.UnVisited;
        private int x;
        private int y;
        private Floor floor;

        public Node Next;
        public int Weight = 0;
        public int Key;
        public int Value;

        /*
        public Node(int x, int y, Floor floor)
        {
            this.x = x;
            this.y = y;
            this.floor = floor;
        }
        */
        public Node(int key)
        {
            this.Key = key;
            this.Value = 0;
        }

        public Node(int key, int value)
        {
            this.Key = key;
            this.Value = value;
        }

    }
}
