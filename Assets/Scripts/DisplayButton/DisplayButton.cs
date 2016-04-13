﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using Assets.Scripts.Language;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class DisplayButton : MonoBehaviour {
    

        private SummaryWindow summaryWindow;
        private UnityAction myCloseAction;

        Text yourButtonText;
        private string titleButton = "jjjjjjjjjjjjj";
        private string titlePanel = "tttttttttttt1";
        private string descriptionPanel = "dddddddddddd1";



        void Start() {
           // assignContent(poiList);

            summaryWindow = SummaryWindow.Instance();
            myCloseAction = new UnityAction(summaryWindow.closePanel);
        }

        /*
         * Assign the parsed titleButton, titlePanel, and descriptionPanel from the list of storyline/pos/poi
         */
        public void assignContent(List<Node> poiList)
        {
            //Check inside specific storyline for each existing point of interest
            foreach (var n in poiList)
            {
                //Check if poi at runtime
                if (n.GetType() == typeof(PointOfInterest))
                {
                    this.titleButton = n.GetComponent<Description>().getTitle(); //poi.GetComponent<Description>().getTitle()
                    this.titlePanel = n.GetComponent<Description>().getTitle();
                    this.descriptionPanel = n.GetComponent<Description>().getDescription();

                    displayButtonTitle();
                }
            }


        }

        /*
         * Display the title of the button
         */
        public void displayButtonTitle()
        {
            yourButtonText = transform.FindChild("Text").GetComponent<Text>();
            yourButtonText.text = titleButton;
        }

        /*
         * Display the title and the description of the summary panel
         */
        public void displayPanelSummary()
        {
            summaryWindow.SummaryNoImage(titlePanel, descriptionPanel, myCloseAction);
            //summaryWindow.SummaryOneImage(title2, description2, image1, myCloseAction);
        }
    }
}