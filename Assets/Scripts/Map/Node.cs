using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int floorNumber;
    private Dictionary<Node, double> adjacentNodes;
    public int x;
    public int y;

    public Node(int id, int x, int y, int floorNumber)
    {
        this.x = x;
        this.y = y;
        this.floorNumber = floorNumber;
        this.id = id;
        adjacentNodes = new Dictionary<Node, double>();
    }

    public Node(Node n)
    {
        x = n.x;
        y = n.y;
        floorNumber = n.floorNumber;
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

    public bool IsVisited()
    {
        return Status == State.Visited;
    }

    public int getID()
    {
        return id;
    }

    public void setID(int id)
    {
        this.id = id;
    }

    public int GetFloorNumber()
    {
        return floorNumber;
    }

    public Dictionary<Node, double> getAdjacentNodes()
    {
        return adjacentNodes;
    } 

    public void addAdjacentNode(Dictionary<Node, double> aN)
    {
        adjacentNodes = aN;
    }

    public bool hasAdjacentNode(Node n)
    {
        return adjacentNodes.Keys.Any(key => n.getID() == key.getID());
    }
}
