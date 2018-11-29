using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCtrlCharSeleScript : MonoBehaviour {
    //This script will handle the actual calculation of the character.
    //CharacterSelectScript Will handle the visual changes
    KeyCode p1Up;
    KeyCode p1Down;
    KeyCode p1Left;
    KeyCode p1Right;
    KeyCode p2Up;
    KeyCode p2Down;
    KeyCode p2Left;
    KeyCode p2Right;

    private int p1CharSelector;
    private int p2CharSelector;



    //Character selection by direction
    //IceRider  = 0
    //Blaster   = 1
    //Swinger   = 2
    //Phaser    = 4
    //Right is +1
    //Down is +2
    //left is -1
    //up is -2
    // Use this for initialization
    void Start () {
        p1CharSelector = 1;
        p2CharSelector = 1;

    }
	
	// Update is called once per frame
	void Update () {
        //Switch Statmen with get keys and leading to changing the function and CharacterSelectScript 
    }
}
