using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //I am going to make this class specifically a icemage controller class
    //and after this character is "Finished" I am going to strip it and make
    //A general controller class to use as a base for every other class.
    //For now it is only name player Controller.
    public float moveSpeed; //standard walking speed
    public float moveSpdIncrimentor;
    public float moveSpdCeiling;
    public float jumpForce;
    public float fireRate;
    public float tForSprint;
    public bool walking = false;
    public bool sprinting = false;
    public bool gliding = false;


    private float nextFire;
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
        Debug.Log(AC.angle);
        if (tor.GetBool("Dead") == true)
        {
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
        
    }

    //FixedUpdate is used instead of update b/c you use
    //FixedUpdate to update physics
    void FixedUpdate()
    {
        if (tor.GetBool("Dead") == true)
        {
            return;
        }
        if (gliding == true)
        {
            //Debug.Log(CurMoveSpeed);
            //Debug.Log(AC.angle);

            if (Input.GetKey(momventKey))
            {
                tor.SetBool("Gliding", false);
                walking = false;
                sprinting = false;
                gliding = false;
                return;            
            }
            if ((AC.angle >= 10 && AC.angle <= 80) || (AC.angle >= 100 && AC.angle <= 170)) // If the aim is pointing upward it will be faster
            {
                //CHANGE THE transform.localRotation = Quaternion.Euler(0, 0, 0); to The direction it is pointiing. IE. if (AC.angle >= 10 && AC.angle <= 80) then point change its rotation left 
                //~-10 Velocity 
                if (AC.angle >= 10 && AC.angle <= 80) //Facing Right
                {
                    if (CurMoveSpeed > 0)
                    {
                        if ((CurMoveSpeed -= moveSpdIncrimentor) <= 0)
                        {
                            CurMoveSpeed = 0;
                        }
                        else
                        {
                            CurMoveSpeed -= moveSpdIncrimentor;
                        }
                    }
                    theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                    theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.x);
                    //In transform.localRotation = Quaternion.Euler(0, 0, AC.angle); the change from 0 to AC.angle has the characters rotation match the position of the aim position
                    transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if (AC.angle >= 100 && AC.angle <= 170)//Facing Left
                {
                    if (CurMoveSpeed > 0)
                    {
                        if ((CurMoveSpeed -= moveSpdIncrimentor) <= 0)
                        {
                            CurMoveSpeed = 0;
                        }
                        else
                        {
                            CurMoveSpeed -= moveSpdIncrimentor;
                        }
                    }
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
            }
            else if (AC.angle > 80 && AC.angle < 100)// If the aim is stright up it drop down fast
            {
                //~-30 Velocity
                //It should lose all velocity and begin to fall in the opposite direction.
                //NEED TO CHANGE
                //It should be faceing up for a second before it starts to fall (Or making the increments and move speed slower makes this not matter)
                if (CurMoveSpeed > 0)
                {
                    if ((CurMoveSpeed -= moveSpdIncrimentor * 3) <= 0)
                    {
                        CurMoveSpeed = 0;
                    }
                    else
                    {
                        CurMoveSpeed -= moveSpdIncrimentor * 3;
                    }
                }
                //Xinstead of y so it falls
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.x);

            }
            else if ((AC.angle >= -170 && AC.angle <= -100) || (AC.angle >= -80 && AC.angle <= -10))// If the aim is pointing downward it will be faster
            {
                //~+10 Velocity

                theRB.velocity = new Vector2(0, theRB.velocity.y);
                if (AC.angle >= -80 && AC.angle <= -10) //Facing Right
                {
                    if (CurMoveSpeed < moveSpdCeiling)
                    {
                        if ((CurMoveSpeed += moveSpdIncrimentor) > moveSpdCeiling)
                        {
                            CurMoveSpeed = moveSpdCeiling;
                        }
                        else
                        {
                            CurMoveSpeed += moveSpdIncrimentor;
                        }
                    }
                    theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                    transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if (AC.angle >= -170 && AC.angle <= -100)//Facing Left
                {
                    if (CurMoveSpeed < moveSpdCeiling)
                    {
                        if ((CurMoveSpeed += moveSpdIncrimentor) > moveSpdCeiling)
                        {
                            CurMoveSpeed = moveSpdCeiling;
                        }
                        else
                        {
                            CurMoveSpeed += moveSpdIncrimentor;
                        }
                    }
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
            else if ((AC.angle > -100 && AC.angle < -80)) // If the aim is stright down it will be significantly faster
            {
                //~+20 Velocity
                if (CurMoveSpeed < moveSpdCeiling)
                {
                    if ((CurMoveSpeed += moveSpdIncrimentor * 2) > moveSpdCeiling)
                    {
                        CurMoveSpeed = moveSpdCeiling;
                    }
                    else
                    {
                        CurMoveSpeed += moveSpdIncrimentor * 2;
                    }
                }
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                if (AC.angle > -90)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, AC.angle);//Facing Right
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(1, 180, AC.angle);//Facing Left
                }
            }
            else if ((AC.angle >= 0 && AC.angle <= 9) || (AC.angle <= 0 && AC.angle >= -9) || (AC.angle >= 170 && AC.angle <= 180) || (AC.angle >= -180 && AC.angle <= -170))
            {// If the aim is pointing horizontally it will not change speed.r
                //+-0 Velocity
                if ((AC.angle >= 0 && AC.angle <= 9) || (AC.angle <= 0 && AC.angle >= -9))//Right
                {
                    if (CurMoveSpeed == 0)
                    {
                        CurMoveSpeed = moveSpeed;
                    }
                    theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                    transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if ((AC.angle >= 170 && AC.angle <= 180) || (AC.angle >= -180 && AC.angle <= -170))//Left
                {
                    if (CurMoveSpeed == 0)
                    {
                        CurMoveSpeed = moveSpeed;
                    }
                    theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.y);

                    float leftangle = AC.angle;
                    leftangle = Mathf.Abs(leftangle);
                    leftangle = 360 - (leftangle * 2);
                    //Debug.Log(leftangle);
                    //The left angle needs to be negative when pointing to to get the proper
                    //angle i.e. if it is pointing downward it needs to be negative so that the 
                    //sprite will also be pointed down.
                    transform.localRotation = Quaternion.Euler(1, 180, -leftangle);

                    //transform.localRotation = Quaternion.Euler(1, 180, AC.angle);
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
        }
        else if (Input.GetKey(momventKey))
        {
            tor.SetBool("Walking", false);
            tor.SetBool("Gliding", true);
            walking = false;
            sprinting = false;
            gliding = true;
            Debug.Log("MOVEMENT PRESSED");
        }
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
}
