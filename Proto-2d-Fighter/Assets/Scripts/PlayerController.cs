using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //I am going to make this class specifically a icemage controller class
    //and after this character is "Finished" I am going to strip it and make
    //A general controller class to use as a base for every other class.
    //For now it is only name player Controller.
    public float moveSpeed; //standard walking speed
    //Changing moveSpdCeiling to glide speed and taking out the moveSpdIncrimentor
    //I like the idea of that kind of changing speeds but it is not playable, at least
    //At high speeds. DELETE THIS COMMENT ON NEXT COMMIT
    public float glideMoveSpd; 
    public float jumpForce;
    public float fireRate;
    public float tForSprint;
    public bool walking = false;
    public bool sprinting = false;
    public bool gliding = false;
    public bool isStaggered = false;
    public float staggerTime;


    private float nextFire;
    private float curStagT;
    private float CurMoveSpeed;


    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode throwIce;
    public KeyCode momventKey;
    public Transform AttackSpawn;
    public Transform aimposition;
    public GameObject iceBolt;
    public AimController AC;


    private Rigidbody2D theRB;
    private Animator tor;
    private Animation tion;


	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        tion = GetComponent<Animation>();
        //AC = GetComponent<AimController>();
        CurMoveSpeed = moveSpeed;
    }
	
	// Update is called once per frame
    void Update ()
    {
        //Debug.Log(AC.angle);
        if (tor.GetBool("Dead") == true)
        {
            return;
        }
        else if (isStaggered)
        {
            if(curStagT >= staggerTime)
            {
                isStaggered = false;
                tor.SetBool("Stagger", false);
                curStagT = 0;
                return;
            }
            curStagT += Time.time;
            Debug.Log(curStagT);

            return;
        }
        else if (Input.GetKey(throwIce) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //Added the aimposition rotation to the generation of the icebolt.
            //BUG: If the position is behind the mage the icebolt will hit himself.
            Instantiate(iceBolt, AttackSpawn.position, aimposition.rotation);
            tor.SetBool("Attack_IB", true);
            // aps.genAttack();
        }

        if (gliding == true)
        {
            //Debug.Log(CurMoveSpeed);
            //Debug.Log(AC.angle);

            if (Input.GetKey(momventKey))
            {
                tor.SetBool("Gliding", false);
                theRB.gravityScale = 10;
                walking = false;
                sprinting = false;
                gliding = false;
                return;
            }
        } else if (Input.GetKey(momventKey))
        {
            tor.SetBool("Walking", false);
            tor.SetBool("Gliding", true);
            walking = false;
            sprinting = false;
            gliding = true;
            Debug.Log("MOVEMENT PRESSED");
        }

    }

    //FixedUpdate is used instead of update b/c you use
    //FixedUpdate to update physics
    void FixedUpdate()
    {
        if (tor.GetBool("Dead") == true || tor.GetBool("Stagger") == true)
        {
            return;
        }
        if (gliding == true)
        {
            CurMoveSpeed = glideMoveSpd;
            //Anywhere from 2-3 Thee is much faster than Two
            theRB.gravityScale = 3;
            //Debug.Log(CurMoveSpeed);
            //Debug.Log(AC.angle);

        if ((AC.angle >= 0 && AC.angle <= 90) || (AC.angle >= 91 && AC.angle <= 180)) 
            {
            //CHANGE THE transform.localRotation = Quaternion.Euler(0, 0, 0); to The direction it is pointiing. IE. if (AC.angle >= -90 && AC.angle <= 90) then point change its rotation Right 
            //Facing Right
            if (AC.angle >= 0 && AC.angle <= 90) //Facing Right
                {
                    theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                    theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.x);
                    //In transform.localRotation = Quaternion.Euler(0, 0, AC.angle); the change from 0 to AC.angle has the characters rotation match the position of the aim position
                    transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if (AC.angle > 90 && AC.angle <= 180)//Facing Left
                {
                    //The y axis needs to be positive so it will go up
                    theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                    theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.x);
                    //This is -leftangle is that the character is not upside down, bc the value will be negative.
                    //This changes the rotation of the z axis so that the character does not flip upsidedown and backwards when moving left.
                    //Honestly I got this answer intuativly, I need to go back to college or somthing to brush up on my math skills.
                    float leftangle = AC.angle;
                    leftangle = Mathf.Abs(leftangle);
                    leftangle = 360 - (leftangle * 2);
                    //Debug.Log(leftangle);
                    transform.localRotation = Quaternion.Euler(1, 180, leftangle);
                    
                    //transform.localRotation = Quaternion.Euler(1, 180, AC.angle);
                }
            } else if ((AC.angle >= -180 && AC.angle <= -90) || (AC.angle > -90 && AC.angle < 0))// If the aim is pointing downward it will be faster
            {
                if (AC.angle > -90 && AC.angle < 0) //Facing Right
                {
                    theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                    transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if (AC.angle >= -180 && AC.angle <= -90)//Facing Left
                {

                    theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.y);

                    float leftangle = AC.angle;
                    leftangle = Mathf.Abs(leftangle);
                    leftangle = 360 - (leftangle * 2);
                    //Debug.Log(leftangle);
                    //The left angle needs to be negative when pointing to to get the proper
                    //angle i.e. if it is pointing downward it needs to be negative so that the 
                    //sprite will also be pointed down.
                    transform.localRotation = Quaternion.Euler(1, 180, -leftangle);
                    //transform.localRotation = Quaternion.Euler(1, 180, -AC.angle);
                    
                }

            }

            return;
                                 
        }
        if (Input.GetKey(left))
        {
            if (sprinting == false)
            {
                if (walking == false)
                {
                    tForSprint = Time.time;
                }
                walking = true;
                theRB.velocity = new Vector2(-moveSpeed / 2, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                tor.SetBool("Walking", true);
            }
            if (sprinting)
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Time.time >= tForSprint + 1.0f)
            {
                sprinting = true;
                walking = false;
            }
        }
        else if (Input.GetKey(right))
        {
            if (sprinting == false)
            {
                if (walking == false)
                {
                    tForSprint = Time.time;
                }
                walking = true;
                theRB.velocity = new Vector2(moveSpeed / 2, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                tor.SetBool("Walking", true);
            }
            if (sprinting)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Time.time >= tForSprint + 1.0f)
            {
                sprinting = true;
                walking = false;
            }
        }/*
        else if (Input.GetKey(momventKey))
        {
            tor.SetBool("Walking", false);
            tor.SetBool("Gliding", true);
            walking = false;
            sprinting = false;
            gliding = true;
            Debug.Log("MOVEMENT PRESSED");
        }*/
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            tor.SetBool("Walking", false);
            tor.SetBool("Gliding", false);
            walking = false;
            sprinting = false;
            gliding = false;

        }


        if (Input.GetKeyDown(jump))
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
    }

    public void playerStagger()
    {
        if (isStaggered)
        {
            return;
        }
        else
        {
            if (tor.GetBool("Dead") != true)
            {
                isStaggered = true;
                tor.SetBool("Stagger", true);
            }
        }
        if (gliding == true)
        {
            tor.SetBool("Gliding", false);
            theRB.gravityScale = 10;
            walking = false;
            sprinting = false;
            gliding = false;
            return;
        }
        return;

    }

}
