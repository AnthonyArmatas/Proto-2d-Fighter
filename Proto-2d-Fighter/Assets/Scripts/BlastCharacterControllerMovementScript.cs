using System.Collections;
using System.Collections.Generic;
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
    public Rigidbody2D blastPointRGB;
    public Rigidbody2D apRGB;
    public SpriteRenderer fGuage;

    public double tSinceLastReFuel;
    public double tSinceLastBlast;

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
        fGuage = GameObject.Find("CurFuel").GetComponent<SpriteRenderer>();
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
        BMCS.blast(theRB, blastPoint, ref tSinceLastBlast);
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
}
