using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCharacterControllerMovementScript : BasePControllerScript
{
    public KeyCode movementKey;

    public int fullFuelGage = 4;
    public int curFuelGage;

    public bool movAbltyEnabled = false;   
    public bool charging = false;
    public bool exploding = false;
    public GameObject blastPoint;
    public Rigidbody2D blastPointRGB;
    public Rigidbody2D apRGB;

    public BlastMovementControllerScript BMCS;

    private float explosionCounter;
    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        aimsight = GameObject.Find("aimPosition");
        RightThumbStickDir = Vector3.zero;
        blastPoint = GameObject.Find("AttackSpawn");
        curFuelGage = fullFuelGage;
        apRGB = GameObject.Find("aimPosition").GetComponent<Rigidbody2D>();
        blastPointRGB = GameObject.Find("AttackSpawn").GetComponent<Rigidbody2D>();
        BMCS = GetComponent<BlastMovementControllerScript>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateAim();
        IsUsingSpecialMovement();
    }
    void IsUsingSpecialMovement()
    {
        if (!movAbltyEnabled)
        {
            IsMoving();
        }
        
        
        BMCS.Blasting(theRB, movementKey);
        
        //if (Input.GetKey(movementKey))
        //{
        //    exploding = true;
        //}

    }

    private void isCharging()
    {

    }
    private void FixedUpdate ()
    {
        BMCS.blast(theRB, blastPoint);
        preformBaseMovement();
    }

    //private void blast()
    //{
    //    if(exploding == true)
    //    {
    //        //I just subtracted the world position of the blastpint from the blaster character to get the position of the thrust and then I modified the power by multiplication.
    //        theRB.AddForce(new Vector2((theRB.position.x - blastPoint.transform.position.x)* 2f, (theRB.position.y - blastPoint.transform.position.y) * 2f), ForceMode2D.Impulse);
    //        exploding = false;
    //    }
    //    //theRB.AddForceAtPosition(new Vector2(blastPoint.transform.localPosition.x + 5, blastPoint.transform.localPosition.y + 5),new Vector2(blastPoint.transform.localPosition.x, blastPoint.transform.localPosition.y));
    //    //theRB.velocity = new Vector2(jumpForce, jumpForce);
    //    float thrust = 20.0f;
    //    //transform.position = new Vector3(0.0f, -2.0f, 0.0f);
    //    //apRGB.AddForce(transform.up * thrust);
    //    //theRB.AddForceAtPosition(new Vector2(theRB.velocity.x, 20f), apRGB.position);
    //    //theRB.velocity = transform.forward * thrust;
    //    //theRB.AddForceAtPosition(blastPoint.transform.forward * thrust, blastPoint.transform.forward);
    //    Debug.Log("Blasted");

    //}
}
