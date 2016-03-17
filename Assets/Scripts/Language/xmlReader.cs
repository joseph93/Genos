using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Assets.Scripts.Language
{
   public class xmlReader : MonoBehaviour
   {
       public TextAsset dictionary; //xmlLanguages.xml
       public string languageName;  //string for current language in editor
       public static int currentLanguage;  //input int for current language in editor

       string button1;
       string button2;

       List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
       Dictionary<string, string> obj; //eg. button1, "Play"

        void Awake()
       {
           Reader();
       }

       void Update()
       {

           languages[currentLanguage].TryGetValue("Name", out languageName);
           languages[currentLanguage].TryGetValue("button1", out button1);
           languages[currentLanguage].TryGetValue("button2", out button2);

        }

        void OnGUI()
       {
           GUILayout.Button(button1);
           GUILayout.Button(button2);

 /*           if (GUILayout.Button("English"))
            {
                currentLanguage = 0;
            }

            else if (GUILayout.Button("Francais"))
            {
                currentLanguage = 1;
            }
*/
       }

       void Reader()
       {
           XmlDocument xmlDoc = new XmlDocument(); 
            xmlDoc.LoadXml(dictionary.text); 
            XmlNodeList languagesList = xmlDoc.GetElementsByTagName("language"); 


           foreach (XmlNode languageValue in languagesList) //in the xml file, every pair of <language>...</language> is a node
            {
               XmlNodeList languageContent = languageValue.ChildNodes;
               obj = new Dictionary<string, string>();

               foreach (XmlNode value in languageContent) //in the xml file, every sub tag of <language>...</language> is a child node
               {
                   if (value.Name == "Name")
                   obj.Add(value.Name,value.InnerText);

                   if(value.Name == "button1")
                   obj.Add(value.Name,value.InnerText);

                   if(value.Name == "button2")
                   obj.Add(value.Name, value.InnerText);
               }
               languages.Add(obj);
           }
       }

   }

}
