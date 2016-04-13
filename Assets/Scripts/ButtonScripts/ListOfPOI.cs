using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Assets.Scripts;

public class ListOfPOI : MonoBehaviour
{
    private Map map;

    private SummaryWindow summaryWindow;
    private UnityAction myCloseAction;

    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;
    public Sprite image6;

    private string title1 = "1";
    private string title2 = "2";
    private string title3 = "3";
    private string title4 = "4";
    private string title5 = "5";
    private string title6 = "6";

    private string description1 = "1";
    private string description2 = "2";
    private string description3 = "3";
    private string description4 = "4";
    private string description5 = "5";
    private string description6 = "6";

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

    public void pointOfInterest4()
    {
        summaryWindow.SummaryNoImage(title4, description4, myCloseAction);
    }

    public void pointOfInterest5()
    {
        summaryWindow.SummaryNoImage(title5, description5, myCloseAction);
    }

    public void pointOfInterest6()
    {
        summaryWindow.SummaryNoImage(title6, description6, myCloseAction);
    }

    //map.GetStorylines()[0].getStorypointList()
    public void displayButtons() //take title+summary from json
    {/*
        //foreach to check which storyline list
        foreach ()
        {
            //foreach to check for each "node" or pos or poi 
            foreach () { 
                //assign each ttile to the TEXT of the button
                //assign the description to the TEXT of the summaryWindow
            }
        }
        */
    }
}
