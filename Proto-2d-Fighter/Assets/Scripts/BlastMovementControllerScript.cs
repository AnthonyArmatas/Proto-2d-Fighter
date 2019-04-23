using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastMovementControllerScript : MonoBehaviour {

    public bool charging = false;
    public bool exploding = false;
    public double chargeTime = 0;

    private float BlastPower;


    public void Blasting(Rigidbody2D theRB, KeyCode movementKey)
    {
        Debug.Log("Pre Charging");

        if (Input.GetKeyDown(movementKey) || Input.GetKey(movementKey))
        {
            Debug.Log("Charging");

            if (charging == false)
            {
                chargeTime = Time.deltaTime;
                charging = true;
            }
            else
            {
                chargeTime += Time.deltaTime;
            }
        }
        else if (Input.GetKeyUp(movementKey))
        {
            if (chargeTime <= .7f)
            {
                BlastPower = 10f;
                exploding = true;
            }
            else if (chargeTime > .7 && chargeTime < 2)
            {
                BlastPower = 15f;
                exploding = true;
            }
            else if (chargeTime >= 2)
            {
                BlastPower = 30f;
                exploding = true;
            }
            charging = false;
        }
        //if (Input.GetKey(movementKey))
        //{
        //    exploding = true;
        //}
        
        ////////////////HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEYYYYY REAAADDD MEEEE/////////////////////
        // Next time you code, try to change the way the player walks from changing their position
        // To adding force. Using both kinds feels janky, in that when you are blaster and move,
        // It stops the player mid flight
    }
	

    public void blast(Rigidbody2D theRB, GameObject blastPoint)
    {

        if (exploding == true)
        {
            //I just subtracted the world position of the blastpint from the blaster character to get the position of the thrust and then I modified the power by multiplication.
            theRB.AddForce(new Vector2((theRB.position.x - blastPoint.transform.position.x) * BlastPower, (theRB.position.y - blastPoint.transform.position.y) * BlastPower), ForceMode2D.Impulse);
            exploding = false;
            Debug.Log("Blasted");
            Debug.Log("BlastPower " + BlastPower);
        }
        //theRB.AddForceAtPosition(new Vector2(blastPoint.transform.localPosition.x + 5, blastPoint.transform.localPosition.y + 5),new Vector2(blastPoint.transform.localPosition.x, blastPoint.transform.localPosition.y));
        //theRB.velocity = new Vector2(jumpForce, jumpForce);
        //float thrust = 20.0f;
        //transform.position = new Vector3(0.0f, -2.0f, 0.0f);
        //apRGB.AddForce(transform.up * thrust);
        //theRB.AddForceAtPosition(new Vector2(theRB.velocity.x, 20f), apRGB.position);
        //theRB.velocity = transform.forward * thrust;
        //theRB.AddForceAtPosition(blastPoint.transform.forward * thrust, blastPoint.transform.forward);
        

    }
}
