using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastMovementControllerScript : MonoBehaviour {

    public bool charging = false;
    public bool exploding = false;
    public double chargeTime = 0;

    private float BlastPower;


    public void Blasting(Rigidbody2D theRB, KeyCode movementKey, ref int CurFuelGage, SpriteRenderer fGuage)
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
            if (chargeTime <= .7f && CurFuelGage >= 1)
            {
                UpdateFuelAmount(ref CurFuelGage, 1);
                UpdateSpendingFuelGage(fGuage, 1f, CurFuelGage);
                BlastPower = 10f;
                exploding = true;
            }
            else if (chargeTime > .7 && chargeTime < 2 && CurFuelGage >= 2)
            {
                UpdateFuelAmount(ref CurFuelGage, 2);
                UpdateSpendingFuelGage(fGuage, 2f, CurFuelGage);
                BlastPower = 15f;
                exploding = true;
            }
            else if (chargeTime >= 2 && CurFuelGage >= 30)
            {
                UpdateFuelAmount(ref CurFuelGage, 3);
                UpdateSpendingFuelGage(fGuage, 3f, CurFuelGage);
                BlastPower = 30f;
                exploding = true;
            }
            else
            {
                //Show putter out effect, Like a burst of black smoke
            }
            charging = false;
        }
        //if (Input.GetKey(movementKey))
        //{
        //    exploding = true;
        //}
    }
	

    public void blast(Rigidbody2D theRB, GameObject blastPoint, ref double tSinceLastBlast)
    {

        if (exploding == true)
        {
            //I just subtracted the world position of the blastpint from the blaster character to get the position of the thrust and then I modified the power by multiplication.
            theRB.AddForce(new Vector2((theRB.position.x - blastPoint.transform.position.x) * BlastPower, (theRB.position.y - blastPoint.transform.position.y) * BlastPower), ForceMode2D.Impulse);
            exploding = false;
            tSinceLastBlast = Time.deltaTime;
            Debug.Log("Blasted");
            Debug.Log("BlastPower " + BlastPower);
        }
        else
        {
            tSinceLastBlast += Time.deltaTime;
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
    public void UpdateSpendingFuelGage(SpriteRenderer fGuage, float FuelSpent, int CurFuelGage)
    {
        if(FuelSpent != 0 && CurFuelGage > 0)
        {
            if(CurFuelGage > 0)
            {
                if ((CurFuelGage - FuelSpent) < 0)
                {
                    fGuage.transform.localScale = new Vector3(fGuage.transform.localScale.x, 0, fGuage.transform.localScale.z);
                }
                else
                {
                    //.1 will take it down by a tenth leaving it with 10 shots.
                    // fGuage.transform.localScale = new Vector3(fGuage.transform.localScale.x, fGuage.transform.localScale.y - .1f, fGuage.transform.localScale.z);
                    // Dividing whatever value sent by 100 will remove the apporpriate amount
                    fGuage.transform.localScale = new Vector3(fGuage.transform.localScale.x, ((CurFuelGage - FuelSpent) / 10f), fGuage.transform.localScale.z);
                }
            }

            float curCologG = fGuage.color.g * 255;
            // 25 is about 1/10 of 255 which is the range for rgb colors. we divide the fuel amt by 10 to get the amount and multiply it by 25 to figure what needs to be subtracted.
            // This seems like it can be streamlinned if we reformat how we calculate fuel.
            curCologG = curCologG - ((FuelSpent) * 25);
            fGuage.color = new Color(1, curCologG / 255, 0);

        }
    }
    private void UpdateFuelAmount(ref int CurFuelGage, int FuelUsed)
    {
        CurFuelGage -= FuelUsed;
        if(CurFuelGage < 0)
        {
            CurFuelGage = 0;
        }
    }

}
