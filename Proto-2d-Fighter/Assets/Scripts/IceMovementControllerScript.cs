using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMovementControllerScript : MonoBehaviour {

    public void iceGlide(float aimAngle, Rigidbody2D theRB, float CurMoveSpeed, GameObject aimsight)
    {
        //Glide angle topright = 1, topLeft = 2, bottomLeft = 3, bottomRight = 4, error = 0
        int glideAngle = getGlideAngle(aimAngle);

        switch (glideAngle)
        {
            case 1: //RIGHT
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.x);
                theRB.transform.localRotation = Quaternion.Euler(0, 0, aimAngle);
                // Below makes it so that when faceing left and using the movement ability
                // if you turn right with the aim controller, the aim controller will work as well
                aimsight.transform.localEulerAngles = new Vector3(0f, 1f, aimAngle);

                break;
            case 2: //LEFT
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.x);
                theRB.transform.localRotation = Quaternion.Euler(180, 0, -aimAngle);
                // Below makes it so that when faceing right and using the movement ability
                // if you turn left with the aim controller, the aim controller will work as well
                aimsight.transform.localEulerAngles = new Vector3(180, -180f, -aimAngle);
                break;
            case 3: //LEFT
                theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.y);
                theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.x);
                theRB.transform.localRotation = Quaternion.Euler(180, 0, -aimAngle);
                // Below makes it so that when faceing right and using the movement ability
                // if you turn left with the aim controller, the aim controller will work as well
                aimsight.transform.localEulerAngles = new Vector3(180, -180f, -aimAngle);
                break;
            case 4: //RIGHT
                theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.y);
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.x);
                theRB.transform.localRotation = Quaternion.Euler(0, 0, aimAngle);
                // Below makes it so that when faceing left and using the movement ability
                // if you turn right with the aim controller, the aim controller will work as well
                aimsight.transform.localEulerAngles = new Vector3(0f, -1f, aimAngle);
                break;
            default:
                return;
        }
    }
    public int getGlideAngle(float aimAngle)
    {
        //Glide angle topright = 1, topLeft = 2, bottomLeft = 3, bottomRight = 4, error = 0
        if (aimAngle >= 0 && aimAngle <= 90)//Facing Right
        {
            return 1;
        }
        else if (aimAngle > 90 && aimAngle <= 180)//Facing Left
        {
            return 2;
        }
        else if (aimAngle > 180 && aimAngle <= 270)//Facing Left
        {
            return 3;
        }
        else if (aimAngle > 270 && aimAngle <= 360)//Facing Right
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }
}
