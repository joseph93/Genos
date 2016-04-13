using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class PopUpWindow : MonoBehaviour {

    public Text description;
    public Image iconImage;
    public Button viewButton;
    public GameObject modalPanelObject;

    private static PopUpWindow popUpWindow;

    public static PopUpWindow Instance()
    {
        if (!popUpWindow)
        {
            popUpWindow = FindObjectOfType(typeof(PopUpWindow)) as PopUpWindow;
            if (!popUpWindow)
                Debug.Log("There needs to be one active ModalWindow script on a GameObject in your scene.");
        }

        return popUpWindow;

    }

    //JOSEPH: Ok/Cancel : A string, an image, OK event, Cancel event
    public void PopUp(string descr, UnityAction viewEvent)
    {
        modalPanelObject.SetActive(true);

        viewButton.onClick.RemoveAllListeners();
        viewButton.onClick.AddListener(viewEvent);
        viewButton.onClick.AddListener(closePanel);
        
        description.text = descr;

        viewButton.gameObject.SetActive(true);

        /*yield return new WaitForSeconds(3.0f);

        closePanel();*/
    }

    public void closePanel()
    {
        modalPanelObject.SetActive(false);
    }
}
