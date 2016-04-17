using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Security;
using System.Collections;
using UnityEngine.UI; // needed for the toggle button
using UnityEngine.SceneManagement;
using System;

/*
public class CameraLevelBoundsTest {

    [Test]
    public void CameraTest()
    {
        try
        {
            var camera = new GameObject();
            var newCamera = "Main Camera";
            camera.name = newCamera;

            Assert.AreEqual(camera, newCamera);

        }
        catch (SecurityException e)
        {
            Console.WriteLine("Security Exception: \n\n{0}", e.Message);
        }
    }

    [Test]
    public void CalculateLevelBoundsTest()
    {
        try
        {
            private Camera myCamera;

            float minZoom = 1.0f;
            float maxZoom = 5.0f;
            bool invertMoveX = false;
            bool invertMoveY = false;
            float mapWidth = 30.0f;
            float mapHeight = 20.0f;

            float minX = 1f, maxX = 10f, minY = 1f, maxY = 10f;
            float horizontalExtent = 1f, verticalExtent = 1f;

            minX = horizontalExtent - mapWidth / 2.0f;
			maxX = mapWidth / 2.0f - horizontalExtent;
			minY = verticalExtent - mapHeight / 2.0f;
			maxY = mapHeight / 2.0f - verticalExtent;
		
			Assert.Equals(CalculateLevelBounds().minX, minX);
       
       }
        catch (SecurityException e)
        {
            Console.WriteLine("Security Exception: \n\n{0}", e.Message);
        }
    }

    [Test]
	public void LateUpdateTest()
    {
        try
        {

            private Camera myCamera;

            public float minZoom = 1.0f;
            public float maxZoom = 5.0f;
            public bool invertMoveX = false;
            public bool invertMoveY = false;
            public float mapWidth = 30.0f;
            public float mapHeight = 20.0f;

            private float minX, maxX, minY, maxY;
            private float horizontalExtent, verticalExtent;

            Vector3 limitedCameraPosition = myCamera.transform.position;
            limitedCameraPosition.x = Mathf.Clamp(limitedCameraPosition.x, minX, maxX);
			limitedCameraPosition.y = Mathf.Clamp(limitedCameraPosition.y, minY, maxY);
			myCamera.transform.position = limitedCameraPosition;
			
			Assert.True(LateUpdate());
		}
		catch(SecurityException e)
		{
			Console.WriteLine("Security Exception: \n\n{0}", e.Message);
		}
	
	} 
}
*/