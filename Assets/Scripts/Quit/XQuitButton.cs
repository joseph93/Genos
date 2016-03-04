using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class XQuitButton : MonoBehaviour {

    private QuitSpash quitSpash;

    private UnityAction yesAction;
    private UnityAction noAction;

    void Awake()
    {
        quitSpash = QuitSpash.Instance();
        yesAction = new UnityAction(ApplicationQuit);
        noAction = new UnityAction(quitSpash.closeQuitPanel);

    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void DisplayQuitPanel()
    {
        quitSpash.quitChoice("Do you want to quit the application?", yesAction, noAction);

    }

}
