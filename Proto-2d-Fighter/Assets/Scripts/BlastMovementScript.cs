using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastMovementScript {
    //Trying to adapt the code from Fire Character Player Controller
    public bool charging = false;
    public bool exploding = false;

    private float explosionCounter;

    private int curArcPos;
    Vector3[] arcPos;

    //Movement Calc Variables
    public float blastVelocity;
    public int resolution = 10;  //At how many points in the arc we will calc. It will determine the smoothness of the arc
    float g; // Force of gravity on the y axis
    float radianAngle;

    public void BlastMove(AimController AC, Rigidbody2D theRB, KeyCode movementKey, float explosionCounter)
    {
        //charging = false;
        //explosionCounter = Time.deltaTime - explosionCounter;
        //Need to add a counter timer to get these to go off at the appropriate time.
        Debug.Log("Hit BlastMove");
        if (explosionCounter <= 1)
        {
            Debug.Log(explosionCounter);

            blastVelocity = 5;
            Debug.Log("jumped");
        }
        else if (explosionCounter <= 2)
        {
            blastVelocity = 10;
            Debug.Log("jumped double");
        }
        else if (explosionCounter >= 3)
        {
            blastVelocity = 15;
            Debug.Log("jumped triple");
        }



        //theRB.transform.localEulerAngles = new Vector3(10f, 10f, AC.angle);
        Vector2 tempVec = new Vector2(theRB.transform.position.x + 5f, theRB.transform.position.x + .05f);
        theRB.AddForce(tempVec);

        //Vector3[] lArcPos = CalculateArcArray(AC);
        //the arc positions are global and referred back to everytime a fixed update happens
        //in this file but I want them to be written over everytime a new arc needs to be made
        //arcPos = lArcPos;
        // curArcPos = 0;
        //Debug.Log("explosionCounter");
        //Debug.Log(explosionCounter);
        //explosionCounter = 0;


        exploding = true;

    }

    //Create an array of vector three positions for arc
    Vector3[] CalculateArcArray(AimController AC)
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * AC.angle;

        float maxDistance = (blastVelocity * blastVelocity * Mathf.Sin(2 * radianAngle) / g);

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }
        return arcArray;
    }

    //calculate height and distance of each vertex
    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * blastVelocity * blastVelocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x, y);
    }
}