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
                StoryPoint sp1 = new StoryPoint(0, 0, 0, 2, new PoiDescription("test", "test"), 1, 0);
                StoryPoint sp2 = new StoryPoint(0, 0, 0, 2, new PoiDescription("test", "test"), 2, 0);
                List<StoryPoint> spList = new List<StoryPoint>();
                spList.Add(sp1);
                spList.Add(sp2);
                Storyline np = new Storyline(0, new StorylineDescription("test", "test"), 4);
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
                StoryPoint sp1 = new StoryPoint(0, 0, 0, 2, new PoiDescription("test", "test"), 1, 0);
                StoryPoint sp2 = new StoryPoint(0, 0, 0, 2, new PoiDescription("test", "test"), 2, 0);
                List<StoryPoint> spList = new List<StoryPoint>();
                spList.Add(sp1);
                spList.Add(sp2);

                List<StoryPoint> visitedSpList = new List<StoryPoint>();
                visitedSpList.Add(sp1);

                Storyline np = new Storyline(0, new StorylineDescription("test", "test"), 4);
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
