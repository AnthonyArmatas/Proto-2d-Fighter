using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeControllerScript : MonoBehaviour {
    private Animator spikeTor;
    // Use this for initialization
    private int collCounter = 0;
    private int priorcollCounter = 0;
    private GameObject obj;

    public GameObject iceSpike;

    void Start()
    {
        spikeTor = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    { ///////////// NEED TO FIX THE DAMAGE
        //Debug.Log("got to collider");

        //Debug.Log(gameObject.tag);
        if (coll.gameObject.tag == "Terrain" || coll.gameObject.tag == "Player")
        {
            if (coll.gameObject.tag == "Terrain")
            {
                spikeTor.SetBool("Attack_SB", true);
                collCounter++;
            }
            if (coll.gameObject.tag == "Player")
            {
                coll.gameObject.SendMessage("TakeDamage", 10);
                //DestroyObject(this.gameObject);
                Debug.Log(gameObject.tag);
            }
            // Debug.Log("Touching the floor");

        }
        else if (collCounter == priorcollCounter)
        {
            Debug.Log("Returned");
            obj = GameObject.Find("iceSpike(Clone)");
            obj.SetActive(false);
            
        }
        else
        {
            priorcollCounter++;
        }

    }

    public void SpawnIceSpike(Transform iceSpkPos)
    {
        Debug.Log(collCounter);
        Debug.Log(priorcollCounter);
        Instantiate(iceSpike, iceSpkPos);
    }
}
