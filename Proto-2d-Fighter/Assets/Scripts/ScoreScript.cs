using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public GameHandlerScript GHS;

    public int score;
    public int winAmt;

    public Text ratioText;


    // Use this for initialization
    void Start () {
        ratioText.text = "" + score;
        StaticInfoScript.ResetScores();
        winAmt = StaticInfoScript.devLevelWinAmnt;
        GHS = GameObject.Find("EventSystem").GetComponent<GameHandlerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void UpdateScore()
    {
        score++;
        switch (this.gameObject.name)
        {
            case "PlayerOneScore":
                StaticInfoScript.p1curScore = score;
                break;
            case "PlayerTwoScore":
                StaticInfoScript.p2curScore = score;
                break;
        }
        if (score >= winAmt)
        {
            switch (this.gameObject.name)
            {
                case "PlayerOneScore":
                    StaticInfoScript.p1AmtOfWins++;
                    break;
                case "PlayerTwoScore":
                    StaticInfoScript.p2AmtOfWins++;
                    break;
            }
            ratioText.text = "WIN";
            GHS.GameOver(this.gameObject.name);
        }
        else
        {
            ratioText.text = "" + score;
        }
    }
}
