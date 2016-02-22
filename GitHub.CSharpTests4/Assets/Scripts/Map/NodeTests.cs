using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class NodeTests
    {
        [Fact()]
        public void hasAdjacentNodeTest()
        {
            try
            {
                Graph g = new Graph();
                Node n1 = new Node(1, 0, 0, 1);
                Node n2 = new Node(2, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);

                n1.addAdjacentNode(new Dictionary<Node, double>() {{n2, 4}});
                n2.addAdjacentNode(new Dictionary<Node, double>() {{n1, 3}});
                Assert.True(n1.hasAdjacentNode(n2),
                    "This tests if hasAdjacentNode returns true if a node is adjacent to a given one.");
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }
    }
}