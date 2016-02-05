using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TourButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeSceneToBeacon()
    {
        SceneManager.LoadScene("Search iBeacon");
    }
}
