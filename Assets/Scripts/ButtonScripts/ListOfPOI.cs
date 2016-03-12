using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ListOfPOI : MonoBehaviour
{
    private SummaryWindow summaryWindow;
    private UnityAction myCloseAction;

    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    
    private string title1 = "Old President's Office";
    private string title2 = "Large Washrooms";
    private string title3 = "The Machine Shop";

    private string description1 = "Stop at the end of the corridor before the bridge to building 18. The door to the " +
                                  "right was the old president’s office.The door is closed, Nipper barks and on the screen " +
                                  "appears a mental image from Nipper with the image of the old office, " +
                                  "as suggested in three drawings by thearchitects Ross and MacDonalds.";

    private string description2 =
        "Nipper now moves towards the Lenoir Street building (building 17), passing through building 5 (from 1920) on the second floor.At the door half way through the corridor, Nipper slows down/stops and sniffs around, and makes a sound of bewildering. He hears toilet flushing and water running, hands are washed.On the floor, markings indicate the position of long gone walls, which once belonged to large washrooms";

    private string description3 =
        "He is crossing a large machine shop. Images show comparable areas throughout different times, sound should represent appropriately the images.The images are projected when passing by, or mounted on the walls or popping up on the smart phone.";
    
    // Use this for initialization
    void Start () {
        summaryWindow = SummaryWindow.Instance();
        
        myCloseAction = new UnityAction(summaryWindow.closePanel);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pointOfInterest1()
    {
        summaryWindow.SummaryNoImage(title1, description1, myCloseAction);
    }

    public void pointOfInterest2()
    {
        summaryWindow.SummaryOneImage(title2, description2, image1, myCloseAction);
    }

    public void pointOfInterest3()
    {
        summaryWindow.SummaryTwoImage(title3, description3, image2, image3, myCloseAction);
    }
}
