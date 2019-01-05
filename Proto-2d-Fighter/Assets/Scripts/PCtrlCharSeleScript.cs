using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCtrlCharSeleScript : MonoBehaviour {
    //This script will handle the actual calculation of the character.
    //CharacterSelectScript Will handle the visual changes
    public KeyCode p1Up;
    public KeyCode p1Down;
    public KeyCode p1Left;
    public KeyCode p1Right;
    public KeyCode p1LockIn;
    public KeyCode p1Backout;
    public KeyCode p2Up;
    public KeyCode p2Down;
    public KeyCode p2Left;
    public KeyCode p2Right;
    public KeyCode p2LockIn;
    public KeyCode p2Backout;
    public bool p1Locked;
    public bool p2Locked;

    private int p1CharSelector;
    private int p2CharSelector;

    public CharacterSelectScript css;



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
        css.GetComponent<CharacterSelectScript>();

        p1Locked = false;
        p1Locked = false;

    }
	
	// Update is called once per frame
	void Update () {
        //Switch Statmen with get keys and leading to changing the function and CharacterSelectScript
        //checkP1Input();
        //checkP2Input();
        checkLockStatus();

    }
    void checkLockStatus()
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
                css.SendMessage("lockSelection", selectData);

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
                css.SendMessage("unlockSelection", selectData);
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
                css.SendMessage("lockSelection", selectData);

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
                css.SendMessage("unlockSelection", selectData);
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
            css.SendMessage("moveSelector", selectData);
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
            css.SendMessage("moveSelector", selectData);
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

}
