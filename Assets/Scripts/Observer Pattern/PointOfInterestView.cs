using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Observer_Pattern
{
    public class PointOfInterestView : Observer
    {
        private PointOfInterest poi;

        public PointOfInterestView(PointOfInterest poi)
        {
            this.poi = poi;
            poi.attachObserver(this);
        }
        public void update()
        {
           // poi.changeIconScale();
           // poi.popUpSound();
            Vibration.Vibrate(1000);
           //  poi.displayPopUpWindow();
            Debug.Log("Point of interest detected.");
        }
        
    }
}
