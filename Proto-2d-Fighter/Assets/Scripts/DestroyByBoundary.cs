using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {
    //Makes it so if a player or attack leaves the preset boundary it is destroyed
    void OnTriggerExit2D(Collider2D other)
    { 
        Destroy(other.gameObject);
    }
}
