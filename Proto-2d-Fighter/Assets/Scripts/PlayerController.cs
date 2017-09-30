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

    //AttackPoolScript instantiation
   // GameObject attackPool;
   // AttackPoolScript aps;


	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody2D>();
        tor = GetComponent<Animator>();
        tion = GetComponent<Animation>();
      //  attackPool = GameObject.Find("AttackSpawn"); ;
      //  aps = attackPool.GetComponent<AttackPoolScript>();
    }
	
	// Update is called once per frame
    void Update ()
    {
        if (tor.GetBool("Dead") == true)
        {
            return;
        }
        else if (Input.GetKey(throwIce) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(iceBolt, AttackSpawn.position, AttackSpawn.rotation);
            tor.SetBool("Attack_IB", true);
           // aps.genAttack();
        }
        
    }

    //FixedUpdate is used instead of update b/c you use
    //FixedUpdate to update physics
    void FixedUpdate()
    {
        if (tor.GetBool("Dead") == true)
        {
            return;
        }else if (Input.GetKey(left))
        {

            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            tor.SetBool("Walking", true);
        }
        else if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            tor.SetBool("Walking", true);
        }
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            tor.SetBool("Walking", false);
        }


        if (Input.GetKeyDown(jump))
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
    }
}
