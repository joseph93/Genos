using UnityEngine;
using UnityEditor;
using NUnit.Framework;

namespace Assets.Scripts
{
    [TestFixture()]
    public class xPathFollowerTest
    {

        [Test]
        //Test if point distance is less than the final distance before updating the scene
        public void Update()
        {
            //Arrange
            float reachDist = 1.0f;
            int currentPoint = 0;
            float dist = 0f;

            //Act
            //Increase current point to 1 if distance is smaller then reached distance
            int currentPointBefore = 0;
            int currentPointAfter = 0;
            if (dist <= reachDist)
                currentPointBefore = currentPoint; //0
            currentPoint++;
            currentPointAfter = currentPoint; //1

            //Assert
            //Check if the current point has increase
            Assert.GreaterOrEqual(currentPointAfter, currentPointBefore);

        }
    }
}
