using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Text;

namespace Assets.Scripts.Language
{
   public class xmlReader : MonoBehaviour
   {
       public TextAsset dictionary;
       public string languageName;
       public int currentLanguage;

       string button1;
       string button2;

       List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
       Dictionary<string, string> obj;

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
       }
       void Reader()
       {
           XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(dictionary.text);
           XmlNodeList languagesList = xmlDoc.GetElementsByTagName("language");


           foreach (XmlNode languageValue in languagesList)
           {
               XmlNodeList languageContent = languageValue.ChildNodes;
               obj = new Dictionary<string, string>();

               foreach (XmlNode value in languageContent)
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
=======
using System.Text;
using System.IO;
using System.Xml;

namespace Assets.Scripts.Language
{
    public class xmlReader : MonoBehaviour
    {
        public TextAsset dictionary;
        public string languageName;
        public int currentLanguage;

        string button1;
        string button2;

        List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
        Dictionary<string, string> obj;

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
        }
        void Reader()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(dictionary.text);
            XmlNodeList languagesList = xmlDoc.GetElementsByTagName("language");


            foreach (XmlNode languageValue in languagesList)
            {
                XmlNodeList languageContent = languageValue.ChildNodes;
                obj = new Dictionary<string, string>();

                foreach (XmlNode value in languageContent)
                {
                    if (value.Name == "Name")
                        obj.Add(value.Name, value.InnerText);

                    if (value.Name == "button1")
                        obj.Add(value.Name, value.InnerText);

                    if (value.Name == "button2")
                        obj.Add(value.Name, value.InnerText);
                }
                languages.Add(obj);
            }
        }

    }
>>>>>>> d867dcece733ea2d5a5460f7812df6c352932efe
}
