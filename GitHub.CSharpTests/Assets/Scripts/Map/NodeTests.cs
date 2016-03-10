using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [TestFixture()]
    public class NodeTests
    {
        
            [Test]
            public void hasAdjacentNodeTest()
            { 
                try {
            
                    Graph g = new Graph();
                    Node n1 = new Node(1, 0, 0, 1);
                    Node n2 = new Node(2, 0, 0, 1);
                    Node n3 = new Node(3, 0, 0, 1);

                    g.InsertNewVertex(n1);
                    g.InsertNewVertex(n2);
                    g.InsertNewVertex(n3);

                    n1.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n2, 4}});
                    n2.addListOfAdjacentNodes(new Dictionary<Node, float>() {{n1, 3}});
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