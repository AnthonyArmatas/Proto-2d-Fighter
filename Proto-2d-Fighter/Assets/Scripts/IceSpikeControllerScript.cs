using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeControllerScript : MonoBehaviour {
    private Animator spikeTor;


    // Use this for initialization
    void Start()
    {
        spikeTor = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    { ///////////// NEED TO FIX THE DAMAGE
        Debug.Log("got to collider");

        Debug.Log(gameObject.tag);
        if (coll.gameObject.tag == "Terrain")
        {
            spikeTor.SetBool("Attack_SB", true);

            Debug.Log("Touching the floor");
            if (coll.gameObject.tag == "Player")
            {
                coll.gameObject.SendMessage("TakeDamage", 10);
                DestroyObject(this.gameObject);
                Debug.Log(gameObject.tag);
            }
            else
                Debug.Log(gameObject.tag);

        }

    }
}
