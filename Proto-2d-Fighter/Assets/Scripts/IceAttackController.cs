using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttackController : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed;
    //public Rigidbody player;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(transform.up * speed * 10);
        //rb.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

        rb.velocity = transform.right * speed;

       /* if (player.transform.rotation.y == 0)
        {xc 
        }
        else
        {
           // rb.velocity = transform. * speed;
        }*/
	}
	
}
