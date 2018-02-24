//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class IceAbilityScript : MonoBehaviour {

    public float glideMoveSpd;
    public bool gliding;
    public bool cantMove = false;


    public float fireRateIceBall; //.5
    private float nextFire;

    public KeyCode shootIce;
    public KeyCode momventKey;


    private Rigidbody2D theRB;
    private Animator tor;
    private HealthScript hs;
    private genericPlayerController gpc;

    public Transform iceBolt;

    // Use this for initialization
    void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        hs = GetComponent<HealthScript>();
        gpc = GetComponent<genericPlayerController>();

        glideMoveSpd = gpc.moveSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (hs.isDead || gpc.isStaggered) // Would it be better to access it through the gpc script or the hs?
        {
            return;
        }
        attackCheck();
        glideCheck();
    }

    //FixedUpdate is used instead of update b/c you use
    //FixedUpdate to update physics
    private void FixedUpdate()
    {
        //Need to add the fixed update changes to this and then tweak the gliding mechanics. 
    }


    public void attackCheck()
    {
        if (Input.GetKey(shootIce))
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRateIceBall;
                Instantiate(iceBolt,this.gameObject.transform.GetChild(0).GetChild(0).position , this.gameObject.transform.GetChild(0).rotation);
                tor.SetBool("Attack_IB", true);
            }
        }
    }

    public void glideCheck()
    {
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
}
