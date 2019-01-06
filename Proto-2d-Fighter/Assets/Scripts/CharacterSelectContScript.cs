using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectContScript : MonoBehaviour {

    //Takes the controlls from the staticinfoscript
    private KeyCode p1Left = StaticInfoScript.p1Left;
    private KeyCode p1Right = StaticInfoScript.p1Right;
    private KeyCode p1LockIn = StaticInfoScript.p1LockIn;
    private KeyCode p1Backout = StaticInfoScript.p1Backout;
    private KeyCode p2Left = StaticInfoScript.p2Left;
    private KeyCode p2Right = StaticInfoScript.p2Right;
    private KeyCode p2LockIn = StaticInfoScript.p2LockIn;
    private KeyCode p2Backout = StaticInfoScript.p2Backout;

    private Button[] allButtons;

    public Button curButton;
    public Button yesQuit;
    public Button noQuit;

    // Use this for initialization
    void Start () {
        allButtons = this.gameObject.GetComponentsInChildren<Button>();
        yesQuit = allButtons[0];
        noQuit = allButtons[1];
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(p1Left) || Input.GetKeyUp(p1Right) || Input.GetKeyUp(p2Left) || Input.GetKeyUp(p2Right))
        {
            if (curButton == null)
            {
                curButton = noQuit;
                curButton.Select();
            }
            else
            {
                //This allows the curButton to be set to null if the mouse interfears in the selection. Not 100% sure why. Requires further inquery.
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    curButton = null;
                }
            }
        }
        if ((Input.GetKeyUp(p1LockIn) || Input.GetKeyUp(p2LockIn)) && curButton != null)
        {
            curButton.onClick.Invoke();
        }
    }
    public void EvtTrgNewHLQM(Button HLButton)
    {
        //Event Trigger code for new highlight.
        //When a button is highlighted then this function is called and updates the curbutton
        curButton = HLButton;
    }
}
