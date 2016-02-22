using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class TouchScreenHandlingTest {

    [Test]
    //Verify the size of the zoom
    public void CalculateLevelBoundsTest()
    {
        //Arrange
        //Width is currently set smaller than the height
        float mapWidth = 30.0f;
        float mapHeight = 20.0f;
        Camera myCamera;
        float minX, maxX, minY, maxY;
        float horizontalExtent, verticalExtent;
        Vector2 scrollDirection = Vector2.zero;

        //Act
        //Initialize the camera
        myCamera = Camera.main;
      
        //Calculate the size of each zoom axes
        verticalExtent = myCamera.orthographicSize;
        horizontalExtent = myCamera.orthographicSize * Screen.width / Screen.height;
        minX = horizontalExtent - mapWidth / 2.0f;
        maxX = mapWidth / 2.0f - horizontalExtent;
        minY = verticalExtent - mapHeight / 2.0f;
        maxY = mapHeight / 2.0f - verticalExtent;

        //Assert
        //Check if zoom min is smaller than the zoom max
        Assert.GreaterOrEqual(maxY, minY);
        Assert.GreaterOrEqual(maxX, minX);
    }
}
