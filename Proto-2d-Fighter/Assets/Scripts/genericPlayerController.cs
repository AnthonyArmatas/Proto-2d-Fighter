using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericPlayerController : MonoBehaviour {

	public float moveSpeed; //standard walking speed
	public float jumpForce;
	public float tForSprint;
    public float CurMoveSpeed;

    public bool walking = false;
	public bool sprinting = false;
	public bool isStaggered = false;
	public bool cantMove = false;

    private float whenStaggered;
    private float staggerTime = 0.5f;

    public KeyCode left;
	public KeyCode right;
	public KeyCode jump;
	//public KeyCode momventKey;
	public AimController AC;

	private Rigidbody2D theRB;
	private Animator tor;
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
        //Why are we checking the animator? Why not check the bool variables in the script?
		if (hs.isDead || isStaggered)
		{
			return;
		}
		if (Input.GetKey(left)) //Make a get direction method and have that method call another which will set these values to clear the clutter
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
                theRB.velocity = new Vector2(-moveSpeed / 2, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 180, 0);

            } else if (sprinting)
			{
				theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
				transform.localRotation = Quaternion.Euler(0, 180, 0);
			}
		}
		else if (Input.GetKey(right))
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
                theRB.velocity = new Vector2(moveSpeed / 2, theRB.velocity.y);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (sprinting)
			{
				theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
				transform.localRotation = Quaternion.Euler(0, 0, 0);
			}
		}
		else
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
}
