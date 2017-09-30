using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoolScript : MonoBehaviour {
    //Pasuing this code for now
    //The iceballs are triggers and the code to make them work as attacks
    //is built upon that fact.I would have to change that to make the pool work
    //And until the frame rate becomes an issue or I NEED the pool for somthing else
    //Spawning attacks and using them as triggers works for now.
    //
    //IF I do not use triggers remeber: You can use attackPool[attack01Counter].SetActive(true);
    //and attackPool[attack01Counter].SetActive(false); to make the items disapear and appear and still
    //have it have trigger effects oncollide or somthing like that
    //
   /* private int attack01Counter = 0;
    public GameObject[] attackPool = new GameObject[6];

    //GameObject thePlayer = GameObject.Find("Player1");
    //PlayerController pController = thePlayer.GetComponent<PlayerController>();
    //private
	// Called from PlayerController.cs When an attack is made
    public void genAttack()
    {
        if (attack01Counter >= attackPool.Length)
        {
            attack01Counter = 0;
        }
        Debug.Log(attack01Counter);
        Debug.Log(attackPool.Length);
        attackPool[attack01Counter].transform.rotation = transform.rotation;
        attackPool[attack01Counter].transform.position = transform.position;
        attackPool[attack01Counter].SetActive(true);
        attack01Counter += 1;


    }

    */

}
