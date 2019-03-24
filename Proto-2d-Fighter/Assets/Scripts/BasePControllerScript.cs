using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePControllerScript : MonoBehaviour {
    // This Script is the equivalant of the PlayerBaseMovement script, but for using a controller instead of a keyboard and mouse

    public float walkSpeed = 2.5f;  //Standard walking speed
    public float dashSpeed = 5f;    //Standard dashing speed
    public float aimAngle = 0;
    public bool dashing = false;    //Is dashing check
    public float jumpForce = 20f;   //Standard jump force 
    public int leftTotal = 0;       //Total times player taped left
    public int rightTotal = 0;      //Total times player taped right
    //Changing elapsed time to double from float because of https://randomascii.wordpress.com/2012/02/13/dont-store-that-in-a-float/ 
    public double leftTimeDelay = 0;    //Time delay after first tap left
    public double rightTimeDelay = 0;   //Time delay after first tap right
    public int curJumps = 0;        //Total number pf jumps a player has made
    public int maxJumps = 2;           //Max number of jumps a player can jump before hitting a piece of terrain
    //A bool the specific character classes have access to which disables usual movement when
    //Their special movement ability is enabled

    private float whenStaggered;
    private float staggerTime = 0.5f;
    private float whenSlowed;
    private float slowTime = 0.5f;

    public Rigidbody2D theRB;
    public Animator tor;
    public GameObject aimsight;

    public AimController AC;
    public HealthScript hs;

    //Ability buttons
    public KeyCode jump;

    //Left Thumbstick(Character Movement) variables
    public float yRotation;
    public float lastlStickHorizontal = 0;
    // This is used to determine dash checks. This needs to hit zero and lastlStickHorizontal needs to be higher or lower before a dash can occur
    public float secondToLastlStickHorizontal = 0;
    public float lStickHorizontal = 0;
    //Right Thumbstick(Character Aim) variables
    public Vector3 RightThumbStickDir;
    //SET FACING RIGHT AS TRUE FOR FRIST PLAYER IN STATIC START SCRIPT AND FALSE FOR SECOND PLAYER
    public bool facingRight;

    // Use this for initialization
    void Start() {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        RightThumbStickDir = Vector3.zero;
        aimsight = GameObject.Find("aimPosition");
    }

    // Update is called once per frame
    void Update() {
        IsMoving();
        UpdateAim();
    }

    protected void IsMoving()
    {
        preformBaseMovement();
        sideDashCheck();
        tapTimeCheck();
        jumpCheck();
    }
    private void preformBaseMovement()
    {
        secondToLastlStickHorizontal = lastlStickHorizontal;
        lastlStickHorizontal = lStickHorizontal;
        lStickHorizontal = Input.GetAxis("Horizontal");
        Debug.Log(yRotation);

        if (((lastlStickHorizontal > 0) || (lastlStickHorizontal < 0)) && !dashing)
        {
            if (lastlStickHorizontal > 0)
            {
                moveRight(walkSpeed, true);
            }
            else if (lastlStickHorizontal < 0)
            {
                moveLeft(walkSpeed, true);
            }
        }
        else if (dashing) //Checks to see if the player is/was dashing
        {
            if ((lastlStickHorizontal == 0))    //if the player was dashing but stoped pressing any keys, make the character stop 
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
                if (lastlStickHorizontal > 0)
                {
                    moveRight(dashSpeed, false);
                }
                else if (lastlStickHorizontal < 0)
                {
                    moveLeft(dashSpeed, false);
                }
            }
        }
        else
        {
            tor.SetBool("Walking", false); //Removes the walking animation
        }
    }

    public void moveRight(float moveSpeed, bool walking)
    {
        theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);  //Makes the player walk right
        transform.localRotation = Quaternion.Euler(0, 0, 0);        //Rotates a players model right
        facingRight = true;                                         //Sets the facing right bool to true;
        if (walking)
        {
            tor.SetBool("Walking", true);                               //Sets the players walking animation
        }
    }
    public void moveLeft(float moveSpeed, bool walking)
    {
        theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        facingRight = false;
        if (walking)
        {
            tor.SetBool("Walking", true);                               //Sets the players walking animation
        }
    }

    private void sideDashCheck()
    {
        if ((lastlStickHorizontal > 0)  && (secondToLastlStickHorizontal == 0))
        {
            if (leftTotal != 0)
            {
                //Comment this out to allow side dashing( i.e. right then left then right will have you run right. Not sure if I want this or not. Not sure why I wouldnt. but for now its off)
                leftTotal = 0;
            }
            rightTotal++;
        }
        if ((lastlStickHorizontal < 0) && (secondToLastlStickHorizontal == 0))
        {
            if (rightTotal != 0)
            {
                //Comment this out to allow side dashing( i.e. right then left then right will have you run right. Not sure if I want this or not. Not sure why I wouldnt. but for now its off)
                rightTotal = 0;
            }
            leftTotal++;
        }
    }

    private void tapTimeCheck()
    {
        belowTapTime(ref rightTotal, ref rightTimeDelay);
        belowTapTime(ref leftTotal, ref leftTimeDelay);

        overTapTime(ref rightTotal, ref rightTimeDelay);
        overTapTime(ref leftTotal, ref leftTimeDelay);

        withinTapTime(ref rightTotal, ref rightTimeDelay, facingRight);
        withinTapTime(ref leftTotal, ref leftTimeDelay, facingRight);
    }
    private void belowTapTime(ref int pushTotal, ref double timeDelay)
    {
        if ((pushTotal == 1) && (timeDelay < .5))   //If movement keys have been taps once and its not over the .5 time limit, increase the time limit
        {
            timeDelay += Time.deltaTime;
        }
    }
    private void overTapTime(ref int pushTotal, ref double timeDelay)
    {
        if ((pushTotal == 1) && (timeDelay >= .5))  //If its been over the time limit with out tapping the key a second time
        {
            timeDelay = 0;
            pushTotal = 0;
        }
    }
    private void withinTapTime(ref int pushTotal, ref double timeDelay, bool direction)
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

    private void jumpCheck()
    {
        if (Input.GetKeyDown(jump)) //Checks for jump inputs
        {
            if (curJumps < maxJumps) //if the character has not already reached its max jump limit, jump and inc.
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                curJumps++;
            }

        }
    }

    protected void UpdateAim()
    {
        RightThumbStickDir.y = Input.GetAxis("RightThumbStickVert") * 100;
        RightThumbStickDir.x = Input.GetAxis("RightThumbStickHor") * 100;

        aimAngle = Mathf.Atan2((Input.GetAxis("RightThumbStickVert")), Input.GetAxis("RightThumbStickHor")) * Mathf.Rad2Deg;
        if (aimAngle < 0)
        {
            aimAngle = aimAngle * -1;
        }
        else
        {
            aimAngle = 360 - aimAngle;
        }

        Debug.Log(aimsight.transform.GetChild(0).transform.rotation.eulerAngles);
        if (facingRight == true)
        {
            aimsight.transform.localEulerAngles = new Vector3(0f, 0f, aimAngle);
        }
        else
        {
            // If facing left its left rotation will be at 180degrees so the aim transformation needs to match it
            aimsight.transform.localEulerAngles = new Vector3(0f, -180f, aimAngle);
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
