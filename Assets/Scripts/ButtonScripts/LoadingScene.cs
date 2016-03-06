using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


//Ihcene: This loads the main screen automatically
public class LoadingScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(loadingScreen());
	}

    IEnumerator loadingScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
