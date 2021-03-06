﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCharacterMovement : PlayerBaseMovement
{
    public KeyCode movementKey; 


    public bool movAbltyEnabled = false;    //This toggles the movement ice glide ability
    public double timeSinceMov = 0;         //The time delay between being able to toggle the movement ability on or off
    private bool firstMove = true;          //This check is the only way to bypass the 1.5 sec limit. It is for if the player wants to toggle the movemnt as soon as the game starts



    public AimController AC;
    public IceMovementScript ims;

    // Use this for initialization
    void Start () {
        //Grabs all of the components from the character
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        AC = GetComponentInChildren<AimController>();
        ims = GetComponent<IceMovementScript>();

    }
	
	// Update is called once per frame
	void Update () {
        
        if (!movAbltyEnabled)   //If ice movement is not enabled, preform normal movement from super class
        {
            preformBaseMovement();
        }
        else                    //If ice movement is enabled, call IceMovementScript and preform special movement
        {
            //Changing the gravity makes it seem more floaty delivering the desired effect
            //Anywhere from 2-3 Thee is much faster than Two
            ims.iceGlide(AC, theRB, dashSpeed);
        }
        if (Input.GetKey(movementKey))
        {
            if (timeSinceMov >= 1.5 || firstMove){  //Creates a time delay between toggle on and off
                firstMove = false;                  //Sets the intial toggle off no matter when it is called. Only beat the system once
                if (movAbltyEnabled)
                {
                    Debug.Log("MOVEMENT DISABLED");
                    movAbltyEnabled = false;        //Turns off the movement toggle
                    theRB.gravityScale = 10;        //Sets the gravity back to normal
                    tor.SetBool("Gliding", false);  //Turns off the movement animation
                }
                else
                {
                    Debug.Log("MOVEMENT ENABLED");
                    theRB.gravityScale = 3;
                    tor.SetBool("Walking", false);
                    tor.SetBool("Gliding", true);
                    movAbltyEnabled = true;
                }
                timeSinceMov = 0f;                  //Sets the toggle clock back to 0
            }

        }
        timeSinceMov += Time.deltaTime;             //Incriments the toggle clock for the movement ability

    }
}
