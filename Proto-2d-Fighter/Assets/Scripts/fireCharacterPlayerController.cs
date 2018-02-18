using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireCharacterPlayerController : MonoBehaviour {

	public float moveSpeed; //standard walking speed
	public float jumpForce;
	public float tForSprint;
	public bool walking = false;
	public bool sprinting = false;
	public bool isStaggered = false;
	public float staggerTime;
	public bool cantMove = false;
    public bool charging = false;
    public bool exploding = false;
    public bool midPloding = false;

    private float curStagT;
    private float CurMoveSpeed;
    private float explosionCounter;
    private int curArcPos;
    Vector3[] arcPos;

    //Movement Calc Variables
    public float blastVelocity;
    public int resolution = 10;  //At how many points in the arc we will calc. It will determine the smoothness of the arc

    float g; // Force of gravity on the y axis
    float radianAngle;
    //

    public KeyCode left;
	public KeyCode right;
	public KeyCode jump;
	public KeyCode movementKey;
	public AimController AC;

	private Rigidbody2D theRB;
	private Animator tor;

	// Use this for initialization
	void Start () {
		theRB = GetComponent<Rigidbody2D>();
		tor = GetComponent<Animator>();
        g = Mathf.Abs(Physics2D.gravity.y);

		CurMoveSpeed = moveSpeed;
        //RenderArc();
	}

	// Update is called once per frame
	void Update () {
		if (tor.GetBool("Dead") == true)
		{
			return;
		}

        /* if (Input.GetKey(movement))
         {
             tor.SetBool("Walking", false);
             //tor.SetBool("Gliding", true);
             walking = false;
             sprinting = false;
             charging = true;
             Debug.Log("MOVEMENT PRESSED");
         }*/

        if (Input.GetKeyDown(movementKey)) // GetKeyDown tracks the intial press not that it is held down
        {
            if (charging == true)
            {
                Debug.Log(explosionCounter);

            }
            else
            {
                charging = true;
                explosionCounter += Time.deltaTime;
                Debug.Log(explosionCounter);
            }
        }   else if (Input.GetKeyUp(movementKey))
                {
                    charging = false;
                    //explosionCounter = Time.deltaTime - explosionCounter;
                    //Need to add a counter timer to get these to go off at the appropriate time.

                    if (explosionCounter <= 1)
                    {
                        blastVelocity = 5;
                        Debug.Log("jumped");
                    }
                    else if (explosionCounter <= 2)
                        {
                            blastVelocity = 10;
                            Debug.Log("jumped double");
                        }
                        else if (explosionCounter >= 3)
                            {
                                blastVelocity = 15;
                                Debug.Log("jumped triple");
                            }





                    Vector3[] lArcPos = CalculateArcArray();
                    //the arc positions are global and referred back to everytime a fixed update happens
                    //in this file but I want them to be written over everytime a new arc needs to be made
                    arcPos = lArcPos;
                    curArcPos = 0;
                    Debug.Log(explosionCounter);
                    explosionCounter = 0;
                    exploding = true;
                }

    }

    //FixedUpdate is used instead of update b/c you use
    //FixedUpdate to update physics; anything that needs to be applied to a rigidbody should happen in FixedUpdate
    void FixedUpdate()
	{
		if (tor.GetBool("Dead") == true || tor.GetBool("Stagger") == true)
		{
			return;
		}
        if (exploding)
        {
            Vector2 newTempPos = new Vector2(theRB.position.x + arcPos[curArcPos].x, theRB.position.y + arcPos[curArcPos].y);

            if ((midPloding == false) && (theRB.position != newTempPos) && (curArcPos != 0 ) )
            {
                //May consider using lerp in the "wrong way" to get it to slow down at the end
                theRB.position = Vector3.MoveTowards(theRB.position, newTempPos, blastVelocity);
                midPloding = true;
            }else if((midPloding == true) && ((Vector3)theRB.position != arcPos[curArcPos]))
            {
                return;
            } else if(theRB.position == newTempPos)
            {
                midPloding = false;
                curArcPos++;
                return;
            }
            /*
            if ((AC.angle >= 0 && AC.angle <= 90) || (AC.angle >= 91 && AC.angle <= 180))
            {
                //CHANGE THE transform.localRotation = Quaternion.Euler(0, 0, 0); to The direction it is pointiing. IE. if (AC.angle >= -90 && AC.angle <= 90) then point change its rotation Right 
                //Facing Right
                if (AC.angle >= 0 && AC.angle <= 90) //Facing Right
                {
                    //theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.x);
                    //In transform.localRotation = Quaternion.Euler(0, 0, AC.angle); the change from 0 to AC.angle has the characters rotation match the position of the aim position
                    //transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if (AC.angle > 90 && AC.angle <= 180)//Facing Left
                {
                    //The y axis needs to be positive so it will go up
                    //theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                    //theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.x);
                    //This is -leftangle is that the character is not upside down, bc the value will be negative.
                    //This changes the rotation of the z axis so that the character does not flip upsidedown and backwards when moving left.
                    //Honestly I got this answer intuativly, I need to go back to college or somthing to brush up on my math skills.
                    //float leftangle = AC.angle;
                    //leftangle = Mathf.Abs(leftangle);
                    //leftangle = 360 - (leftangle * 2);
                    //Debug.Log(leftangle);
                    //transform.localRotation = Quaternion.Euler(1, 180, leftangle);

                    //transform.localRotation = Quaternion.Euler(1, 180, AC.angle);
                }
            }
            else if ((AC.angle >= -180 && AC.angle <= -90) || (AC.angle > -90 && AC.angle < 0))// If the aim is pointing downward it will be faster
            {
                if (AC.angle > -90 && AC.angle < 0) //Facing Right
                {
                    if (explosionCounter <= .5)
                    {

                        theRB.velocity = new Vector2(theRB.position.x, (jumpForce / 2));
                        //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce / 2));
                        //theRB.velocity = new Vector2()
                        Debug.Log("jumped half");
                        Debug.Log(explosionCounter);
                    }
                    else if (explosionCounter <= 1)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce));
                        Debug.Log("jumped");
                    }
                    else if (explosionCounter <= 1.5)
                    {
                        //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce + (jumpForce / 2)));
                        Debug.Log("jumped double");
                    }
                    else if (explosionCounter >= 2)
                    {
                        //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce * jumpForce));
                        Debug.Log("jumped triple");
                    }



                    //theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                    //transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                }
                else if (AC.angle >= -180 && AC.angle <= -90)//Facing Left
                {

                    if (explosionCounter <= .5)
                    {

                        theRB.velocity = new Vector2(-theRB.position.x , (jumpForce / 2));
                        //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce / 2));
                        //theRB.velocity = new Vector2()
                        Debug.Log("jumped half");
                        Debug.Log(explosionCounter);
                    }
                    else if (explosionCounter <= 1)
                    {
                        theRB.velocity = new Vector2(-theRB.velocity.x, (jumpForce));
                        Debug.Log("jumped");
                    }
                    else if (explosionCounter <= 1.5)
                    {
                        //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce + (jumpForce / 2)));
                        Debug.Log("jumped double");
                    }
                    else if (explosionCounter >= 2)
                    {
                        //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce * jumpForce));
                        Debug.Log("jumped triple");
                    }
                    //float leftangle = AC.angle;
                    //leftangle = Mathf.Abs(leftangle);
                    //leftangle = 360 - (leftangle * 2);
                    //Debug.Log(leftangle);
                    //The left angle needs to be negative when pointing to to get the proper
                    //angle i.e. if it is pointing downward it needs to be negative so that the 
                    //sprite will also be pointed down.
                   // transform.localRotation = Quaternion.Euler(1, 180, -leftangle);


                }

            }
            ///////////////////////////////////////////////////////////////////////
            exploding = false;
            //////////////////////////////////////////////////////////////////////
            //Add below to each of the angle possibilities and change the velocityies to match the opposite positions they are facing.
            //i.e. (AC.angle >= 0 && AC.angle <= 90) will have the aim pointer facing the top right, make the explosion go down left and so on
            ///////////////////////////////////////////////////////////////////////
            /*if (explosionCounter <= .5)
            {
                
                theRB.velocity = new Vector2((theRB.position.x + attackSpawn.position.x), (theRB.position.y + attackSpawn.position.y));
                //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce / 2));
                //theRB.velocity = new Vector2()
                Debug.Log("jumped half");
                Debug.Log(explosionCounter);
            }
            else if (explosionCounter <= 1)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce));
                    Debug.Log("jumped");
                } else if (explosionCounter <= 1.5)
                    {
                        //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce + (jumpForce / 2)));
                        Debug.Log("jumped double");
                    } else if (explosionCounter >= 2)
                            {
                                //theRB.velocity = new Vector2(theRB.velocity.x, (jumpForce * jumpForce));
                                Debug.Log("jumped triple");
                            }*/
            ///////////////////////////////////////////////////////////////////////
            midPloding = false;
            exploding = false;

            explosionCounter = 0;
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
		else
		{
			theRB.velocity = new Vector2(0, theRB.velocity.y);
			tor.SetBool("Walking", false);
			tor.SetBool("Gliding", false);
			walking = false;
			sprinting = false;

		}
		if (Input.GetKeyDown(jump))
		{
			theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
		}
	}

	public void PlayerStagger()
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
		return;

	}

    //populating the LineRender witht the appropriate settings 
    void RenderArc()
    {
        Vector3[] arcPos = CalculateArcArray();

        
    }

    //Create an array of vector three positions for arc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * AC.angle;

        float maxDistance = (blastVelocity * blastVelocity * Mathf.Sin(2 * radianAngle) / g);

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }
        return arcArray;
    }

    //calculate height and distance of each vertex
    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * blastVelocity * blastVelocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x, y);
    }

}
