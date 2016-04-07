using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class WarningPanel : MonoBehaviour
{

    public Text description;
    public Image iconImage;
    public Button okButton;
    public Button cancelButton;
    public GameObject modalPanelObject;

    private static WarningPanel warningPanel;

    public static WarningPanel Instance()
    {
        if (!warningPanel)
        {
            warningPanel = FindObjectOfType(typeof(WarningPanel)) as WarningPanel;
            if (!warningPanel)
                Debug.Log("There needs to be one active ModalWindow script on a GameObject in your scene.");
        }

        return warningPanel;

    }

    //JOSEPH: Ok/Cancel : A string, an image, OK event, Cancel event
    public void Warn(string descr, Sprite iconImage, UnityAction okEvent, UnityAction cancelEvent)
    {
        modalPanelObject.SetActive(true);

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(okEvent);
        okButton.onClick.AddListener(closePanel);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(cancelEvent);
        cancelButton.onClick.AddListener(closePanel);

        description.text = descr;
        this.iconImage.sprite = iconImage;

        this.iconImage.gameObject.SetActive(true);
        okButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }

    public void closePanel()
    {
        modalPanelObject.SetActive(false);
    }
}
