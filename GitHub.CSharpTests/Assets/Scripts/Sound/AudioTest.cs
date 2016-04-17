using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI; // needed for the toggle button
using UnityEngine.SceneManagement;
using System.Security;
using System;

public class AudioTest {

    [Test]
    public void SoundCompareTest()
    {
        try
        {
            var audio1 = new GameObject();
            var audio2 = "fx sound";
            SoundManager audio3 = new SoundManager();

            audio1.SetActive(true);
            Camera myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            audio1.tag = audio2;

            Assert.AreEqual(audio1, audio2);
        }
        catch (SecurityException e)
        {
            Console.WriteLine("Security Exception: \n\n{0}", e.Message);
        }
    }

    [Test]
    public void SoundDisableTest()
    {
        try
        {
            GameObject sound = new GameObject();
            bool muteToggle = false;
            Camera myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            myCamera.gameObject.GetComponent<AudioListener>().enabled = false;

            Assert.IsNotNull(sound);
            //Assert.AreEqual(myCamera, sound.SoundDisable());
        }
        catch (SecurityException e)
        {
            Console.WriteLine("Security Exception: \n\n{0}", e.Message);
        }
    }

    [Test]
    public void SoundEnableTest()
    {
        try
        {
            GameObject sound = new GameObject();
            bool muteToggle = true;
            Camera myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            myCamera.gameObject.GetComponent<AudioListener>().enabled = true;

            Assert.IsNotNull(sound);
            //Assert.AreEqual(myCamera, sound.SoundEnable());
        }
        catch (SecurityException e)
        {
            Console.WriteLine("Security Exception: \n\n{0}", e.Message);
        }
    }

}
