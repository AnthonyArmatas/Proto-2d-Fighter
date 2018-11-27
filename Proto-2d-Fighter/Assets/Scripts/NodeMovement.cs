using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMovement : MonoBehaviour {

    public float speed;             //the speed in which the node will travel
    private float waitTime;         //How long the node will wait at any point
    public float startWaitTime;     //The initial wait time

    public Transform moveSpots;     //Maybe change this into a matrix (With each map passing in their specific matrix). With each value [0][*] having its corrisponding positions [*][0] so it does not pass through terrain
                                    //This would be a semi stop gap between implimenting roaming movement which bounces off terrian. This would prevent it from getting stuck while still allowing it to be at least semi random.
    public float minX;  //The bounds of the node
    public float maxX;
    public float minY;
    public float maxY;

    // Use this for initialization
    void Start () {
        waitTime = startWaitTime;

        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)); //Sets the first position
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime); 
        if (Vector2.Distance(transform.position, moveSpots.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;

            } else {
                waitTime -= Time.deltaTime;
            }
        }
	}

}
