using UnityEngine;
using System.Collections;

public class UI_Manager : MonoBehaviour {

    public void disableBoolAnimator(Animator anim)
    {
        anim.SetBool("isDisplayed", false);
    }

    public void enableBoolAnimator(Animator anim)
    {
        anim.SetBool("isDisplayed", true);
    }
}
