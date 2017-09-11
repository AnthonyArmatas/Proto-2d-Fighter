using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    public float fireRate;

    private float nextFire;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode throwIce;
    public Transform AttackSpawn;
    public GameObject iceBolt;



    private Rigidbody2D theRB;
    private Animator tor;
    private Animation tion;


	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        tion = GetComponent<Animation>();
    }
	
	// Update is called once per frame
    void Update ()
    {
        if (Input.GetKey(throwIce) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(iceBolt, AttackSpawn.position, AttackSpawn.rotation);
        }
    }

    //FixedUpdate is used instead of update b/c you use
    //FixedUpdate to update physics
    void FixedUpdate()
    {
        if (Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            tor.SetBool("Walking", true);
            //tion.Stop("subZero_idle");
            //tion.Play("subZero_walking");
            // ani.SetBool("subZero_walking", true);
        }
        else if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            tor.SetBool("Walking", true);
            //tion.Stop("subZero_idle");
            //tion.Play("subZero_walking");
            //ani.SetBool("subZero_walking", true);
        }
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            tor.SetBool("Walking", false);
            //tion.Stop("subZero_walking");
            //tion.Play("subZero_idle");

        }


        if (Input.GetKeyDown(jump))
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
    }
}
