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


    private State Status = State.UnVisited;
    private int id;
    private Floor floor;
    private Node next;
    public int x;
    public int y;
        

    public Node(int id, int x, int y, Floor floor)
    {
        this.x = x;
        this.y = y;
        this.floor = floor;
        this.id = id;
    }

    public Node(Node n)
    {
        x = n.x;
        y = n.y;
        floor = n.floor;
        id = n.id;
    }

    public State getState()
    {
        return Status;
    }

    public void setState(State newState)
    {
        Status = newState;
    }

    public int getID()
    {
        return id;
    }

    public void setID(int id)
    {
        this.id = id;
    }

    public Floor GetFloor()
    {
        return floor;
    }

    public Node getNext()
    {
        return next;
    }

    public void setNext(Node next)
    {
        this.next = next;
    }

}
