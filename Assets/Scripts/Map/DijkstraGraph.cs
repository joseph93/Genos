using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DijkstraGraph : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   
        Dictionary<Node, Dictionary<Node, int>> vertices = new Dictionary<Node, Dictionary<Node, int>>();

        public void add_vertex(Node n, Dictionary<Node, int> edges)
        {
            vertices[n] = edges;
        }

        public List<Node> shortest_path(Node start, Node finish)
        {
            var previous = new Dictionary<Node, Node>();
            var distances = new Dictionary<Node, int>();
            var nodes = new List<Node>();

            List<Node> path = null;

            foreach (var vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == finish)
                {
                    path = new List<Node>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }

            return path;
        }
    }

/*

    class MainClass
    {

        // Verify if Nodes are all on the same floor
        // Verify if Nodes are adjacent

        public static void Main(string[] args)
        {
            Floor f1 = new Floor(1);
            Node n1 = new Node(1, 0, 0, f1);
            Node n2 = new Node(2, 0, 0, f1);
            Node n3 = new Node(3, 0, 0, f1);
            Node n4 = new Node(4, 0, 0, f1);
            Node n5 = new Node(5, 0, 0, f1);
            DijkstraGraph g = new DijkstraGraph();
            g.add_vertex(n1, new Dictionary<Node, int>() { { n2, 4 }, { n3, 2 } });
            g.add_vertex(n2, new Dictionary<Node, int>() { { n3, 3 }, { n5, 3 }, { n4, 2 } });
            g.add_vertex(n3, new Dictionary<Node, int>() { { n4, 4 }, { n5, 5 }, { n2, 1 } });
            g.add_vertex(n4, new Dictionary<Node, int>() { });
            g.add_vertex(n5, new Dictionary<Node, int>() { { n4, 1 } });

            g.shortest_path(n1, n4).ForEach(x => Console.WriteLine(x.getID()));
        }
    }
    */

