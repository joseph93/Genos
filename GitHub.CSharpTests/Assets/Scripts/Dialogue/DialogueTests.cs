using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using SunCubeStudio.Localization;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace Assets.Scripts
{
    [TestFixture()]
    public class DialogueTests
    {

        [Test]
        public void IsNullOrEmptyTest()
        {
            try
            {
                string key1 = "button1";
                string key2 = "button2";

                //Check if strings are not empty before inserting in the content box
                Assert.Equals(true, string.IsNullOrEmpty(key1));
                Assert.Equals(true, string.IsNullOrEmpty(key2));
            }
            catch (SecurityException e)
            {
                Console.WriteLine("Security Exception:\n\n{0}", e.Message);
            }
        }

    }
}