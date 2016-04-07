using NUnit.Framework;
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
    public class iBeaconServerTest
    {
        [Test()]
        public void EqualsTest()
        {
            try
            {
                iBeaconServer beacon1 = new iBeaconServer("test", 10, 10);
                Beacon beacon2 = new Beacon("test", 10, 10, 10, 10, 20.00, 10);

                Assert.IsNotNull(beacon1);
                Assert.IsNotNull(beacon2);
                Assert.True(beacon1.Equals(beacon2));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }
    }
}
