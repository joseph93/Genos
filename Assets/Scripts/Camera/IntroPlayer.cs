using UnityEngine;
using System.Collections;

public class IntroPlayer : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    Handheld.PlayFullScreenMovie("MOEB_Introduction_-_Small.mp4", Color.black, FullScreenMovieControlMode.Hidden);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
