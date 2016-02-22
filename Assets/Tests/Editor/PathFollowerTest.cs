using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PathFollowerTest {

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
    public void Update()
    {      Transform[] path;
           float speed = 5.0f;
           float reachDist = 1.0f;
           int currentPoint = 0;
        /*   float dist = Vector3.Distance(path[currentPoint].position, transform.position);

           transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, Time.deltaTime * speed);

        //Nipper goes to next point
        if (dist <= reachDist)
            currentPoint++;
            */
        /*      
        //Nipper goes back to initial point
                if (currentPoint >= path.Length)
                    currentPoint = 0;
        */
    }
}
