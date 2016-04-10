using NUnit.Framework;
using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Language;

namespace Assets.Scripts
{
    [TestFixture()]
    public class StorylineTest
    {
        [Test()]
        public void isInOrderTest()
        {
            try
            {
                POS sp1 = new POS(0, 0, 0, "blue", 2, 0);
                POS sp2 = new POS(0, 0, 0, "blue", 2,  0);
                List<POS> spList = new List<POS>();
                spList.Add(sp1);
                spList.Add(sp2);
                Storyline np = new Storyline(0, 4, "test", "test");
                np.setStorypointList(spList);

                Assert.True(np.isInOrder(sp1));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

        [Test()]
        public void findLastUnvisitedSpTest()
        {
            try
            {
                POS sp1 = new POS(0, 0, 0, "blue", 2, 0);
                POS sp2 = new POS(0, 0, 0, "blue", 2, 0);
                List<POS> spList = new List<POS>();
                spList.Add(sp1);
                spList.Add(sp2);

                List<POS> visitedSpList = new List<POS>();
                visitedSpList.Add(sp1);

                Storyline np = new Storyline(0, 4, "test", "test");
                np.setStorypointList(spList);
                np.setVisitedStorypointList(visitedSpList);

                Assert.IsNotNull(sp1);
                Assert.IsNotNull(sp2);
                Assert.IsNotNull(np);
                Assert.IsNotNull(spList);
                Assert.IsNotNull(visitedSpList);
                Assert.Equals(sp1, np.findLastUnvisitedSp());
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }
    }
}
