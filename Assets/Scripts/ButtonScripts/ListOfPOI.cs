using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Assets.Scripts;

public class ListOfPOI : MonoBehaviour
{
    private Map map;

    private SummaryWindow summaryWindow;
    private UnityAction myCloseAction;

    private string title;
    private string description;
    private int poiId;

    // Use this for initialization
	
	// Update is called once per frame
	void Start() {
        summaryWindow = SummaryWindow.Instance();

        myCloseAction = new UnityAction(summaryWindow.closePanel);
        //myFindAction = new UnityAction(locatePoi);
    }

    public void pointOfInterest1()
    {
        poiId = 1;
        string lg = PlayerPrefs.GetString("language");

        if (lg.Equals("EN"))
        {
            summaryWindow.SummaryOneButton(title, description, myCloseAction);
        }

        else if (lg.Equals("FR"))
        {
            summaryWindow.SummaryOneButton(title, description, myCloseAction);
        }
        
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
