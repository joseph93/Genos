using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Assets.Scripts;

public class ListOfPOI : MonoBehaviour
{
    private Map map;

    private SummaryWindow summaryWindow;
    private UnityAction myCloseAction;

    /*public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    public Sprite image5;
    public Sprite image6;

    public GameObject poi1;
    public GameObject poi2;
    public GameObject poi3;
    public GameObject poi4;
    public GameObject poi5;
    public GameObject poi6;*/

    private int poiId;

    //EN
    private string title1 = "MOEB Start for all tours";
    private string title2 = "Ross and MacDonald Building";
    private string title3 = "Presidents office";
    private string title4 = "Freight Elevator";
    private string title5 = "The column on the 5th floor";
    private string title6 = "The court yard";

    private string description1 = "You can start an exploration tour on your own. Just keep your smart phone handy and walk. You will get notefied when you can listen to a audioclip, a video, images or written information on the building and its fascinating history. Happy walk!";
    private string description2 = "You have entered into the small extension that RCA had commissioned in the mid 1930s from Montreal;s architectural firm Ross and MacDonald. The architects were in high demand, many buildings in Montreal carry their signature. Look at the smooth column heads. Nowhere else in the building will you see this level of care taken in the appearance.";
    private string description3 = "When the building opened in 1936, the management and administration moved from the older complex along Lacasse and St. Antoine Street to here. The floorplan has cangened, resulting that you now can walk barrierfree from the cloakrooms, at the bottom of the stairs, through the showroom, crossing the advertisement department and ending up, where the secretary of the president had her or his desk. A private stair case, which starts today on the second floor, allowed the president to enter and exit the building relatively unobserved. How convenient.";
    private string description4 = "Look at the wall around the passenger elevator. You see the shadow of a much larger frame. We do not know why originally two freight elevators were here side by side. The old passenger elevator is still in place behind the door to the staircase. Have a look. Did you try to push the butten to call the elevator here? Well, good luck!";
    private string description5 = "put text";
    private string description6 = "put text";

    //FR
    private string title11 = "Debut de tous le tours";
    private string title22 = "L'annex du Ross & MacDonald";
    private string title33 = "Le bureau du President RCA";
    private string title44 = "Les lifts";
    private string title55 = "Colunne du 5em etage";
    private string title66 = "View dans la court du edifice";

    private string text11 = "a besoin du traduction";
    private string text22 = "Aussi je besoin d;une tradution";
    private string text33 = "Une autre place de mettre du contenu";
    private string text44 = "pas encore traduit";
    private string text55 = "a besoin du text";
    private string text66 = "a besoin du text en francaise";



    // Use this for initialization
	
	// Update is called once per frame
	void Start() {
        summaryWindow = SummaryWindow.Instance();

        myCloseAction = new UnityAction(summaryWindow.closePanel);
        //myFindAction = new UnityAction(locatePoi);
    }

   /* public void locatePoi()
    {

        if (poiId == 1)
        {
            Camera.main.transform.position = poi1.transform.position;
            Camera.main.orthographicSize = 30f;
        }
            

        if (poiId == 2)
        {
            Camera.main.transform.position = poi2.transform.position;
            Camera.main.orthographicSize = 30f;
        }
            

        if (poiId == 3)
        {
            Camera.main.transform.position = poi3.transform.position;
            Camera.main.orthographicSize = 30f;
        }
            

        if (poiId == 4)
        {
            Camera.main.transform.position = poi4.transform.position;
            Camera.main.orthographicSize = 30f;
        }
            

        if (poiId == 5)
        {
            Camera.main.transform.position = poi5.transform.position;
            Camera.main.orthographicSize = 30f;
        }
            

        if (poiId == 6)
        {
            Camera.main.transform.position = poi6.transform.position;
            Camera.main.orthographicSize = 30f;
        }
            
       
    }*/

    public void pointOfInterest1()
    {
        poiId = 1;
        string lg = PlayerPrefs.GetString("language");

        if (lg.Equals("EN"))
        {
            summaryWindow.SummaryOneButton(title1, description1, myCloseAction);
        }

        else if (lg.Equals("FR"))
        {
            summaryWindow.SummaryOneButton(title11, text11, myCloseAction);
        }
        
    }

    public void pointOfInterest2()
    {
        poiId = 2;
        string lg = PlayerPrefs.GetString("language");

        if (lg.Equals("EN"))
        {
            summaryWindow.SummaryOneButton(title2, description2, myCloseAction);
        }

        else if (lg.Equals("FR"))
        {
            summaryWindow.SummaryOneButton(title22, text22, myCloseAction);
        }

    }

    public void pointOfInterest3()
    {
        poiId = 3; string lg = PlayerPrefs.GetString("language");

        if (lg.Equals("EN"))
        {
            summaryWindow.SummaryOneButton(title3, description3, myCloseAction);
        }

        else if (lg.Equals("FR"))
        {
            summaryWindow.SummaryOneButton(title33, text33, myCloseAction);
        }
       
    }

    public void pointOfInterest4()
    {
        poiId = 4;
        string lg = PlayerPrefs.GetString("language");

        if (lg.Equals("EN"))
        {
            summaryWindow.SummaryOneButton(title4, description4, myCloseAction);
        }

        else if (lg.Equals("FR"))
        {
            summaryWindow.SummaryOneButton(title44, text44, myCloseAction);
        }

    }

    public void pointOfInterest5()
    {
        poiId = 5;
        string lg = PlayerPrefs.GetString("language");

        if (lg.Equals("EN"))
        {
            summaryWindow.SummaryOneButton(title5, description5, myCloseAction);
        }

        else if (lg.Equals("FR"))
        {
            summaryWindow.SummaryOneButton(title55, text55, myCloseAction);
        }

    }

    public void pointOfInterest6()
    {
        poiId = 6;
        string lg = PlayerPrefs.GetString("language");

        if (lg.Equals("EN"))
        {
            summaryWindow.SummaryOneButton(title6, description6, myCloseAction);
        }

        else if (lg.Equals("FR"))
        {
            summaryWindow.SummaryOneButton(title66, text66, myCloseAction);
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
