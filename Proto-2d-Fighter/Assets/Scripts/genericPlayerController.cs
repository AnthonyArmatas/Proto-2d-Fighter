using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericPlayerController : MonoBehaviour {

    public int charClass;// { Ice = 1, Blast = 2, Swing = 3, Phase = 4} If it is 0, we know there was an error in passing in the character type
    public float moveSpeed = 5; //standard walking speed;
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
    private float whenSlowed;
    private float slowTime = 0.5f;

    public KeyCode left;
	public KeyCode right;
    public KeyCode movementKey;
    public KeyCode jump;
    public KeyCode attack;
    //public KeyCode movementKey;

    private Rigidbody2D theRB;
	private Animator tor;

    public AimController AC;
    public HealthScript hs;

    //Class Specific Variables: //Will try to consolidate and share variables in the future
    //Blast
    public bool charging = false;
    public bool exploding = false;
    public bool midPloding = false;

    private float explosionCounter;


    // Use this for initialization
    void Start () {
		theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
		hs = GetComponent<HealthScript>();

        CurMoveSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (hs.isDead || isStaggered)
		{
            if (isStaggered)
            {
                if ((Time.time - whenStaggered) >= staggerTime)
                {
                    isStaggered = false;
                    tor.SetBool("Stagger", false);
                    return;
                }
            }
                return;
        }
        else if (isSlowed)
        {
            if(Time.time - whenSlowed >= slowTime)
            {
                isSlowed = false;
                CurMoveSpeed = moveSpeed;
                return;
            }

            //return; //This effectivly made slow a stun, idk why it is here.
        }
        if (movAbltyEnabled)
        {
            moveSpecial();
        }
        if (Input.GetKey(attack))
        {
            playerAttack();
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
        else if (Input.GetKey(movementKey) || charging == true)
        {
            switch (charClass)
            {
                case 1:
                    toggleIce();
                    break;
                case 2:
                    updateToggleBlast();
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
        else if (movAbltyEnabled != true)
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            tor.SetBool("Walking", false);
            walking = false;
            sprinting = false;

        }
        if (Input.GetKeyDown(jump))
        {
            if (isSlowed)
            {
                //Instread of changing the velocity maybe adding force would be better.
                // Add force to the cloned object in the object's forward direction
                //clone.rigidbody.AddForce(clone.transform.forward * shootForce);
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce / 2);
            }
            else
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

        }

    }

	//FixedUpdate is used instead of update b/c you use
	//FixedUpdate to update physics
	void FixedUpdate() ///////////////NEED TO CHANGE THE GetKey to update and have it respond in FixedUpdate
	{
        /* if (Input.GetKey(movementKey)) //Added it twice to make try to assure it always goes off.
         {                             //https://answers.unity.com/questions/620981/input-and-applying-physics-update-or-fixedupdate.html
             toggleSpclMove();
         }*/
        if (hs.isDead || isStaggered)
        {
            if (isStaggered)
            {
                if ((Time.time - whenStaggered) >= staggerTime)
                {
                    isStaggered = false;
                    tor.SetBool("Stagger", false);
                    return;
                }
            }
            return;
        }
        else if (isSlowed)
        {
            if (Time.time - whenSlowed >= slowTime)
            {
                isSlowed = false;
                CurMoveSpeed = moveSpeed;
                return;
            }

            //return; //This effectivly made slow a stun, idk why it is here.
        }
        if (movAbltyEnabled)
        {
            moveSpecial();
        }
        if (Input.GetKey(attack))
        {
            playerAttack();
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
        else if (Input.GetKey(movementKey))
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
            if (isSlowed)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce / 2);
            }
            else
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

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
            theRB.velocity = new Vector2(-(CurMoveSpeed / 2), theRB.velocity.y); //This works functionally, but CurMoveSpeed never actually changes to so anything based on that will be in error if slowed.
            //Test to see if you can change the velocity with a vector from another file. You can.
            //IceMovementScript ims = new IceMovementScript();
            //ims.runTest(theRB, CurMoveSpeed);
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
            theRB.velocity = new Vector2((CurMoveSpeed / 2), theRB.velocity.y);//This works functionally, but CurMoveSpeed never actually changes to so anything based on that will be in error if slowed.
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

    public void playerAttack()
    {
        switch (charClass)
        {
            case 1:
                IceAttacktScript ias = new IceAttacktScript();
                ias = GetComponent<IceAttacktScript>();
                ias.launchIceBolt(tor);
                break;
            case 2:
                //toggleBlast();
                break;
            case 3:
                //toggleSwing();
                break;
            case 4:
                //togglePhase();
                break;
            default:
                return;
        }
    }

    public void iceMovement()
    {
        IceMovementScript ims = new IceMovementScript();
        ims.iceGlide(AC, theRB, CurMoveSpeed);
    }
    public void blastMovement()
    {
        BlastMovementScript bms = new BlastMovementScript();
        movAbltyEnabled = false;
        bms.BlastMove(AC, theRB, movementKey, explosionCounter);
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
                //toggleBlast();
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
    public void updateToggleBlast()
    {
        if (charging == true)
        {
            if (Input.GetKeyUp(movementKey))
            {
                charging = false;
                movAbltyEnabled = true;
            }
            else
            {
                explosionCounter += Time.deltaTime; //incriments the counterB
                // Debug.Log("Charging-eplosionCounter");
                // Debug.Log(explosionCounter);
            }
        }
        else
        {
            if (Input.GetKeyDown(movementKey)) // GetKeyDown tracks the intial press not that it is held down
            {
                charging = true;
                //Debug.Log("Pre-eplosionCounter");
                //Debug.Log(explosionCounter);
                explosionCounter = Time.deltaTime; //Starts the counter
                //Debug.Log("Post-explosionCounter");
                //Debug.Log(explosionCounter);
            }
        }
    }

    public void toggleBlast()
    {
        //if (charging == false)             //All of this functionality should be done in update not fixed update. Fixed update should only deal with the actual physics
        //{
        //    return;
        //}
        //else
        //{
        //    if (Input.GetKeyDown(movementKey)) // GetKeyDown tracks the intial press not that it is held down
        //    {
        //        Debug.Log(explosionCounter);
        //    }
        //    else if (Input.GetKeyUp(movementKey))
        //    {
        //        charging = false;
        //        blastMovement();
        //    }
        //}
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

    public void PlayerSlowed() 
    {
        if (isSlowed)
        {
            return;
        }
        else
        {
            if (hs.isDead != true)
            {
                isSlowed = true;
                whenSlowed = Time.time;
                CurMoveSpeed = moveSpeed / 2;
                //tor.SetBool("Slowed", true);
            }
        }
        return;

    }

}


//Known bugs:
//When Running if the player tries to activate the movement key, it does not go off.