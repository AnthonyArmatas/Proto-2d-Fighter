using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseMovement : MonoBehaviour {

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    public float walkSpeed = 2.5f;  //Standard walking speed
    public float dashSpeed = 5f;    //Standard dashing speed
    public bool dashing = false;    //Is dashing check
    public float jumpForce = 20f;   //Standard jump force 
    public int leftTotal = 0;       //Total times player taped left
    public int rightTotal = 0;      //Total times player taped right
    //Changing elapsed time to double from float because of https://randomascii.wordpress.com/2012/02/13/dont-store-that-in-a-float/ 
    public double leftTimeDelay = 0;    //Time delay after first tap left
    public double rightTimeDelay = 0;   //Time delay after first tap right
    public int curJumps = 0;        //Total number pf jumps a player has made

    public bool facingRight;    //A bool to use as a check for the current direction the player is facing. I dont think I am using it right now. I may use it later or remove it entirely.
    public int maxJumps = 2;           //Max number of jumps a player can jump before hitting a piece of terrain


    public Rigidbody2D theRB;
    public Animator tor;


    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        preformBaseMovement();
    }

    protected void preformBaseMovement()
    {
        if ((Input.GetKey(right) || Input.GetKey(left)) && !dashing)
        {
            if (Input.GetKey(right))
            {
                theRB.velocity = new Vector2(walkSpeed, theRB.velocity.y);  //Makes the player walk right
                transform.localRotation = Quaternion.Euler(0, 0, 0);        //Rotates a players model right
                facingRight = true;                                         //Sets the facing right bool to true;
                tor.SetBool("Walking", true);                               //Sets the players walking animation
            }
            else
            {
                theRB.velocity = new Vector2(-walkSpeed, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                facingRight = false;
                tor.SetBool("Walking", true);
            }

        }
        else if (dashing)   //Checks to see if the player is/was dashing
        {
            if (!Input.GetKey(right) && !Input.GetKey(left))    //if the player was dashing but stoped pressing any keys, make the character stop 
            {
                theRB.velocity = new Vector2(0, theRB.velocity.y);
                rightTimeDelay = 0;
                rightTotal = 0;
                leftTimeDelay = 0;
                leftTotal = 0;
                dashing = false;
            }
            else
            {
                if (Input.GetKey(right))    //Sets the character to dash right or left
                {
                    theRB.velocity = new Vector2(dashSpeed, theRB.velocity.y);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    facingRight = true;
                }
                else
                {
                    theRB.velocity = new Vector2(-dashSpeed, theRB.velocity.y);
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    facingRight = false;
                }
            }
        }
        else
        {
            tor.SetBool("Walking", false); //Removes the walking animation
        }

        if (Input.GetKeyDown(right))
        {
            if (leftTotal != 0)
            {
                //Comment this out to allow side dashing( i.e. right then left then right will have you run right. Not sure if I want this or not. Not sure why I wouldnt. but for now its off)
                leftTotal = 0;
            }
            rightTotal++;
        }
        if (Input.GetKeyDown(left))
        {
            if (rightTotal != 0)
            {
                //Comment this out to allow side dashing( i.e. right then left then right will have you run right. Not sure if I want this or not. Not sure why I wouldnt. but for now its off)
                rightTotal = 0;
            }
            leftTotal++;
        }


        belowTapTime(ref rightTotal, ref rightTimeDelay);
        belowTapTime(ref leftTotal, ref leftTimeDelay);

        overTapTime(ref rightTotal, ref rightTimeDelay);
        overTapTime(ref leftTotal, ref leftTimeDelay);

        withinTapTime(ref rightTotal, ref rightTimeDelay, facingRight);
        withinTapTime(ref leftTotal, ref leftTimeDelay, facingRight);

        if (Input.GetKeyDown(jump)) //Checks for jump inputs
        {
            if (curJumps < maxJumps) //if the character has not already reached its max jump limit, jump and inc.
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                curJumps++;
            }

        }
    }

    void belowTapTime(ref int pushTotal, ref double timeDelay)
    {
        if ((pushTotal == 1) && (timeDelay < .5))   //If movement keys have been taps once and its not over the .5 time limit, increase the time limit
        {
            timeDelay += Time.deltaTime;
        }
    }
    void overTapTime(ref int pushTotal, ref double timeDelay)
    {
        if ((pushTotal == 1) && (timeDelay >= .5))  //If its been over the time limit with out tapping the key a second time
        {
            timeDelay = 0;
            pushTotal = 0;
        }
    }
    void withinTapTime(ref int pushTotal, ref double timeDelay, bool direction)
    {
        if ((pushTotal == 2) && (timeDelay < .5)) //If its withinh the time limit and the player taps the key a second time, increase the speed and set dash to true
        {
            if (direction)
            {
                theRB.velocity = new Vector2(dashSpeed, theRB.velocity.y);

            }
            else
            {
                theRB.velocity = new Vector2(-dashSpeed, theRB.velocity.y);
            }

            dashing = true;
        }
    }
    void resetJumps()
    {
        curJumps = 0;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Terrain") //If the player touches Terrain then reset the jumps to allow the user to jump again.
        {
            resetJumps();
        }
        else
        {
            //Debug.Log(gameObject.tag);
            //Debug.Log(coll.gameObject.tag);
        }
            

    }
}
