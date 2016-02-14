using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using UnityEditorInternal;
=======
using UnityEngine;
>>>>>>> 6ab2d662f68cd329c1f999fb179d3fd170b3911a

namespace Assets.Scripts
{

    public enum State { Visited = 0, UnVisited = 1, Processed = 2 }
    public class Node
    {
<<<<<<< HEAD
        public State Status = State.UnVisited;
        private int x;
        private int y;
        private Floor floor;

        public Node Next;
        public int Weight = 0;
        public int Key;
        public int Value;
=======
        private int x { get; set; }
        private int y { get; set; }
>>>>>>> 6ab2d662f68cd329c1f999fb179d3fd170b3911a

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

<<<<<<< HEAD
        public Node(int key, int value)
        {
            this.Key = key;
            this.Value = value;
        }

=======
>>>>>>> 6ab2d662f68cd329c1f999fb179d3fd170b3911a
    }
}
