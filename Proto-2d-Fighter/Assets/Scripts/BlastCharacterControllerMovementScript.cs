using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BlastCharacterControllerMovementScript : BasePControllerScript
{
    public KeyCode movementKey;

    // fullFuelGage = 100
    public int fullFuelGage;
    public int curFuelGage = 10;

    public bool movAbltyEnabled = false;   
    public bool charging = false;
    public bool exploding = false;
    public GameObject blastPoint;
    public GameObject fwdHndR;
    public Rigidbody2D blastPointRGB;
    public SpriteRenderer bapSR;
    public SpriteRenderer fGuage;
    public Animator blastAnimator;

    public double tSinceLastReFuel;
    public double tSinceLastBlast;

    public BlastMovementControllerScript BMCS;

    private float explosionCounter;

    ///// <summary>
    ///// Working on getting the jumping animation for blast to work on y velosity increasing and 
    ///// when jump is set to true, and the fall when y is negative. Idk about the xaxis when running.
    ///// I am using this as a jumping off point. https://answers.unity.com/questions/1393229/how-can-i-detect-if-an-object-is-accelerating-or-d.html
    ///// if it doesnt work then maybe use the usual animation way to get the jump and fall animations to work
    ///// 
    ///// </summary>

    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        //tors = new BlastAnimationScript(GetComponent<Animator>());
        tor = GetComponent<Animator>();
        aimsight = GameObject.Find("BlastAimPosition").gameObject;
        RightThumbStickDir = Vector3.zero;
        blastPoint = GameObject.Find("BlastAttackSpawn");
        blastAnimator = blastPoint.GetComponent<Animator>();

        curFuelGage = fullFuelGage;
        fwdHndR = GameObject.Find("FwdHndRight");
        bapSR = GameObject.Find("BlastAimPosition").GetComponent<SpriteRenderer>();
        blastPointRGB = GameObject.Find("BlastAttackSpawn").GetComponent<Rigidbody2D>();
        BMCS = GetComponent<BlastMovementControllerScript>();
        fGuage = GameObject.Find("CurFuel").GetComponent<SpriteRenderer>();
        facingRight = true;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 position = transform.position;
        Vector3 velocity = (position - lastPositionY) / Time.deltaTime;
        Vector3 acceleration = (velocity - lastVelocityY) / Time.deltaTime;
        if (Mathf.Abs(acceleration.magnitude - lastAccelerationY.magnitude) < 0.01f)
        {
            Debug.Log("Tor is now idle");
            tor.SetBool("Falling", false);
            tor.SetBool("Jumping", false);
        }
        else if (acceleration.magnitude > lastAccelerationY.magnitude && (acceleration.magnitude - lastAccelerationY.magnitude) > .5f)
        {
            Debug.Log("Tor is now Jumping");
            tor.SetBool("Falling", false);
            tor.SetBool("Jumping", true);
        }
        else if (acceleration.magnitude <= lastAccelerationY.magnitude && (lastAccelerationY.magnitude - acceleration.magnitude) > .5f)
        {
            Debug.Log("Tor is now falling");
            tor.SetBool("Falling", true);
            tor.SetBool("Jumping", false);

        }
         
        UpdateAim();
        IsUsingSpecialMovement();

        lastAccelerationY = acceleration;
        lastVelocityY = velocity;
        lastPositionY = position;

    }
    void IsUsingSpecialMovement()
    {
        if (!movAbltyEnabled)
        {
            IsMoving();
        }
        
        
        BMCS.Blasting(theRB, movementKey, ref curFuelGage, fGuage);
        
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
        BMCS.blast(theRB, blastPoint, blastAnimator, ref tSinceLastBlast);
        preformBaseMovement();
        RegenerateFuel();
    }
    
    private void RegenerateFuel()
    {
        if(curFuelGage != 10)
        {
            if(tSinceLastReFuel >= 1 && tSinceLastBlast > 1)
            {
                tSinceLastReFuel = Time.deltaTime;
                UpdateReFuelGage(1f);
                curFuelGage += 1;
                if(curFuelGage > 10)
                {
                    curFuelGage = 10;
                }
                UpdateReFuelColor();
                return;
            }
            else
            {
                tSinceLastReFuel += Time.deltaTime;
            }
        } 
    }

    public void UpdateReFuelGage(float refillAmt)
    {
        if (curFuelGage < 10)
        {
            if ((refillAmt + curFuelGage) >= 10)
            {
                fGuage.transform.localScale = new Vector3(fGuage.transform.localScale.x, 1, fGuage.transform.localScale.z);
            }
            else
            {
                fGuage.transform.localScale = new Vector3(fGuage.transform.localScale.x, ((refillAmt + curFuelGage) / 10f), fGuage.transform.localScale.z);
            }
        }
    }

    public void UpdateReFuelColor()
    {
        //Below gets the color.g which is a decimal number effectivly making it 255 or its equiv
        // Then the passed in num (usually 1) multiplied by 25.5 (225 /10 is 25.5 so  1/10th)
        //Then take that number and divide it by 255 to get the decimal for the new color
        //float curCologG = fGuage.color.g * 255;
        //curCologG = curCologG + ((refillAmt) * 25.5);
        //fGuage.color = new Color(1, curCologG / 255, 0);

        float curCologG = ((float)curFuelGage * 25.5f) / 255;
        fGuage.color = new Color(1, curCologG, 0);
    }

    protected override void UpdateAim()
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
            aimsight.transform.localPosition = new Vector3(aimsight.transform.localPosition.x, aimsight.transform.localPosition.y, 1f);
            //fwdHndR.transform.localPosition = new Vector3(fwdHndR.transform.localPosition.x, fwdHndR.transform.localPosition.y, 0f);
            fGuage.transform.localPosition = new Vector3(fGuage.transform.localPosition.x, fGuage.transform.localPosition.y, -0.5f);
        }
        else
        {
            // If facing left its left rotation will be at 180degrees so the aim transformation needs to match it
            aimsight.transform.localEulerAngles = new Vector3(0f, -180f, aimAngle);
            aimsight.transform.localPosition = new Vector3(aimsight.transform.localPosition.x, aimsight.transform.localPosition.y, -1f);
            //fwdHndR.transform.localPosition = new Vector3(fwdHndR.transform.localPosition.x, fwdHndR.transform.localPosition.y, 0f);
            fGuage.transform.localPosition = new Vector3(fwdHndR.transform.localPosition.x, fGuage.transform.localPosition.y, 0.5f);

        }
    }
}
