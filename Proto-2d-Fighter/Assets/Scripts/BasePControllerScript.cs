using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePControllerScript : MonoBehaviour {
    // This Script is the equivalant of the PlayerBaseMovement script, but for using a controller instead of a keyboard and mouse

    public float curMoveSpeed = 0;
    public float walkSpeed = 1f;  //Standard walking speed
    public float dashSpeed = 10f;    //Standard dashing speed
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
    public GameObject aimsight;
    public Animator tor;
    //protected string curAnimation = "";
    //protected string animationToPref = "";


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

    public bool walkRight = false;
    public bool walkLeft = false;
    public bool initalDashState = false;
    public bool shouldJump = false;


    /// <summary>
    /// Working on getting the jumping animation for blast to work on y velosity increasing and 
    /// when jump is set to true, and the fall when y is negative. Idk about the xaxis when running.
    /// I am using this as a jumping off point. https://answers.unity.com/questions/1393229/how-can-i-detect-if-an-object-is-accelerating-or-d.html
    /// if it doesnt work then maybe use the usual animation way to get the jump and fall animations to work
    /// 
    /// </summary>

    protected Vector3 lastPositionY;
    protected Vector3 lastVelocityY;
    protected Vector3 lastAccelerationY;

    // Use this for initialization
    void Start() {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        RightThumbStickDir = Vector3.zero;
        aimsight = GameObject.Find("aimPosition");
    }

    // Update is called once per frame
    void Update()
    {
        IsMoving();
        UpdateAim();
    }

    void FixedUpdate()
    {
        preformBaseMovement();
    }

    private void LateUpdate()
    {
        //if(theRB.transform.position.x.)
    }

    protected void IsMoving()
    {
        preformBaseMovementLogic();
        sideDashCheck();
        tapTimeCheck();
        jumpCheck();
    }

    private void preformBaseMovementLogic()
    {
        secondToLastlStickHorizontal = lastlStickHorizontal;
        lastlStickHorizontal = lStickHorizontal;
        lStickHorizontal = Input.GetAxis("Horizontal");
        //Debug.Log(yRotation);

        if (((lastlStickHorizontal > 0) || (lastlStickHorizontal < 0)) && !dashing)
        {
            if (lastlStickHorizontal > 0)
            {
                walkRight = true;
                walkLeft = false;
                tor.SetBool("Walking", true);
            }
            else if (lastlStickHorizontal < 0)
            {
                walkLeft = true;
                walkRight = false;
                tor.SetBool("Walking", true);
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
                    walkRight = true;
                    walkLeft = false;
                    tor.SetBool("Walking", true);
                    //moveRight(dashSpeed, false);
                }
                else if (lastlStickHorizontal < 0)
                {
                    walkLeft = true;
                    walkRight = false;
                    tor.SetBool("Walking", true);
                    //moveLeft(dashSpeed, false);
                }
            }
        }
        else
        {
            walkRight = false;
            walkLeft = false;
            initalDashState = false;
            tor.SetBool("Walking", false); //Removes the walking animation
            curMoveSpeed = 0;

        }
    }

    protected void preformBaseMovement()
    {
        moveRight(dashSpeed, false);
        moveLeft(dashSpeed, false);
        moveDash(facingRight);
        jumping(shouldJump);

    }

    public void moveRight(float moveSpeed, bool walking)
    {
        if (walkRight)
        {
            //theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);  //Makes the player walk right
            //Below makes the player walk right BUT It does it by progrissivly applying force
            //This addition of force over time helps make the transition from blasting and using other
            //Physics manipulations and walking smoother
            //The if Statment will increase the curMoveSpeed and then check it, if it is above dashSpeed it will drop it to equal dashSpeed
            if ((curMoveSpeed += moveSpeed) > dashSpeed)
            {
                curMoveSpeed = dashSpeed;
            }
            if (theRB.velocity.magnitude < dashSpeed)
            {
                theRB.AddForce(new Vector2(curMoveSpeed, theRB.velocity.y));
            }
            //Debug.Log(theRB.velocity);


            transform.localRotation = Quaternion.Euler(0, 0, 0);        //Rotates a players model right
            facingRight = true;                                         //Sets the facing right bool to true;

        }

    }
    public void moveLeft(float moveSpeed, bool walking)
    {
        if (walkLeft)
        {
            //theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            //Below makes the player walk right BUT It does it by progrissivly applying force
            //This addition of force over time helps make the transition from blasting and using other
            //Physics manipulations and walking smoother
            //The if Statment will increase the curMoveSpeed and then check it, if it is above dashSpeed it will drop it to equal dashSpeed
            if ((curMoveSpeed += moveSpeed) > dashSpeed)
            {
                curMoveSpeed = dashSpeed;
            }
            if (theRB.velocity.magnitude < dashSpeed)
            {
                theRB.AddForce(new Vector2(-curMoveSpeed, theRB.velocity.y));
            }
            //Debug.Log(theRB.velocity);


            transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;

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
        //DASHING SCRIPT
        if ((pushTotal == 2) && (timeDelay < .5)) //If its withinh the time limit and the player taps the key a second time, increase the speed and set dash to true
        {
            //Moved to fixed update call in the new moveDash() function

            //if (direction)
            //{
            //    //theRB.velocity = new Vector2(dashSpeed, theRB.velocity.y);
            //    if (theRB.velocity.magnitude < dashSpeed)
            //    {
            //        theRB.AddForce(new Vector2(dashSpeed, theRB.velocity.y));
            //        curMoveSpeed = dashSpeed;
            //    }
            //    //Debug.Log(theRB.velocity);
            //}
            //else
            //{
            //    //theRB.velocity = new Vector2(-dashSpeed, theRB.velocity.y);
            //    if (theRB.velocity.magnitude < dashSpeed)
            //    {
            //        theRB.AddForce(new Vector2(dashSpeed, theRB.velocity.y));
            //        curMoveSpeed = dashSpeed;
            //    }
            //}

            dashing = true;
        }
    }
    private void moveDash(bool direction)
    {
        if (dashing)
        {
            
            if (direction)
            {
                if (initalDashState == false)
                {
                    // I am adding this non addforce change to theRB to get it instantly up to speed just once
                    theRB.velocity = new Vector2(dashSpeed, theRB.velocity.y);
                    initalDashState = true;
                }
                //theRB.velocity = new Vector2(dashSpeed, theRB.velocity.y);
                if (theRB.velocity.magnitude < dashSpeed)
                {
                    theRB.AddForce(new Vector2(dashSpeed, theRB.velocity.y));
                    curMoveSpeed = dashSpeed;
                }
                //Debug.Log(theRB.velocity);
            }
            else
            {
                if (initalDashState == false)
                {
                    theRB.velocity = new Vector2(-dashSpeed, theRB.velocity.y);
                    initalDashState = true;
                }
                //theRB.velocity = new Vector2(-dashSpeed, theRB.velocity.y);
                if (theRB.velocity.magnitude < dashSpeed)
                {
                    theRB.AddForce(new Vector2(-dashSpeed, theRB.velocity.y));
                    curMoveSpeed = dashSpeed;
                }
            }
        }

    }
    //////////////////////////////////////////////////////////////////////////////////////////
    // CAN ONLY JUMP ONCE FIGURE OUT WHY THE CUR JUMP CODE DOESNT WORK ANYMORE
    //////////////////////////////////////////////////////////////////////////////////////////
    private void jumpCheck()
    {
        // Input.GetKey(jump)) makes the character bounce, and has an interesting interaction with the blast. Keep in mind for later
        if (Input.GetKeyDown(jump)) //Checks for jump inputs
        {
            if (curJumps < maxJumps) //if the character has not already reached its max jump limit, jump and inc.
            {
                shouldJump = true;
                curJumps++;
            }

        }
    }
    private void jumping(bool jumpCheck)
    {
        if (jumpCheck)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            shouldJump = false;

        }
    }


    protected virtual void UpdateAim()
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
