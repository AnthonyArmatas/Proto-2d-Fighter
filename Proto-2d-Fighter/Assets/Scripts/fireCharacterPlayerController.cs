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

    private float curStagT;
    private float CurMoveSpeed;
    private float explosionCounter;

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

		CurMoveSpeed = moveSpeed;
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
        if (Input.GetKeyDown(movementKey))
        {
            if (charging == true)
            {

            }
            else
            {
                charging = true;
                explosionCounter = Time.deltaTime;
            }
        }
        else if (Input.GetKeyUp(movementKey))
        {
            charging = false;
            exploding = true;
            explosionCounter = Time.deltaTime - explosionCounter;
            Debug.Log(explosionCounter);
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
        if (exploding)
        {
            exploding = false;
            if(explosionCounter <= .5)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce/2);

            } else if (explosionCounter <= 1)
                {
                    

                } else if (explosionCounter <= 1.5)
                    {

                    } else if (explosionCounter >= 2)
                            {

                            }


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
}
