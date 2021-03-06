﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {
    /*
     * Try to use the anchor of the players position and the mouse to make the end point of the players aim be the lerp
    The basic operation of linear interpolation between two values is commonly used in computer graphics.
     * In that field's jargon it is sometimes called a lerp. The term can be used as a verb or noun for the operation. 
     * e.g. "Bresenham's algorithm lerps incrementally between the two endpoints of the line."
     https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/LinearInterpolation.svg/300px-LinearInterpolation.svg.png
     */

    public Transform playerPosition;
    public Transform tipOfAim;
    private Vector2 mousePos;
    private Vector2 rotationPosition;
    private Vector2 relative;
    public float angle;
    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Get the Screen positions of the player
         Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
         //Get the Screen position of the mouse
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);


        //To make it flip ( Have the reticle on the opposite side of the cursor)
        //Switch the position of the mouse and transform input in the angle function.
        //Currently if angle is at 180 - 360 degrees it appears as a neg 179 through .01.
        // to fix if neg then add it to 360. If it passes over 180 and jumps neg 179 , then
        //-179 + 360 = 181
        angle = AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);
        if (angle < 0)
        {
            angle = angle + 360;
        }
        
        //changes the rotation of the reticle, on the z axis, to have the end that is not anchored to the player to point towards the mouse.
        transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));   
	}

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
