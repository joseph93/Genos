using UnityEngine;
using System.Collections;

using UnityEngine.UI;


public class JsonDecode : MonoBehaviour {

    public Image myImage;

    public Sprite someOtherSprite;

    public void ChangeImage(string newImageTitle)
    {
        myImage.sprite = Resources.Load<Sprite>(newImageTitle);
    }

    // Use this for initialization
    void Start() {

        decode();
    }

    // Update is called once per frame
    void Update() {

    }

    public void decode()
    {
        // string encodedString = "{hey: [1,2,3]}";

  string encodedString =      " {floorPlan: [ { floorID: 2,imagePath: /floor/2/floor2.png,imageWidth: 1870,imageHeight: 3925}]} ";

        //value->key

        /*
        PLACER LE SPRITE DANS RESOURCE pour que ca fonctionne
        string picture = tag "imagePath", get me its value!!
        GetComponent<SpriteRenderer>().sprite =  Resources.Load<Sprite>(picture);


        POI poiAttributes = tag "POI", get me its value dans un ARRAY.
        poiAttributes[0] -> id
        poiAttributes[1] -> title
        ->x
        ->y
        ->color
        ->description
        ->floorID
        ->ibeacon
        ->media
        */

        JSONObject j = new JSONObject(encodedString);
        accessData(j);
       
        }
    void accessData(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    Debug.Log(key);
                    accessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in obj.list)
                {
                    accessData(j);
                }
                break;
            case JSONObject.Type.STRING:
                Debug.Log(obj.str);
                break;
            case JSONObject.Type.NUMBER:
                Debug.Log(obj.n);
                break;
            case JSONObject.Type.BOOL:
                Debug.Log(obj.b);
                break;
            case JSONObject.Type.NULL:
                Debug.Log("NULL");
                break;

        }

    }
}
