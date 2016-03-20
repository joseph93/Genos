using Assets.Scripts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Language;

namespace Assets.Scripts
{
    [TestFixture()]
    public class NodeTests
    {
        [Test()]
        public void setTitleAndSummaryTest()
        {
            try
            {
                PointOfInterest p1 = new PointOfInterest(0, 0, 0, 1, null);
                p1.setTitleAndSummary("test", "test");
                Assert.IsNotNull(p1.GetPoiDescription());
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void CompareToTest()
        {
            try
            {
                StoryPoint sp1 = new StoryPoint(1, 0, 0, 2, new PoiDescription("test", "test"), 1);
                StoryPoint sp2 = new StoryPoint(1, 0, 0, 2, new PoiDescription("test", "test"), 3);

                Assert.Equals(1, sp1.CompareTo(sp2));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void SortTest()
        {
            try
            {
                StoryPoint sp1 = new StoryPoint(1, 0, 0, 2, new PoiDescription("test", "test"), 1);
                StoryPoint sp2 = new StoryPoint(1, 0, 0, 2, new PoiDescription("test", "test"), 3);
                List<StoryPoint> unsortedList = new List<StoryPoint>();
                unsortedList.Add(sp2);
                unsortedList.Add(sp1);
                List<StoryPoint> sortedList = new List<StoryPoint>();
                sortedList.Add(sp1);
                sortedList.Add(sp2);
                unsortedList.Sort();

                Assert.Equals(sortedList, unsortedList);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test]
        public void hasAdjacentNodeTest()
        {
            try
            {

                Graph g = new Graph();
                Node n1 = new Node(1, 0, 0, 1);
                Node n2 = new Node(2, 0, 0, 1);
                Node n3 = new Node(3, 0, 0, 1);

                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);
                g.InsertNewVertex(n3);

                n1.addListOfAdjacentNodes(new Dictionary<Node, float>() { { n2, 4 } });
                n2.addListOfAdjacentNodes(new Dictionary<Node, float>() { { n1, 3 } });
                Assert.IsNotNull(g);
                Assert.IsNotNull(n1);
                Assert.IsNotNull(n2);
                Assert.IsNotNull(n3);
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