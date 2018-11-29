using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollectionScript : MonoBehaviour {

    public ScoreScript gameScore;
    public GameObject Node;

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
            Invoke("RespawnNode", 1.5f);            


        }
        /*else if (coll.gameObject.tag == "Slow")
        {

        }*/
        else
            Debug.Log(gameObject.tag);

    }

    private void RespawnNode()
    {
        Instantiate(Node, new Vector2(0, 0), Quaternion.identity);
    }

}
