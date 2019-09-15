using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastAnimationScript : MonoBehaviour {

    public Animator charAnimator;

    public BlastAnimationScript(Animator Tor)
    {
        charAnimator = Tor;
    }

    public string PreformAnimation(string AnimationToPref, string CurAnimation)
    {
        if (CurAnimation == "")
        {
            charAnimator.SetBool(AnimationToPref, true);
            return AnimationToPref;
        }
        else
        {
            switch (AnimationToPref)
            {
                case "Run":
                    charAnimator.SetBool(CurAnimation, false);
                    charAnimator.SetBool("Run", true);
                    return "Run";
                default:
                    return "There was no set animation for CurAnimation";
            }
        }

    }
}
