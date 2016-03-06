using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitSpash : MonoBehaviour {

    public Text text;
    public Image icon;
    public Button yes;
    public Button no;
    public GameObject quitPanelObject;

    private static QuitSpash quitSpash;

    public static QuitSpash Instance()
    {
        if (!quitSpash)
        {
            quitSpash = FindObjectOfType(typeof(QuitSpash)) as QuitSpash;
            if (!quitSpash)
            {
                Debug.Log("Error not created.");
            }
        }
        return quitSpash;
    }

    public void quitChoice(string question, UnityAction yesEvent, UnityAction noEvent)
    {
        quitPanelObject.SetActive(true);
        yes.onClick.RemoveAllListeners();
        yes.onClick.AddListener(yesEvent);
        yes.onClick.AddListener(closeQuitPanel);

        no.onClick.RemoveAllListeners();
        no.onClick.AddListener(noEvent);
        no.onClick.AddListener(closeQuitPanel);

        icon.gameObject.SetActive(true);
        yes.gameObject.SetActive(true);
        no.gameObject.SetActive(true);

        text.text = question;
    }

    public void closeQuitPanel()
    {
        quitPanelObject.SetActive(false);
    }
}
