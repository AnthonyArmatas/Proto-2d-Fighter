using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttackController : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed;
    //public Rigidbody player;

	// Use this for initialization
	void Start () {
        //Gets the object(Usually an Iceball) and applies velocity to it making it move 
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

	}

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("TakeIceDamage");
            DestroyObject(this.gameObject);
        }
        /*else if (coll.gameObject.tag == "Slow")
        {

        }*/
        else
            Debug.Log(gameObject.tag);

    }
}
