using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TestModalWindow : MonoBehaviour
{

    private ModalWindow modalWindow;
    private DisplayManager displayManager;

    private UnityAction myOkAction;
    private UnityAction myCancelAction;

    void Awake()
    {
        modalWindow = ModalWindow.Instance();
        displayManager = DisplayManager.Instance();

        myOkAction = new UnityAction(TestOkFunction);
        myCancelAction = new UnityAction(TestCancelFunction);
    }

    //Send to the modal window to set up the buttons and functions to call
    public void TestOC()
    {
        modalWindow.Choice("Here is the building of 1920, and here are the bathrooms of the 1936 building.", myOkAction, myCancelAction);
    }
    //These are going to be wrapped into unity actions

    void TestOkFunction()
    {
       displayManager.DisplayMessage("One Punch!"); 
    }

    void TestCancelFunction()
    {
        displayManager.DisplayMessage("Canceled");
    }
}
