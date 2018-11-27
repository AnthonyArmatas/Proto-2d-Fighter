using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollectionScript : MonoBehaviour {

    public ScoreScript gameScore;


    // Use this for initialization
    void Start () {
        gameScore = GameObject.Find("PlayerOneScore").GetComponent<ScoreScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Node")
        {
            gameScore.SendMessage("UpdateScore");
            DestroyObject(coll.gameObject);
            Instantiate(coll.gameObject, new Vector2(0,0), Quaternion.identity);
        }
        /*else if (coll.gameObject.tag == "Slow")
        {

        }*/
        else
            Debug.Log(gameObject.tag);

    }
}
