﻿using NUnit.Framework;
using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [TestFixture()]
    public class MapTest
    {
        [Test()]
        public void addNodeTest()
        {
            try
            {
                Map map = new Map();
                Graph g = new Graph();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);

                List<Node> nodeList = new List<Node>();

                nodeList.Add(n1);
                nodeList.Add(n2);
                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);

                map.addNode(n1);
                map.addNode(n2);

                Graph mapGraph = map.getGraph();
                List<Node> mapNodes = map.GetPoiNodes();

                Assert.IsNotNull(nodeList);
                Assert.IsNotNull(g);
                Assert.IsNotNull(mapGraph);
                Assert.IsNotNull(mapNodes);

                Assert.Equals(nodeList, mapNodes);
                Assert.Equals(g, mapGraph);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void initializeGraphTest()
        {
            try
            {
                Map map = new Map();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);

                List<Node> nodeList = new List<Node>();

                nodeList.Add(n1);
                nodeList.Add(n2);

                map.setPoiList(nodeList);

                Graph g = new Graph();
                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);

                map.initializeGraph(nodeList);
                Graph mapGraph = map.getGraph();

                Assert.IsNotNull(nodeList);
                Assert.IsNotNull(g);
                Assert.IsNotNull(mapGraph);

                Assert.Equals(g, mapGraph);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void addNodeListTest()
        {
            try
            {
                Map map = new Map();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);

                List<Node> nodeList = new List<Node>();

                nodeList.Add(n1);
                nodeList.Add(n2);

                map.setPoiList(nodeList);

                List<Node> mapNodes = map.GetPoiNodes();

                Assert.IsNotNull(nodeList);
                Assert.IsNotNull(mapNodes);
                Assert.Equals(nodeList, mapNodes);
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void getGraphTest()
        {
            try
            {
                Map map = new Map();
                Node n1 = new PointOfInterest(1, 0, 0, 1);
                Node n2 = new PointOfInterest(2, 0, 0, 1);
                map.addNode(n1);
                map.addNode(n2);

                Graph g = new Graph();
                g.InsertNewVertex(n1);
                g.InsertNewVertex(n2);

                Assert.Equals(g, map.getGraph());
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }
    }
}

