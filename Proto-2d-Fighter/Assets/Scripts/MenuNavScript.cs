using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuNavScript : MonoBehaviour {

    public Button curButton;
    public Button arcadeButton;
    public Button vsButton;
    public Button settingsButton;
    public Button exitButton;


    private KeyCode p1Up = StaticInfoScript.p1Up;
    private KeyCode p1Down = StaticInfoScript.p1Down;
    private KeyCode p1LockIn = StaticInfoScript.p1LockIn;
    private KeyCode p1Backout = StaticInfoScript.p1Backout;
    private Button[] allButtons;


    // Use this for initialization
    void Start () {
        allButtons = this.gameObject.GetComponentsInChildren<Button>();
        //Canvas[] allCanvases = this.gameObject.GetComponentsInParent<Canvas>();
        arcadeButton = allButtons[0];
        vsButton = allButtons[1];
        settingsButton = allButtons[2];
        exitButton = allButtons[3];


    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(p1Up) || Input.GetKeyUp(p1Down))
        {
            if (curButton == null)
            {
                curButton = arcadeButton;
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
        if (Input.GetKeyUp(p1LockIn) && curButton != null)
        {

            curButton.onClick.Invoke();
        };
    }
    public void EvtTgrnewHL(Button HLButton)
    {
        //Event Trigger code for new highlight.
        //When a button is highlighted then this function is called and updates the curbutton
        curButton = HLButton;
    }

}


