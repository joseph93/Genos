using UnityEngine;
using System.Collections;
using Assets.Scripts;


public enum State { Visited = 0, UnVisited = 1, Processed = 2 }

public class Node : MonoBehaviour {

 

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    
        public State Status = State.UnVisited;

        public Floor floor;

        public Node Next;
        public int Weight = 0;
        public int x;
        public int y;

        /*
        public Node(int x, int y, Floor floor)
        {
            this.x = x;
            this.y = y;
            this.floor = floor;
        }
        */
        public Node(int x)
        {
            this.x = x;
            this.y = 0;
        }

        public Node(int x, int y, Floor floor)
        {
            this.x = x;
            this.y = y;
            this.floor = floor;
        }

    


}
