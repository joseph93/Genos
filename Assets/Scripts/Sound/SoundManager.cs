using UnityEngine;
using System.Collections;
using UnityEngine.UI; // needed for the toggle button
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    /*
        The method below is to be used with a toggle button on an onClick() event.
    */

    //turn on/off sound with a toggle
    public void toggleButtonSound()
    {
       
        if(GetComponent<Toggle>().isOn == false) // toggle is checked meaning sound is on
        {
         
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
        }
        else if (GetComponent<Toggle>().isOn == true) //toggle is unchecked meaning sound if off
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
       
        Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;

    }

    //turn on button function when clicked
    public void SoundEnable()
    {
      
        Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
       

    }
}
