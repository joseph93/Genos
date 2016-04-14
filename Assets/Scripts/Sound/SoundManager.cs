using UnityEngine;
using System.Collections;
using UnityEngine.UI; // needed for the toggle button
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
    private bool muteToggle = false;
    /*
    public GameObject audioSource;
    bool soundToggle = true;


 public void OnGUI()
    {
        soundToggle = !soundToggle;
        if (soundToggle)
        {
            audioSource.SetActive(true);
            //audioSource.mute = true;
            //audioSource.volume = 1.0f;
        }
        else
        {
            audioSource.SetActive(false);
            //audioSource.mute = false;
            //audioSource.volume = 0.0f;
        }
    }

*/



    /*
        The method below is to be used with a toggle button on an onClick() event.
    */

    //turn on/off sound with a toggle
    public void toggleButtonSound()
    {
       
        if(GetComponent<Toggle>().isOn == false && GetComponent<AudioSource>().mute == true) // toggle is checked meaning sound is on
        {
            
             Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
        }
        else if (GetComponent<Toggle>().isOn == true && GetComponent<AudioSource>().mute == false) //toggle is unchecked meaning sound is off
        {
            
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
        }
    }


    /*
        The two method below are to be used with a button on an onClick() event.
    */

    //turn off button function when clicked
    public void SoundDisable()
    {
        if (GetComponent<AudioSource>().mute == true)//(muteToggle == true)//(AudioListener.pause == true)
        {
            Camera myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            myCamera.gameObject.GetComponent<AudioListener>().enabled = false;
        }
        //AudioListener.volume = 0f;
        //Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;

    }

    //turn on button function when clicked
    public void SoundEnable()
    {
        if (GetComponent<AudioSource>().mute == false)//(muteToggle == false)//(AudioListener.pause == false)
        {
            Camera myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            myCamera.gameObject.GetComponent<AudioListener>().enabled = true;
            // AudioListener.volume = 1f;
            //Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
        }

    }
}
