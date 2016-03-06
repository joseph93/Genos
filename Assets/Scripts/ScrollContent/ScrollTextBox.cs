using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;


public class ScrollTextBox : MonoBehaviour {

  //  public string txtFile = "Intro";
  //  string txtContents;
  public string textIntro = "Introduction (as audio/text) During the tour, visitors will walk through different sections of RCA Victor’s production site, constructed over a period of roughly 25 years.The tour leads them through three different time zones, the 1920s, back when Montreal was the world’s largest grain hub and Canada’s productive power house.This was also Emile Berliner’s time with the production of early recordings and record playing equipment, Montreal’s entertainment rich 1930s, when music was still at the core of RCA Victor’s production, to 1943, when production at RCA Victor diversified to serve military needs. Montreal was Canada’s most industrialized metropolis and 1943 in the middle of war production to support the allies fighting in Europe.Economy boomed and the RCA’s workforce quickly increased from around 300 to over 3.000 workers.Not only here, but in all kinds of industry, women needed to fill in formerly male positions.Military guarded industrial sites that produced for the armies. Changes came quickly, technologies advanced fast.The visitor experiences how much the working life and production changed in this factory.Together with the famous dog Nipper, they explore the core of the RCA-Victor plant, going through today’s spaces where they encounter 10 places that give them an audio-visual experience of the atmosphere of this place’s past that once played a paramount role in Montreal.";

   void Awake()
    {
      //  TextAsset txtAssets = (TextAsset)Resources.Load(txtFile);
      //  txtContents = txtAssets.text;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine("AutoType");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator AutoType()
    {
        foreach(char letter in textIntro.ToCharArray())
        {
         //   guiText.text += letter; //deprecated
            GetComponent<GUIText>().text += letter;
            yield return new WaitForSeconds(0.3f);
        }
    }

/*    public void OnGUI()
    {
        GUILayout.Label(txtContents);

    }
    */
}
