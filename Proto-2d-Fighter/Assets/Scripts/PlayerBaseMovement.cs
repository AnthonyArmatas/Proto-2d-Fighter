using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseMovement : MonoBehaviour {

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    public float walkSpeed = 2.5f; //standard walking speed;
    public float dashSpeed = 5f;
    public bool dashing = false;
    public float jumpForce = 20f;
    public int leftTotal = 0;
    public int rightTotal = 0;
    public float leftTimeDelay = 0f;
    public float rightTimeDelay = 0f;

    private bool facingRight = true;


    private Rigidbody2D theRB;
    private Animator tor;


    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //Tap the right key, the character will run right
        /*if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(walkSpeed, theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }*/
        if ((Input.GetKey(right) || Input.GetKey(left)) && !dashing)
        {
            if (Input.GetKey(right))
            {
                theRB.velocity = new Vector2(walkSpeed, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                facingRight = true;
                tor.SetBool("Walking", true);
            }
            else
            {
                theRB.velocity = new Vector2(-walkSpeed, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                facingRight = false;
                tor.SetBool("Walking", true);
            }
            
        }
        else if (dashing)
        {
            if (!Input.GetKey(right) && !Input.GetKey(left))
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
                if (Input.GetKey(right))
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
            tor.SetBool("Walking", false);
        }

        if (Input.GetKeyDown(right))
        {
            if(leftTotal != 0)
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

        if (Input.GetKeyDown(jump))
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

    }

    void belowTapTime(ref int pushTotal, ref float timeDelay)
    {
        if ((pushTotal == 1) && (timeDelay < .5))
        {
            timeDelay += Time.deltaTime;
        }
    }
    void overTapTime(ref int pushTotal, ref float timeDelay)
    {
        if ((pushTotal == 1) && (timeDelay >= .5))
        {
            timeDelay = 0;
            pushTotal = 0;
        }
    }
    void withinTapTime(ref int pushTotal, ref float timeDelay, bool direction)
    {
        if ((pushTotal == 2) && (timeDelay < .5))
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
}
