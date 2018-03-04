using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMageControllerScript : MonoBehaviour {

    public float glideMoveSpd;
    public bool gliding;
    public bool cantMove = false;


    public float fireRateIceBall; //.5
    public float fireRateIceSpike; //1.5
    private float nextFire;


    public KeyCode throwIce;
    public KeyCode summonIceSpike;
    public KeyCode momventKey;
    public Transform attackSpawnIceBall;
    public Transform aimpositionIceBall;
    public Transform attackSpawnIceSpike;
    public Transform aimpositionIceSpike;
    public GameObject iceBolt;
    public GameObject iceSpike;
    public AimController AC;
    public IceSpikeControllerScript iceSpkContrlScpt;

    private Rigidbody2D theRB;
    private Animator tor;
    private HealthScript hs;
    private genericPlayerController gpc;

    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        hs = GetComponent<HealthScript>();
        gpc = GetComponent<genericPlayerController>();

    }
	
	// Update is called once per frame
	void Update () {
        if (hs.isDead || gpc.isStaggered) // Would it be better to access it through the gpc script or the hs?
        {
            return;
        }
        if (Input.GetKey(throwIce) || Input.GetKey(summonIceSpike))
        {
            //Think about making the rest of the attacks function like this. Not Sure how else to get it to take in mult.
            //Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.F)
            if (Input.GetKey(throwIce) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRateIceBall;
                //Added the aimposition rotation to the generation of the icebolt.
                //BUG: If the position is behind the mage the icebolt will hit himself.
                Instantiate(iceBolt, attackSpawnIceBall.position, aimpositionIceBall.rotation);
                tor.SetBool("Attack_IB", true);
                // aps.genAttack();
            }
            else if (Input.GetKey(summonIceSpike) && Time.time > nextFire)
            {
                //BUG:NEED TO MAKE THE SUMMONSPIKE ANIMATION MATCH THE CANTMOVE TIME
                cantMove = true;
                nextFire = Time.time + fireRateIceSpike;
                //Instantiate(iceSpike, attackSpawnIceSpike.position, aimpositionIceSpike.rotation);
                iceSpkContrlScpt.SpawnIceSpike(attackSpawnIceSpike);
                tor.SetBool("Attack_SB", true);
            }
            return;
        }

        if (gliding == true)
        {
            //Debug.Log(CurMoveSpeed);
            //Debug.Log(AC.angle);

            if (Input.GetKey(momventKey))
            {
                tor.SetBool("Gliding", false);
                theRB.gravityScale = 10;
                gpc.walking = false;
                gpc.sprinting = false;
                gliding = false;
                gpc.movAbltyEnabled = false;
                return;
            }
        }
        else if (Input.GetKey(momventKey))
        {
            tor.SetBool("Walking", false);
            tor.SetBool("Gliding", true);
            gpc.walking = false;
            gpc.sprinting = false;
            gliding = true;
            gpc.movAbltyEnabled = true;
            Debug.Log("MOVEMENT PRESSED");
        }

    }

    //FixedUpdate is used instead of update b/c you use
    //FixedUpdate to update physics
    void FixedUpdate()
    {
        //Make a get direction method and have that method call another which will set these values to clear the clutter
        if (hs.isDead || gpc.isStaggered)
        {
            return;
        }
        if (cantMove)
        {
            if (nextFire > Time.time)
            {
                return;
            }
            cantMove = false;
            //DELETES GAMEOBJECT INSTEAD OF DEACTIVATING IT. WHY!?
            //iceSpike.SetActive(false);
        }
        if(gliding == true)
        {
            gpc.CurMoveSpeed = glideMoveSpd;
            //Anywhere from 2-3 Thee is much faster than Two
            theRB.gravityScale = 3;
            //Debug.Log(CurMoveSpeed);
            //Debug.Log(AC.angle);

            if((AC.angle >= 0 && AC.angle <= 90) || (AC.angle >= 91 && AC.angle <= 180))
            {
                //CHANGE THE transform.localRotation = Quaternion.Euler(0, 0, 0); to The direction it is pointiing. IE. if (AC.angle >= -90 && AC.angle <= 90) then point change its rotation Right 
                //Facing Right
                if (AC.angle >= 0 && AC.angle <= 90) //Facing Right
                {
                    theRB.velocity = new Vector2(gpc.CurMoveSpeed, theRB.velocity.y);
                    theRB.velocity = new Vector2(gpc.CurMoveSpeed, theRB.velocity.x);
                    //In transform.localRotation = Quaternion.Euler(0, 0, AC.angle); the change from 0 to AC.angle has the characters rotation match the position of the aim position
                    transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if (AC.angle > 90 && AC.angle <= 180)//Facing Left
                {
                    //The y axis needs to be positive so it will go up
                    theRB.velocity = new Vector2(gpc.CurMoveSpeed, theRB.velocity.y);
                    theRB.velocity = new Vector2(-gpc.CurMoveSpeed, theRB.velocity.x);
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
            else if ((AC.angle >= -180 && AC.angle <= -90) || (AC.angle > -90 && AC.angle < 0))// If the aim is pointing downward it will be faster
            {
                if (AC.angle > -90 && AC.angle < 0) //Facing Right
                {
                    theRB.velocity = new Vector2(gpc.CurMoveSpeed, theRB.velocity.y);
                    transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if (AC.angle >= -180 && AC.angle <= -90)//Facing Left
                {

                    theRB.velocity = new Vector2(-gpc.CurMoveSpeed, theRB.velocity.y);

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
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            tor.SetBool("Gliding", false);
            gliding = false;
        }

    }


    }
