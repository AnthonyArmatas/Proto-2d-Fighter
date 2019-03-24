using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Dev_Script : MonoBehaviour
{
    public float yRotation;
    public float lookSenitivity = 5;
    public float lastlStickHorizontal = 0;
    public float lStickHorizontal = 0;
    public bool facingRight;    //A bool to use as a check for the current direction the player is facing. I dont think I am using it right now. I may use it later or remove it entirely.
    public float walkSpeed = 2.5f;  //Standard walking speed

    public Vector3 RightThumbStickDir;
    private GameObject aimsight;


    public Rigidbody2D theRB;
    public Animator tor;


    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        RightThumbStickDir = Vector3.zero;
        aimsight = GameObject.Find("aimPosition");
    }
	
	// Update is called once per frame
	void Update () {
        lastlStickHorizontal = lStickHorizontal;
        lStickHorizontal = Input.GetAxis("Horizontal");

        Debug.Log(yRotation);
        if (lastlStickHorizontal > 0)
        {
            theRB.velocity = new Vector2(walkSpeed, theRB.velocity.y);  //Makes the player walk right
            transform.localRotation = Quaternion.Euler(0, 0, 0);        //Rotates a players model right
            facingRight = true;                                         //Sets the facing right bool to true;
            tor.SetBool("Walking", true);                               //Sets the players walking animation
        }
        else if(lastlStickHorizontal < 0)
        {
            theRB.velocity = new Vector2(-walkSpeed, theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
            tor.SetBool("Walking", true);
        }
        Debug.Log(yRotation);
        GetRightThumbstick();
    }

    void GetRightThumbstick()
    {
        /////////////////////////////////////////////////////////////////////////
        //Below is how to get 360 degrees with a thumbstick using get axis
        //RightThumbStickDir.y = Input.GetAxis("RightThumbStickVert") * 100;

        //RightThumbStickDir.x = Input.GetAxis("RightThumbStickHor") * 100;

        //float angle = Mathf.Atan2((Input.GetAxis("RightThumbStickVert")), Input.GetAxis("RightThumbStickHor")) * Mathf.Rad2Deg;
        //if (angle < 0)
        //{
        //    angle = angle * -1;
        //}
        //else
        //{
        //    angle = 360 - angle;
        //}
        /////////////////////////////////////////////////////////////////////////////
        RightThumbStickDir.y = Input.GetAxis("RightThumbStickVert") * 100;

        RightThumbStickDir.x = Input.GetAxis("RightThumbStickHor") * 100;

        float angle = Mathf.Atan2((Input.GetAxis("RightThumbStickVert")), Input.GetAxis("RightThumbStickHor")) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle = angle * -1;
        }
        else
        {
            angle = 360 - angle;
        }

        //Debug.Log((angle));
        //NEED TO HAVE CHECK TO SEE IF THE PARENT ROTATION IS FACING THE OPPOSITE WAY TO MAKE SURE THE AIM IS NOT BACKWARDS.
        Debug.Log(aimsight.transform.GetChild(0).transform.rotation.eulerAngles);
        if(facingRight == true)
        {
            aimsight.transform.localEulerAngles = new Vector3(0f, 0f, angle);
        }
        else
        {
            aimsight.transform.localEulerAngles = new Vector3(0f, 180f, angle);
        }
        


    }

}
