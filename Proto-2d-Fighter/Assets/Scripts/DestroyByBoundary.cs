using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
    //Makes it so if a player or attack leaves the preset boundary it is destroyed
    void OnTriggerExit2D(Collider2D other)
    { 
        // GameObject thePlayer = GameObject.Find("Player1");
        //PlayerController pController = thePlayer.GetComponent<PlayerController>();

        //if(other.gameObject.name.StartsWith("iceBolt")){
         //   Debug.Log(other.gameObject.name);
         //   other.gameObject.SetActive(false);
          //  other.gameObject.transform.rotation = pController.AttackSpawn.transform.rotation;
          //  other.gameObject.transform.position = pController.AttackSpawn.transform.position;
            
       // }
       // else
       // {
            Debug.Log(other.gameObject.name);
            Destroy(other.gameObject);
      //  }
    }
}
