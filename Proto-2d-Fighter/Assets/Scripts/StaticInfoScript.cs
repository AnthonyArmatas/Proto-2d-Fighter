using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInfoScript : MonoBehaviour {

    //Default Values are set first
    static public KeyCode p1Up = KeyCode.W;
    static public KeyCode p1Down = KeyCode.S;
    static public KeyCode p1Left = KeyCode.A;
    static public KeyCode p1Right = KeyCode.D;
    static public KeyCode p1LockIn = KeyCode.N;
    static public KeyCode p1Backout = KeyCode.M;
    static public KeyCode p2Up = KeyCode.UpArrow;
    static public KeyCode p2Down = KeyCode.DownArrow;
    static public KeyCode p2Left = KeyCode.LeftArrow;
    static public KeyCode p2Right = KeyCode.RightArrow;
    static public KeyCode p2LockIn = KeyCode.Keypad0;
    static public KeyCode p2Backout = KeyCode.KeypadPeriod;
    static public int p1CharacterChoice;
    static public int p2CharacterChoice;
    static public int levelChoice;
    static public string levelChoiceName;
    static public int p1curScore;
    static public int p2curScore;
    static public int p1AmtOfWins;
    static public int p2AmtOfWins;
    static public bool gameOver;

    static public int devLevelWinAmnt = 1;

    public static void ResetWinAmt()
    {
        p1AmtOfWins = 0;
        p2AmtOfWins = 0;
    }
    public static void ResetScores()
    {
        p1curScore = 0;
        p2curScore = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
