using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class TouchScreenHandlingTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
        var gameObject = new GameObject();

        //Act
        //Try to rename the GameObject
        var newGameObjectName = "My game object";
        gameObject.name = newGameObjectName;

        //Assert
        //The object has a new name
        Assert.AreEqual(newGameObjectName, gameObject.name);
    }

    [Test]
    public void CalculateLevelBoundsTest()
    {
        float mapWidth = 30.0f;
        float mapHeight = 20.0f;
        Camera myCamera;
        float minX, maxX, minY, maxY;
        float horizontalExtent, verticalExtent;
        Vector2 scrollDirection = Vector2.zero;

        myCamera = Camera.main;
        verticalExtent = myCamera.orthographicSize;
        horizontalExtent = myCamera.orthographicSize * Screen.width / Screen.height;
        minX = horizontalExtent - mapWidth / 2.0f;
        maxX = mapWidth / 2.0f - horizontalExtent;
        minY = verticalExtent - mapHeight / 2.0f;
        maxY = mapHeight / 2.0f - verticalExtent;
    }
}
