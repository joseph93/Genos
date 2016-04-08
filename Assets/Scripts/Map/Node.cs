using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts;


public enum State { Visited = 0, UnVisited = 1, Processed = 2 }
[ExecuteInEditMode]
public abstract class Node : MonoBehaviour{

    
    private State Status = State.UnVisited;
    [SerializeField] private readonly int id;
    [SerializeField] private readonly int floorNumber;
    private Dictionary<Node, float> adjacentNodes;
    public float x;
    public float y;
    public string color { get; set; }

    protected Node(int id, int x, int y, string color, int floorNumber)
    {
        this.x = x;
        this.y = y;
        this.floorNumber = floorNumber;
        this.id = id;
        this.color = color;
        adjacentNodes = new Dictionary<Node, float>();
    }

    protected Node(Node copyNode)
    {
        id = copyNode.id;
        x = copyNode.x;
        y = copyNode.y;
        floorNumber = copyNode.floorNumber;
        color = copyNode.color;
        adjacentNodes = copyNode.adjacentNodes;
    }

    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
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

    public int GetFloorNumber()
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
            //Debug.Log("Im before the if statement for node " + id);
            adjacentNodes.Add(adjacentNode, weight);
            Debug.Log("Node " + id + " added to his adjacent nodes Node " + adjacentNode.id);
            //Debug.Log("Added adjacent node " + adjacentNode.id + " to node " + this.id + " with edge weight " + weight);
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

    public abstract void setBeacon(iBeaconServer b);

    public abstract void addContent(ExhibitionContent c);

    public abstract void setLanguage(string lg);

    public abstract void setTitle(string title);

    public abstract void setDescription(string descr);
}
