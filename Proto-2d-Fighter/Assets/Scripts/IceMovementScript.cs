using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMovementScript : MonoBehaviour {

    public void iceGlide(AimController AC, Rigidbody2D theRB, float CurMoveSpeed)
    {
        //Glide angle topright = 1, topLeft = 2, bottomLeft = 3, bottomRight = 4, error = 0
        int glideAngle = getGlideAngle(AC);

        switch (glideAngle)
        {
            case 1: //RIGHT
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.x);
                theRB.transform.localRotation = Quaternion.Euler(0, 0, AC.angle);

                break;
            case 2: //LEFT
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.y);
                theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.x);
                theRB.transform.localRotation = Quaternion.Euler(180, 0, -AC.angle);
                break;
            case 3: //LEFT
                theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.y);
                theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.x);
                theRB.transform.localRotation = Quaternion.Euler(180,0,-AC.angle);
                break;
            case 4: //RIGHT
                theRB.velocity = new Vector2(-CurMoveSpeed, theRB.velocity.y);
                theRB.velocity = new Vector2(CurMoveSpeed, theRB.velocity.x);
                theRB.transform.localRotation = Quaternion.Euler(0, 0, AC.angle);
                break;
            default:
                return;
        }
    }


    public int getGlideAngle(AimController AC)
    {
        //Glide angle topright = 1, topLeft = 2, bottomLeft = 3, bottomRight = 4, error = 0
        if (AC.angle >= 0 && AC.angle <= 90)//Facing Right
        {
            return 1;
        }
        else if (AC.angle > 90 && AC.angle <= 180)//Facing Left
        {
            return 2;
        }
        else if (AC.angle > 180 && AC.angle <= 270)//Facing Left
        {
            return 3;
        }
        else if (AC.angle > 270 && AC.angle <= 360)//Facing Right
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }


    //Test to see if you can change the velocity with a vector from another file. You can.
    /*    public void runTest(Rigidbody2D theRB, float CurMoveSpeed)
        {
            theRB.velocity = new Vector2(-(CurMoveSpeed / 2), theRB.velocity.y);
        }
        */
}
