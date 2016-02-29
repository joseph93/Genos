using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitSpash : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
           
    }

    public void OnClickYes()
    {
        Application.Quit();
    }

    public void OnClickNo()
    {
        Application.CancelQuit();
        SceneManager.LoadScene("F2");
    }
    public void OnclickNo_Storyline()
    {
        Application.CancelQuit();
        SceneManager.LoadScene("ListOfStorylines");
    }

    public void changeSceneToQuit()
    {
        SceneManager.LoadScene("Quit");
    }


}
