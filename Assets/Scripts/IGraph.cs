using UnityEngine;
using System.Collections;

public interface IGraph {

	
        bool Contains(Node vertex);
        void InsertDirectEdge(int edgeAKey, int edgeBKey, int weight = 0);
        void InsertNewVertex(int vertexKey);
        bool ExistKey(int vertexKey);
        // Node InitializeDFS(int vertexKeyToFind);
        // bool MakeItBipartite();
        // Node DFS(Node root, int vertexKeyToFind);
        //  void FindNumberOfConnectedComponents();
        void BFS(int startVertexKey);
        Node InitializeBFS(int vertexKeyToFind);
        bool IsVisited(Node v);

        //  Node MarkVertexAsVisited(Node v);
        Node GetFirstElementOfTheList(int findKey);
        void InsertUndirectedEdge(int vertexAKey, int vertexBKey, int Weight = 0);
        //  Node PrimAlgorithm();
        Node FindByKey(int vertexKey);
    }

