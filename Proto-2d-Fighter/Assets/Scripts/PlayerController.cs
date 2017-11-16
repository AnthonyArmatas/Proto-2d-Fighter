using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //I am going to make this class specifically a icemage controller class
    //and after this character is "Finished" I am going to strip it and make
    //A general controller class to use as a base for every other class.
    //For now it is only name player Controller.
    public float moveSpeed;
    public float jumpForce;
    public float fireRate;
    public float tForSprint;
    public bool walking = false;
    public bool sprinting = false;
    public bool gliding = false;


    private float nextFire;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode throwIce;
    public KeyCode momventKey;
    public Transform AttackSpawn;
    public Transform aimposition;
    public GameObject iceBolt;


    private Rigidbody2D theRB;
    private Animator tor;
    private Animation tion;
    private AimController AC;


    //AttackPoolScript instantiation
   // GameObject attackPool;
   // AttackPoolScript aps;


	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        tion = GetComponent<Animation>();
        AC = GetComponent<AimController>();
      //  attackPool = GameObject.Find("AttackSpawn"); ;
      //  aps = attackPool.GetComponent<AttackPoolScript>();
    }
	
	// Update is called once per frame
    void Update ()
    {
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
        }else if (Input.GetKey(left))
        {
            if (gliding == false)
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
            }
            if (gliding)
            {
                //Going to get the aimcontroller set up then add make the gliding functionality 
                //Run off of the position of the mouse
                //EX. if the mouse if aimed horizontally to the left and the left key is held
                //The character will glide at a steady speed. If the left key is held and the mouse is at
                // a 230 degree angle (from a 180 degree position) the character will speed up substantially. If the left key is held and the mouse is at
                // a 130 degree angle (from a 180 degree position) the movement will begin to slow untill it is a 0 velosity and then will plumet.
                //However, if it goes from a 180 degree pos to a 230 pos and then abruptly 130 and movement is cancelled, the character will be sent flying at 130 degrees.
                //theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                //transform.localRotation = Quaternion.Euler(0, 180, 0);

                //NEED TO CHANGE THESE PARAMETERS TO TAKE INTO ACCOUNT THAT THE ANGLE GOES FROM 0 (Usually 0) to 179 (179) to -179 (181) to -0 (360)
                if ((AC.angle >= 10 && AC.angle <= 80) || (AC.angle >= 100 && AC.angle <= 170))
                {
                    //-10 Velocity
                }
                else if (AC.angle >= 190 && AC.angle <= 350)
                {
                    //+10 Velocity
                }
                else if ((AC.angle >= 0 && AC.angle <= 9) || (AC.angle >= 169 && AC.angle <= 189) || (AC.angle >= 351 && AC.angle <= 360))
                {
                    //+-0 Velocity
                }
                    
            }
            else if (sprinting)
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
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            tor.SetBool("Walking", false);
            tor.SetBool("Gliding", true);
            walking = false;
            sprinting = false;
            gliding = true;
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
