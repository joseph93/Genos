using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Observer_Pattern
{
    public class StoryPointView : MonoBehaviour, Observer
    {
        private POS sp;

        public StoryPointView(POS sp)
        {
            this.sp = sp;
            sp.attachObserver(this);
        }

        public void update()
        {
            sp.changeIconScale();
            //sp.popUpSound();
            Vibration.Vibrate(1000);
            sp.displayStorylinePopUpWindow();
            //StartCoroutine(sp.playVideo());
            //sp.storyIsVisited();
            Debug.Log("Storypoint detected.");
        }
    }
}
