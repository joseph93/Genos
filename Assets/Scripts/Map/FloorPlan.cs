using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts
{
    public class FloorPlan : MonoBehaviour
    {
        private string prefix = "http://localhost";
        public string floorNumber { get; set; }
        public string imagePath { get; set; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }
        private Texture2D tex;

        //Added
        public Scrollbar progressbar;
        public Text displayedtext;
        public GameObject progress;

        static WWW objSERVER;
        static string pathURL;
        static string pathLOCAL;



        public FloorPlan(string fn, string ip, int iw, int ih)
        {
            floorNumber = fn;
            imagePath = prefix + ip;
            imageWidth = iw;
            imageHeight = ih;
        }

        public int getImageWidth()
        {
            return imageWidth;
        }

        public int getImageHeight()
        {
            return imageHeight;
        }

        void Start()
        {
            StartCoroutine(LoadImgIntoTxture());
        }

        public IEnumerator LoadImgIntoTxture()
        {
            /*            yield return 0;
                        WWW www = new WWW("http://localhost/floor/3/floor3.png");
                        yield return www;
                        tex = www.texture;
                        GetComponent<RawImage>().texture = tex;
                        //GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0,0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));

            */


            //location of the file on the server
            pathURL = "http://mowbray.tech/exposeum/";

            //location of the file on the device
            pathLOCAL = Application.persistentDataPath + "/../assetbundles/" + "test" + ".unity3d";

            objSERVER = new WWW(pathURL);

            // Wait for download to finish
            yield return objSERVER;

            //Save it to disk
            SaveDownloadedAsset(objSERVER);
            Debug.Log("path" + Application.persistentDataPath);
            AssetBundle objLOCAL = AssetBundle.LoadFromFile(pathLOCAL);
            yield return objLOCAL;

            //Instantiate the asset bundle
            GameObject Model3D = Instantiate(objLOCAL.mainAsset) as GameObject;

            objLOCAL.Unload(false);
        
            

        }   

        public void SaveDownloadedAsset(WWW objSERVER)
        {
            // Create the directory if it doesn't already exist
            if (!Directory.Exists(Application.persistentDataPath + "/../assetbundles/"))
            {
                    Directory.CreateDirectory(Application.persistentDataPath + "/../assetbundles/");
            }

            // Initialize the byte string
            byte[] bytes = objSERVER.bytes;

            // Creates a new file, writes the specified byte array to the file, and then closes the file. 
            // If the target file already exists, it is overwritten.
            File.WriteAllBytes(pathLOCAL, bytes);
        }







    }
}
