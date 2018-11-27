using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public int score;
    public int winAmt;

    public Text ratioText;


    // Use this for initialization
    void Start () {
        ratioText.text = "" + score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void UpdateScore()
    {
        score++;
        if(score == winAmt)
        {
            ratioText.text = "WIN";
        }
        else
        {
            ratioText.text = "" + score;
        }
    }
}
