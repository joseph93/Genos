using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Observer_Pattern
{
    public class BeaconView : Observer
    {
        private PointOfInterest poi;

        public BeaconView(PointOfInterest poi)
        {
            this.poi = poi;
            poi.attachObserver(this);
        }
        public void update()
        {
            poi.changeIconScale();
            poi.popUpSound();
            Vibration.Vibrate(1000);
            poi.displayPopUpWindow();
            Debug.Log("Beacon Detected.");
        }
        
    }
}
