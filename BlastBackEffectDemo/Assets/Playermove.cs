using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour {
    public KeyCode walkLeft;
    public KeyCode walkRight;
    public KeyCode Jump;

    private Rigidbody2D curRGB;
    // Use this for initialization
    void Start () {
        curRGB = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(walkLeft))
        {
            curRGB.AddForce(new Vector2(-10f, curRGB.velocity.y));
        }
        if (Input.GetKey(walkRight))
        {
            curRGB.AddForce(new Vector2(10f, curRGB.velocity.y));
        }
        if (Input.GetKey(Jump))
        {
            curRGB.AddForce(new Vector2(curRGB.velocity.x, 10f));
        }

    }
}
