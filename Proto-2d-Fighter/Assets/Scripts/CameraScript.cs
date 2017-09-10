using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
	}
}
