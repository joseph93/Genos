using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Observer_Pattern
{
    public class StoryPointView : Observer
    {
        private StoryPoint sp;

        public StoryPointView(StoryPoint sp)
        {
            this.sp = sp;
            sp.attachObserver(this);
        }

        public void update()
        {
            sp.changeIconScale();
            sp.popUpSound();
            Vibration.Vibrate(1000);
            sp.displayPopUpWindow();
            sp.storyIsVisited();
            Debug.Log("Storypoint detected.");
        }
    }
}
