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
    
    private string title1 = "EV 3.187";
    private string title2 = "Evacuation instructions";
    private string title3 = "End of your demo";

    private string description1 = "<div>Professor's office</div><div><b>blueberry</b></div>";

    private string description2 = "<div> Read the instructions carefully for your safety!</div><div><b>Mint</b></div>";
    private string description3 = "<div>Get some rest. It's over!</div><div><b>Icy</b></div>";

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
        summaryWindow.SummaryNoImage(title3, description3, myCloseAction);
    }
}
