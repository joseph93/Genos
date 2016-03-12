using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class SummaryWindow : MonoBehaviour {

    public Text title;
    public Text description;
    public Image image1;
    public Image image2;
    public Button closeButton;
    public Scrollbar scrollBar;
    public GameObject modalPanelObject;

    private static SummaryWindow summaryWindow;

    public static SummaryWindow Instance()
    {
        if (!summaryWindow)
        {
            summaryWindow = FindObjectOfType(typeof(SummaryWindow)) as SummaryWindow;
            if (!summaryWindow)
                Debug.Log("There needs to be one active ModalWindow script on a GameObject in your scene.");
        }

        return summaryWindow;

    }

    public void SummaryNoImage(string title, string descr, UnityAction closeEvent)
    {
        modalPanelObject.SetActive(true);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(closeEvent);
        closeButton.onClick.AddListener(closePanel);

        this.title.text = title;
        description.text = descr;

        this.image1.gameObject.SetActive(false);
        this.image2.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        scrollBar.gameObject.SetActive(true);
    }

    //JOSEPH: Ok/Cancel : A string, an image, OK event, Cancel event
    public void SummaryOneImage(string title, string descr, Sprite image1, UnityAction closeEvent)
    {
        modalPanelObject.SetActive(true);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(closeEvent);
        closeButton.onClick.AddListener(closePanel);

        this.title.text = title;
        description.text = descr;
        this.image1.sprite = image1;

        this.image1.gameObject.SetActive(true);
        this.image2.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        scrollBar.gameObject.SetActive(true);
    }

    public void SummaryTwoImage(string title, string descr, Sprite image1, Sprite image2, UnityAction closeEvent)
    {
        modalPanelObject.SetActive(true);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(closeEvent);
        closeButton.onClick.AddListener(closePanel);

        this.title.text = title;
        description.text = descr;
        this.image1.sprite = image1;
        this.image2.sprite = image2;

        this.image1.gameObject.SetActive(true);
        this.image2.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        scrollBar.gameObject.SetActive(true);
    }

    public void closePanel()
    {
        modalPanelObject.SetActive(false);
    }
}
