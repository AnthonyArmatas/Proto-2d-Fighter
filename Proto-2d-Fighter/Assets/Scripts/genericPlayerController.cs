using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericPlayerController : MonoBehaviour {

    public int charClass;// { Ice = 1, Blast = 2, Swing = 3, Phase = 4} If it is 0, we know there was an error in passing in the character type
    public float moveSpeed; //standard walking speed;
	public float jumpForce;
	public float tForSprint;
    public float CurMoveSpeed;

    public bool walking = false;
	public bool sprinting = false;
    public bool movAbltyEnabled = false;
    public bool isSlowed = false;
    public bool isStaggered = false;
    //A bool the specific character classes have access to which disables usual movement when
    //Their special movement ability is enabled

    private float whenStaggered;
    private float staggerTime = 0.5f;
    private float slowTime = 0.5f;

    public KeyCode left;
	public KeyCode right;
    public KeyCode momventKey;
    public KeyCode jump;
	//public KeyCode momventKey;

	private Rigidbody2D theRB;
	private Animator tor;

    public AimController AC;
    public HealthScript hs;

	// Use this for initialization
	void Start () {
		theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
		hs = GetComponent<HealthScript>();

        CurMoveSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (hs.isDead)
		{
			return;
        }
        else if (isStaggered)
        {
            if ((Time.time - whenStaggered) >= staggerTime)
            {
                isStaggered = false;
                tor.SetBool("Stagger", false);
                return;
            }

            return;
        }

    }

	//FixedUpdate is used instead of update b/c you use
	//FixedUpdate to update physics
	void FixedUpdate()
	{
		if (hs.isDead || isStaggered)
		{
			return;
		}
        if (movAbltyEnabled)
        {
            moveSpecial();
        }
        if (Input.GetKey(left) && (movAbltyEnabled != true)) //Make a get direction method and have that method call another which will set these values to clear the clutter
        {                                                    //The (movAbltyEnabled != true) should prevent bi-directional movement keys from occuring while the movement
                                                             //key is enabled but also allow for the code to walk to the movement key tri
            moveLeft();
        }
		else if (Input.GetKey(right) && (movAbltyEnabled != true))
		{
            moveRight();
		}
        else if (Input.GetKey(momventKey))
        {
            toggleSpclMove();
        }
		else if (movAbltyEnabled != true)
        {
			theRB.velocity = new Vector2(0, theRB.velocity.y);
			tor.SetBool("Walking", false);
			walking = false;
			sprinting = false;

		}
        if (Input.GetKeyDown(jump))
		{
			theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
		}
	}

    public void moveLeft()
    {
        if (sprinting == false)
        {
            if (walking == false)
            {
                tForSprint = Time.time;

                walking = true;
                tor.SetBool("Walking", true);
            }
            else if (Time.time >= tForSprint + 1.0f)
            {
                sprinting = true;
                walking = false;
            }
            theRB.velocity = new Vector2(-(CurMoveSpeed / 2), theRB.velocity.y);
            //Test to see if you can change the velocity with a vector from another file. You can.
            //IceMovementScript imc = new IceMovementScript();
            //imc.runTest(theRB, CurMoveSpeed);
            transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        else if (sprinting)
        {
            theRB.velocity = new Vector2(-(CurMoveSpeed), theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }


    }

    public void moveRight()
    {
        if (sprinting == false)
        {
            if (walking == false)
            {
                tForSprint = Time.time;

                walking = true;
                tor.SetBool("Walking", true);
            }
            else if (Time.time >= tForSprint + 1.0f)
            {
                sprinting = true;
                walking = false;
            }
            theRB.velocity = new Vector2((CurMoveSpeed / 2), theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (sprinting)
        {
            theRB.velocity = new Vector2((CurMoveSpeed), theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void moveSpecial() //Checks the characters class and then calls a method to calc their movement speed/direction
    {
        switch (charClass)
        {
            case 1:
                iceMovement();
                break;
            case 2:
                blastMovement();
                break;
            case 3:
                swingMovement();
                break;
            case 4:
                phaseMovement();
                break;
            default:
                return;
        }
    }

    public void iceMovement()
    {
        IceMovementScript imc = new IceMovementScript();
        imc.iceGlide(AC, theRB, CurMoveSpeed);
    }
    public void blastMovement()
    {

    }
    public void swingMovement()
    {

    }
    public void phaseMovement()
    {

    }

    public void toggleSpclMove() //Checks the characters class and then calls a method to turn their movement on or off
    {
        switch (charClass)
        {
            case 1:
                toggleIce();
                break;
            case 2:
                toggleBlast();
                break;
            case 3:
                toggleSwing();
                break;
            case 4:
                togglePhase();
                break;
            default:
                return;
        }
    }

    public void toggleIce() //Turns on or off the iceGlide
    {
        if (movAbltyEnabled)
        {
            tor.SetBool("Gliding", false);
            theRB.gravityScale = 10;
            walking = false;
            sprinting = false;
            movAbltyEnabled = false;
            return;
        }
        else
        {
            tor.SetBool("Walking", false);
            tor.SetBool("Gliding", true);
            walking = false;
            sprinting = false;
            movAbltyEnabled = true;
            theRB.gravityScale = 0;
            iceMovement();
        }
    }
    public void toggleBlast()
    {

    }
    public void toggleSwing()
    {

    }
    public void togglePhase()
    {

    }

    public void PlayerStagger()
	{
		if (isStaggered)
		{
			return;
		}
		else
		{
			if (hs.isDead != true)
			{
				isStaggered = true;
                whenStaggered = Time.time;
                tor.SetBool("Stagger", true);
			}
		}
		return;

	}

    public void PlayerSlowed() //Finish Fixing the slow effect. Have the Ice Attack Controller call this function like it does TakeDamage. SLowing it should make its movement speed half of what it usually is, and its jump height half
    {
        if (isSlowed)
        {
            return;
        }
        else
        {
            if (hs.isDead != true)
            {
                isStaggered = true;
                whenStaggered = Time.time;
                tor.SetBool("Stagger", true);
            }
        }
        return;

    }

}
