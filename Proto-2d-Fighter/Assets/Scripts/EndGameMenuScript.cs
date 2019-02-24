using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndGameMenuScript : MonoBehaviour {

    //Takes the controlls from the staticinfoscript
    private KeyCode p1Up = StaticInfoScript.p1Up;
    private KeyCode p1Down = StaticInfoScript.p1Down;
    private KeyCode p1LockIn = StaticInfoScript.p1LockIn;
    private KeyCode p1Backout = StaticInfoScript.p1Backout;
    private KeyCode p2Up = StaticInfoScript.p2Up;
    private KeyCode p2Down = StaticInfoScript.p2Down;
    private KeyCode p2LockIn = StaticInfoScript.p2LockIn;
    private KeyCode p2Backout = StaticInfoScript.p2Backout;

    private Button[] allButtons;

    public Button curButton;
    public Button fightAgain;
    public Button charSelect;
    public Button mainMenu;

    // Use this for initialization
    void Start () {
        allButtons = this.gameObject.GetComponentsInChildren<Button>();
        fightAgain = allButtons[0];
        charSelect = allButtons[1];
        mainMenu = allButtons[2];
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(p1Up) || Input.GetKeyUp(p1Down) || Input.GetKeyUp(p2Up) || Input.GetKeyUp(p2Down))
        {
            if (curButton == null)
            {
                curButton = fightAgain;
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
