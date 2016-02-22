using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assets.Scripts
{
    public class GraphTests
    {
        [Fact]
        public void shortest_pathTest()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new Node(1, 0, 0, 1);
                Node n2 = new Node(2, 0, 0, 1);
                Node n3 = new Node(3, 0, 0, 1);
                Node n4 = new Node(4, 0, 0, 1);
                Node n5 = new Node(5, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);
                g.InsertNewVertex(n4);
                g.InsertNewVertex(n5);

                n1.addAdjacentNode(new Dictionary<Node, double>() { { n2, 4 }, { n3, 2 } });
                n2.addAdjacentNode(new Dictionary<Node, double>() { { n3, 3 }, { n5, 3 }, { n4, 2 } });
                n3.addAdjacentNode(new Dictionary<Node, double>() { { n4, 4 }, { n5, 5 }, { n2, 1 } });
                n4.addAdjacentNode(new Dictionary<Node, double>() { });
                n5.addAdjacentNode(new Dictionary<Node, double>() { { n4, 1 } });

                List<Node> result = new List<Node>() { n4, n2, n3 };

                Assert.Equal(result, g.shortest_path(n1, n4));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Fact]
        public void BFSTestTwoNodes()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new Node(1, 0, 0, 1);
                Node n2 = new Node(2, 0, 0, 1);
                Node n3 = new Node(3, 0, 0, 1);
                Node n4 = new Node(4, 0, 0, 1);
                Node n5 = new Node(5, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);
                g.InsertNewVertex(n4);
                g.InsertNewVertex(n5);
                n1.addAdjacentNode(new Dictionary<Node, double>() { { n2, 4 }, { n3, 2 } });
                n2.addAdjacentNode(new Dictionary<Node, double>() { { n3, 3 }, { n5, 3 }, { n4, 2 } });
                n3.addAdjacentNode(new Dictionary<Node, double>() { { n4, 4 }, { n5, 5 }, { n2, 1 } });
                n4.addAdjacentNode(new Dictionary<Node, double>() { });
                n5.addAdjacentNode(new Dictionary<Node, double>() { { n4, 1 } });
                Assert.Equal(n4, g.BFS(n1, n4));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Fact()]
        public void BFSTestOneNodeIsVisited()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new Node(1, 0, 0, 1);
                Node n2 = new Node(2, 0, 0, 1);
                Node n3 = new Node(3, 0, 0, 1);
                Node n4 = new Node(4, 0, 0, 1);
                Node n5 = new Node(5, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);
                g.InsertNewVertex(n4);
                g.InsertNewVertex(n5);
                n1.addAdjacentNode(new Dictionary<Node, double>() {{n2, 4}, {n3, 2}});
                n2.addAdjacentNode(new Dictionary<Node, double>() {{n3, 3}, {n5, 3}, {n4, 2}});
                n3.addAdjacentNode(new Dictionary<Node, double>() {{n4, 4}, {n5, 5}, {n2, 1}});
                n4.addAdjacentNode(new Dictionary<Node, double>() {});
                n5.addAdjacentNode(new Dictionary<Node, double>() {{n4, 1}});
                g.BFS(n1);
                State status = n4.getState();
                Assert.Equal(State.Visited, status);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }
    }
}