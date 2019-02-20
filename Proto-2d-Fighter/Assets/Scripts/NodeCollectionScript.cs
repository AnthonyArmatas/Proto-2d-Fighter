using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollectionScript : MonoBehaviour {

    public ScoreScript gameScore;
    public GameObject Node;

    ScoreScript GetPlayerScoreBoard()
    {
        // This Gets the last two characters of the game object (The player characters name) so we can tell which player it belongs to
        //Debug.Log("IN GetPlayerScoreBoard !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //Debug.Log(this.gameObject.name.Substring(Mathf.Max(0, this.gameObject.name.Length - 2)));
        string playerSuffix = "";
        playerSuffix = this.gameObject.name.Substring(Mathf.Max(0, this.gameObject.name.Length - 2));
        if(playerSuffix == "p1")
        {
            //Debug.Log("Setting: " + GameObject.Find("PlayerOneScore").GetComponent<ScoreScript>());
            return GameObject.Find("PlayerOneScore").GetComponent<ScoreScript>();
        }
        else
        {
            //Debug.Log("Setting: " + GameObject.Find("PlayerTwoScore").GetComponent<ScoreScript>());
            return GameObject.Find("PlayerTwoScore").GetComponent<ScoreScript>();
        }
        
    }
    // Use this for initialization
    void Start () {
        gameScore = GetPlayerScoreBoard();
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
