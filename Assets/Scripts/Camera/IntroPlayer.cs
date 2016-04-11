using UnityEngine;
using System.Collections;

public class IntroPlayer : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(startVideo());
	}

    public IEnumerator startVideo()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        Handheld.PlayFullScreenMovie("MOEB_Introduction_-_Small.mp4", Color.black, FullScreenMovieControlMode.Full);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Screen.orientation = ScreenOrientation.Portrait;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
