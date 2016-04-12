using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts;


public enum State { Visited = 0, UnVisited = 1, Processed = 2 }
[ExecuteInEditMode]
public class Node : MonoBehaviour{

    
    private State Status = State.UnVisited;
    public int id;
    public int floorNumber;
    private Dictionary<Node, float> adjacentNodes;
    public float x;
    public float y;
    public string color { get; set; }

    public Node(int id, int x, int y, int floorNumber)
    {
        this.x = x;
        this.y = y;
        this.floorNumber = floorNumber;
        this.id = id;
        adjacentNodes = new Dictionary<Node, float>();
    }

    public Node(Node copyNode)
    {
        id = copyNode.id;
        x = copyNode.x;
        y = copyNode.y;
        floorNumber = copyNode.floorNumber;
        color = copyNode.color;
        adjacentNodes = copyNode.adjacentNodes;
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

    public int getFloorNumber()
    {
        return floorNumber;
    }

    //JOSEPH: Changed the type to "float" for the edge distance since it takes less memory (7 digits, 32 bit) 
    //than doubles (15-16 digits, 64 bit).
    public Dictionary<Node, float> getAdjacentNodes()
    {
        return adjacentNodes;
    } 

    //JOSEPH: Refactored - adds a dictionary of the adjacent nodes for this node
    public void addListOfAdjacentNodes(Dictionary<Node, float> aN)
    {
        adjacentNodes = aN;
    }

    //JOSEPH: Added - Adds an adjacent node to the dictionary
    public void addAdjacentNode(Node adjacentNode, float weight)
    {
        if (!isAdjacent(adjacentNode))
        {
            adjacentNodes.Add(adjacentNode, weight);
            
        }
        
    }

    public bool isAdjacent(Node n)
    {
        return adjacentNodes.ContainsKey(n);
    }
    
    public Vector3 getPosition()
    {
        return transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public override bool Equals(object obj)
    {
        Node n = (Node) obj;
        return id == n.id && x == n.x && y == n.y && floorNumber == n.floorNumber;
    }

    public static bool operator ==(Node a, Node b)
    {
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        return a.id == b.id && a.x == b.x && a.y == b.y && a.floorNumber == b.floorNumber;
    }

    public static bool operator !=(Node a, Node b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        int temp = id.GetHashCode() + x.GetHashCode() + y.GetHashCode() + floorNumber.GetHashCode();
        return temp;
    }
}
