using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Start
    {
        public static void Main()
        {
            Graph gDirect = new Graph();

            gDirect.InsertDirectEdge(2, 7, 2);
            gDirect.InsertDirectEdge(7, 4, 4);
            gDirect.InsertDirectEdge(4, 6, 5);
            gDirect.InsertDirectEdge(8, 3, 5);
            gDirect.InsertDirectEdge(6, 2, 4);

            gDirect.ToString();

            Console.WriteLine("Next vertex is {0}");
        }
    }
}
