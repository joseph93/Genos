using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class XQuitButton : MonoBehaviour {

    private ModalWindow modalWindow;

    public Sprite iconImage;
    private UnityAction yesAction;
    private UnityAction noAction;

    void Awake()
    {
        modalWindow = ModalWindow.Instance();
        yesAction = new UnityAction(ApplicationQuit);
        noAction = new UnityAction(modalWindow.closePanel);

    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void DisplayQuitPanel()
    {
        modalWindow.Choice("Do you want to quit the application?", iconImage, yesAction, noAction);

    }

}
