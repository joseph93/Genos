using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [TestFixture()]
    public class GraphTests
    {
        [Test]
        public void shortest_pathTest()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);
                Node n3 = new PointOfInterest(3, 0, 0, 1);
                Node n4 = new PointOfInterest(4, 0, 0, 1);
                Node n5 = new PointOfInterest(5, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);
                g.InsertNewVertex(n4);
                g.InsertNewVertex(n5);

                n1.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n2, 4}, {n3, 2}});
                n2.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n3, 3}, {n5, 3}, {n4, 2}});
                n3.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n4, 4}, {n5, 5}, {n2, 1}});
                n4.addListOfAdjacentNodes(new Dictionary<Node, float>() {});
                n5.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n4, 1}});

                List<Node> result = new List<Node>() {n4, n2, n3};

                Assert.IsNotNull(g);
                Assert.IsNotNull(n1);
                Assert.IsNotNull(n2);
                Assert.IsNotNull(n3);
                Assert.IsNotNull(n4);
                Assert.IsNotNull(n5);
                Assert.IsNotNull(result);
                Assert.Equals(result, g.shortest_path(n1, n4));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test]
        public void BFSTestTwoNodes()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);
                Node n3 = new PointOfInterest(3, 0, 0, 1);
                Node n4 = new PointOfInterest(4, 0, 0, 1);
                Node n5 = new PointOfInterest(5, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);
                g.InsertNewVertex(n4);
                g.InsertNewVertex(n5);
                n1.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n2, 4}, {n3, 2}});
                n2.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n3, 3}, {n5, 3}, {n4, 2}});
                n3.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n4, 4}, {n5, 5}, {n2, 1}});
                n4.addListOfAdjacentNodes(new Dictionary<Node, float>() {});
                n5.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n4, 1}});

                Assert.IsNotNull(g);
                Assert.IsNotNull(n1);
                Assert.IsNotNull(n2);
                Assert.IsNotNull(n3);
                Assert.IsNotNull(n4);
                Assert.IsNotNull(n5);
                Assert.Equals(n4, g.BFS(n1, n4));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test]
        public void BFSTestOneNodeIsVisited()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);
                Node n3 = new PointOfInterest(3, 0, 0, 1);
                Node n4 = new PointOfInterest(4, 0, 0, 1);
                Node n5 = new PointOfInterest(5, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);
                g.InsertNewVertex(n4);
                g.InsertNewVertex(n5);
                n1.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n2, 4}, {n3, 2}});
                n2.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n3, 3}, {n5, 3}, {n4, 2}});
                n3.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n4, 4}, {n5, 5}, {n2, 1}});
                n4.addListOfAdjacentNodes(new Dictionary<Node, float>() {});
                n5.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n4, 1}});
                g.BFS(n1);
                State status = n4.getState();

                Assert.IsNotNull(g);
                Assert.IsNotNull(n1);
                Assert.IsNotNull(n2);
                Assert.IsNotNull(n3);
                Assert.IsNotNull(n4);
                Assert.IsNotNull(n5);
                Assert.IsNotNull(status);
                Assert.Equals(State.Visited, status);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test]
        public void BFSTestOneNodeVerticesIsNull()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                State status = n1.getState();

                Assert.IsNotNull(g);
                Assert.IsNotNull(n1);
                Assert.IsNotNull(status);
                Assert.Equals(State.UnVisited, status);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test]
        public void BFSTestOneNodeNoPath()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                g.InsertNewVertex(n1);

                State status = n1.getState();

                Assert.Equals(State.UnVisited, status);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test]
        public void BFSTestTwoNodesVerticesIsNull()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);

                Assert.Equals(null, g.BFS(n1, n2));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test]
        public void BFSTestTwoNodesNoPath()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);

                Assert.Equals(null, g.BFS(n1, n2));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void shortest_pathTestIsNull()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);

                Assert.Equals(null, g.shortest_path(n1, n2));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void shortest_pathTestNoPath()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0,  1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);
                Node n3 = new PointOfInterest(3, 0, 0,  1);
                Node n4 = new PointOfInterest(4, 0, 0,  1);
                Node n5 = new PointOfInterest(5, 0, 0,  1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);
                g.InsertNewVertex(n4);
                g.InsertNewVertex(n5);

                n1.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n2, 4}, {n3, 2}});
                n2.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n3, 3}, {n4, 2}});
                n3.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n4, 4}, {n2, 1}});
                n4.addListOfAdjacentNodes(new Dictionary<Node, float>() {});
                n5.addListOfAdjacentNodes(new Dictionary<Node, float>() {});

                Assert.IsNotNull(g);
                Assert.IsNotNull(n1);
                Assert.IsNotNull(n2);
                Assert.IsNotNull(n3);
                Assert.IsNotNull(n4);
                Assert.IsNotNull(n5);
                Assert.Equals(null, g.shortest_path(n1, n5));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void FindByKeyTest()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0,  1);
                Node n2 = new PointOfInterest(2, 0, 0,  1);
                Node n3 = new PointOfInterest(3, 0, 0,  1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);

                Assert.Equals(n2, g.FindByKey(2));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void ExistKeyTest()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);

                Assert.True(false);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void shortest_pathTestWithOneNode()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);

                List<Node> nodelist = new List<Node>();
                nodelist.Add(n1);
                nodelist.Add(n2);

                g.InsertNewVertex(n1);
                n1.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n1, 4}});

                Assert.Equals(nodelist, g.shortest_path(n1, n1));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }
    }
}
