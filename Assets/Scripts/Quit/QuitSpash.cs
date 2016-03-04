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
        //changed scene F2 to F2-next
        SceneManager.LoadScene("F2-next");
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
