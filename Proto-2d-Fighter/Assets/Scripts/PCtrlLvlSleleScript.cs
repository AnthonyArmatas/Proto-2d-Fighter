using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PCtrlLvlSleleScript : MonoBehaviour {

    //This script will handle the actual calculation of the level.
    //LevelSelectVisualScript Will handle the visual changes
    //private KeyCode p1Up = StaticInfoScript.p1Up;
    //private KeyCode p1Down = StaticInfoScript.p1Down;
    private KeyCode p1Left = StaticInfoScript.p1Left;
    private KeyCode p1Right = StaticInfoScript.p1Right;
    private KeyCode p1LockIn = StaticInfoScript.p1LockIn;
    private KeyCode p1Backout = StaticInfoScript.p1Backout;
    //private KeyCode p2Up = StaticInfoScript.p2Up;
    //private KeyCode p2Down = StaticInfoScript.p2Down;
    private KeyCode p2Left = StaticInfoScript.p2Left;
    private KeyCode p2Right = StaticInfoScript.p2Right;
    private KeyCode p2LockIn = StaticInfoScript.p2LockIn;
    private KeyCode p2Backout = StaticInfoScript.p2Backout;
    public Button backButton;
    private bool OnBackBut;
    private float justPressed;
    public bool lvlLocked;

    public int lvlSelector;

    public LevelSelectVisualScript LSVS;

    //Character selection by direction
    //Level1    = 1
    //Level2    = 2
    //Level3    = 3
    //Right is +1
    //left is -1
    //We are only using left and right for this script. If we add more we will reenable Up and down. 
    //Down is +2
    //up is -2

    void Start () {
        lvlSelector = 1;
        LSVS = this.GetComponent<LevelSelectVisualScript>();
        //This also works, but the one above can get it with this since it is attached to the same gameobject
        //CSVS = GameObject.Find("CharacterFrameHolder").GetComponent<CharacterSelectVisualScript>();
        backButton = GameObject.Find("BackCanvas").gameObject.GetComponentInChildren<Button>();
        lvlLocked = false;
        OnBackBut = false;
        justPressed = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //Switch Statmen with get keys and leading to changing the function and CharacterSelectVisualScript
        //checkP1Input();
        //checkP2Input();
        checkLockStatus();
        checkBackStatus();
        updateJustPressed();
    }
    void checkLockStatus()
    {
        if (OnBackBut == false) // Makes sure the user is not on the back button, could cause issues if below ran and it was not on a character
        {
            if (lvlLocked == false)
            {
                if (Input.GetKeyUp(p1LockIn) || Input.GetKeyUp(p2LockIn))
                {
                    //set p1Locked to true and call Charselect Script to change its color to the selcted green
                    lvlLocked = true;
                    LSVS.SendMessage("lockSelection", lvlSelector);

                }
                //Add a pop up screen asking if they want to go back to the main menu
                else
                {
                    checkPInput();
                }
            }
            else
            {
                if (Input.GetKeyUp(p1Backout) || Input.GetKeyUp(p2Backout))
                {
                    lvlLocked = false;
                    LSVS.SendMessage("unlockSelection", lvlSelector);
                    justPressed = Time.deltaTime;
                }
            }
        }

    }

    void checkPInput()
    {
        int[] selectData = new int[2];
        //Old Position
        selectData[0] = lvlSelector;
        if (Input.GetKeyUp(p1Left) || Input.GetKeyUp(p1Right) || Input.GetKeyUp(p2Left) || Input.GetKeyUp(p2Right))
        {
            if (Input.GetKeyUp(p1Left) || Input.GetKeyUp(p2Left))
            {
                lvlSelector = updateNewPos(lvlSelector, -1);
            }
            if (Input.GetKeyUp(p1Right) || Input.GetKeyUp(p2Right))
            {
                lvlSelector = updateNewPos(lvlSelector, 1);
            }
            //New Position
            selectData[1] = lvlSelector;
            LSVS.SendMessage("moveSelector", selectData);
        }

    }

    int updateNewPos(int oldPos, int direction)
    {
        if (direction == 1)
        {
            if (oldPos == 3)
            {
                return 1;
            }
            else
            {
                return oldPos + 1;
            }

        }
        else
        {
            if (oldPos == 1)
            {
                return 3;
            }
            else
            {
                return oldPos - 1;
            }
        }
    }

    void checkBackStatus()
    {
        if (justPressed > Time.deltaTime + 1)
        {
            if (OnBackBut == false)
            {
                if ((Input.GetKeyUp(p1Backout) || Input.GetKeyUp(p2Backout)) && lvlLocked == false)
                {
                    //set p1OnBackBut to true and call Charselect Script to change its color to the selcted green
                    OnBackBut = true;
                    LSVS.SendMessage("hideLevelSelector", lvlSelector);
                    lvlSelector = 0;
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("BackButton"));

                }
            }
            else
            {
                if (Input.GetKeyUp(p1LockIn) || Input.GetKeyUp(p2LockIn))
                {
                    backButton.onClick.Invoke();
                }
                if (Input.GetKeyUp(p1Left) || Input.GetKeyUp(p1Right) || Input.GetKeyUp(p2Left) || Input.GetKeyUp(p2Right))
                {
                    OnBackBut = false;
                    //This is manually set to the last level. NEED TO UPDATE IF NEW LEVELS ADDED
                    lvlSelector = 3;
                    LSVS.SendMessage("showLevelSelector", lvlSelector); ;
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Level3"));
                }
            }
        }

    }
    void updateJustPressed()
    {
        //Debug.Log(justPressed);
        justPressed = justPressed + Time.deltaTime;
        //Debug.Log(justPressed);
    }
}
