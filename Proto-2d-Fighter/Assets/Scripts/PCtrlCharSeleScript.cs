using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PCtrlCharSeleScript : MonoBehaviour {

    //This script will handle the actual calculation of the character.
    //CharacterSelectVisualScript Will handle the visual changes
    private KeyCode p1Up        = StaticInfoScript.p1Up;
    private KeyCode p1Down      = StaticInfoScript.p1Down;
    private KeyCode p1Left      = StaticInfoScript.p1Left;
    private KeyCode p1Right     = StaticInfoScript.p1Right;
    private KeyCode p1LockIn    = StaticInfoScript.p1LockIn;
    private KeyCode p1Backout   = StaticInfoScript.p1Backout;
    private KeyCode p2Up        = StaticInfoScript.p2Up;
    private KeyCode p2Down      = StaticInfoScript.p2Down;
    private KeyCode p2Left      = StaticInfoScript.p2Left;
    private KeyCode p2Right     = StaticInfoScript.p2Right;
    private KeyCode p2LockIn    = StaticInfoScript.p2LockIn;
    private KeyCode p2Backout   = StaticInfoScript.p2Backout;
    public Button backButton;
    private bool p1OnBackBut;
    private bool p2OnBackBut;
    private float justPressed;
    public bool p1Locked;
    public bool p2Locked;

    public int p1CharSelector;
    public int p2CharSelector;

    public CharacterSelectVisualScript CSVS;

    //Character selection by direction
    //IceRider  = 1
    //Blaster   = 2
    //Swinger   = 3
    //Phaser    = 4
    //Right is +1
    //Down is +2
    //left is -1
    //up is -2
    // Use this for initialization

    void Start () {
        p1CharSelector = 1;
        p2CharSelector = 1;
        CSVS = this.GetComponent<CharacterSelectVisualScript>();
        //This also works, but the one above can get it with this since it is attached to the same gameobject
        //CSVS = GameObject.Find("CharacterFrameHolder").GetComponent<CharacterSelectVisualScript>();
        backButton = GameObject.Find("BackCanvas").gameObject.GetComponentInChildren<Button>();
        p1Locked = false;
        p1Locked = false;
        p1OnBackBut = false;
        p2OnBackBut = false;
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
        if( p1OnBackBut == false) // Makes sure the user is not on the back button, could cause issues if below ran and it was not on a character
        {
            //PLAYER 1
            int[] selectData = new int[2];
            if (p1Locked == false)
            {
                if (Input.GetKeyUp(p1LockIn))
                {
                    //set p1Locked to true and call Charselect Script to change its color to the selcted green
                    p1Locked = true;
                    selectData[0] = p1CharSelector;
                    selectData[1] = 1;
                    CSVS.SendMessage("lockSelection", selectData);

                }
                //Add a pop up screen asking if they want to go back to the main menu
                else
                {
                    checkP1Input();
                }
            }
            else
            {
                if (Input.GetKeyUp(p1Backout))
                {
                    p1Locked = false;
                    selectData[0] = p1CharSelector;
                    selectData[1] = 1;
                    CSVS.SendMessage("unlockSelection", selectData);
                    justPressed = Time.deltaTime;
                }
            }

            //PLAYER 2
            if (p2Locked == false)
            {
                if (Input.GetKeyUp(p2LockIn))
                {
                    //set p2Locked to true and call Charselect Script to change its color to the selcted green
                    p2Locked = true;
                    selectData[0] = p2CharSelector;
                    selectData[1] = 2;
                    CSVS.SendMessage("lockSelection", selectData);

                }
                //Add a pop up screen asking if they want to go back to the main menu
                else
                {
                    checkP2Input();
                }
            }
            else
            {
                if (Input.GetKeyUp(p2Backout))
                {
                    p2Locked = false;
                    selectData[0] = p2CharSelector;
                    selectData[1] = 2;
                    CSVS.SendMessage("unlockSelection", selectData);
                    justPressed = Time.deltaTime;
                }
            }
        }

    }
    void checkP1Input()
    {
        int[] selectData = new int[3];
        selectData[0] = p1CharSelector;
        selectData[2] = 1;
        if(Input.GetKeyUp(p1Up) || Input.GetKeyUp(p1Down) || Input.GetKeyUp(p1Left) || Input.GetKeyUp(p1Right))
        {
            if (Input.GetKeyUp(p1Up))
            {
                p1CharSelector = updateNewPos(p1CharSelector, -2);
                selectData[1] = p1CharSelector;
            }
            if (Input.GetKeyUp(p1Down))
            {
                p1CharSelector = updateNewPos(p1CharSelector, +2);
                selectData[1] = p1CharSelector;
            }
            if (Input.GetKeyUp(p1Left))
            {
                p1CharSelector = updateNewPos(p1CharSelector, -1);
                selectData[1] = p1CharSelector;
            }
            if (Input.GetKeyUp(p1Right))
            {
                p1CharSelector = updateNewPos(p1CharSelector, 1);
                selectData[1] = p1CharSelector;
            }

            CSVS.SendMessage("moveSelector", selectData);
        }

    }
    void checkP2Input()
    {
        int[] selectData = new int[3];
        selectData[0] = p2CharSelector;
        selectData[2] = 2;

        if (Input.GetKeyUp(p2Up) || Input.GetKeyUp(p2Down) || Input.GetKeyUp(p2Left) || Input.GetKeyUp(p2Right))
        {
            if (Input.GetKeyUp(p2Up))
            {
                p2CharSelector = updateNewPos(p2CharSelector, -2);
                selectData[1] = p2CharSelector;
            }
            if (Input.GetKeyUp(p2Down))
            {
                p2CharSelector = updateNewPos(p2CharSelector, +2);
                selectData[1] = p2CharSelector;
            }
            if (Input.GetKeyUp(p2Left))
            {
                p2CharSelector = updateNewPos(p2CharSelector, -1);
                selectData[1] = p2CharSelector;
            }
            if (Input.GetKeyUp(p2Right))
            {
                p2CharSelector = updateNewPos(p2CharSelector, 1);
                selectData[1] = p2CharSelector;
            }
            CSVS.SendMessage("moveSelector", selectData);
        }
            
    }
    int updateNewPos(int oldPos, int direction)
    {
        if (direction == 1)
        {
            if(oldPos == 4)
            {
                return 1;
            }
            else
            {
                return oldPos + 1;
            }

        } else if (direction == 2)
        {
            if (oldPos == 3)
            {
                return 1;
            }
            else if (oldPos == 4)
            {
                return 2;
            }
            else
            {
                return oldPos + 2;
            }

        } else if (direction == -1)
        {
            if (oldPos == 1)
            {
                return 4;
            }
            else
            {
                return oldPos - 1;
            }
        }
        else
        {
            if (oldPos == 2)
            {
                return 4;
            }
            else if (oldPos == 1)
            {
                return 3;
            }
            else
            {
                return oldPos - 2;
            }
        }
    }

    void checkBackStatus()
    {
        if(justPressed > Time.deltaTime + 1)
        {
            //PLAYER 1
            if (p1OnBackBut == false)
            {
                if (Input.GetKeyUp(p1Backout) && p1Locked == false)
                {
                    //set p1OnBackBut to true and call Charselect Script to change its color to the selcted green
                    p1OnBackBut = true;
                    p1CharSelector = 0;
                    CSVS.SendMessage("hideCharacterSelector", 1);
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("BackButton"));

                }
            }
            else
            {
                if (Input.GetKeyUp(p1LockIn))
                {
                    backButton.onClick.Invoke();
                }
                if (Input.GetKeyUp(p1Up) || Input.GetKeyUp(p1Down) || Input.GetKeyUp(p1Left) || Input.GetKeyUp(p1Right))
                {
                    p1OnBackBut = false;
                    p1CharSelector = 4;
                    CSVS.SendMessage("showCharacterSelector", 1); ;
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Character4"));
                }
            }

            //PLAYER 2
            if (p2OnBackBut == false)
            {
                if (Input.GetKeyUp(p2Backout) && p2Locked == false)
                {
                    //set p1OnBackBut to true and call Charselect Script to change its color to the selcted green
                    p2OnBackBut = true;
                    p2CharSelector = 0;
                    CSVS.SendMessage("hideCharacterSelector", 2);
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("BackButton"));

                }
            }
            else
            {
                if (Input.GetKeyUp(p2LockIn))
                {
                    backButton.onClick.Invoke();
                }

                if (Input.GetKeyUp(p2Up) || Input.GetKeyUp(p2Down) || Input.GetKeyUp(p2Left) || Input.GetKeyUp(p2Right))
                {
                    p2OnBackBut = false;
                    p2CharSelector = 4;
                    CSVS.SendMessage("showCharacterSelector", 2);
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Character4"));
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
