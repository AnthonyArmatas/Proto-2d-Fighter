using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameHandlerScript : MonoBehaviour {

    public Canvas EndGameMenu;
    public EndGameMenuScript EGMS;

    // Use this for initialization
    void Start () {
        EndGameMenu = GameObject.Find("EndGameMenu").GetComponent<Canvas>();
        EndGameMenu.enabled = false;
        EGMS = GameObject.Find("EndGameMenu").GetComponent<EndGameMenuScript>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver(string winner)
    {
        DisableGameObjects();
        StaticInfoScript.gameOver = true;
        AnnounceResults(winner);
        ShowEndGameMenu();
    }
    void DisableGameObjects()
    {
        string[] posPlayer1Names = { "IceRider_p1", "Blaster_p1", "Swinger_p1", "Phaser_p1" };
        string[] posPlayer2Names = { "IceRider_p2", "Blaster_p2", "Swinger_p2", "Phaser_p2" };
        string p1 = "";
        string p2 = "";
        foreach (string p1Name in posPlayer1Names)
        {
            //In a try catch because unity will break otherwise. GameObject.Find(p1Name).name returns a crashing error if nothing is found
            try
            {
                if (p1Name == GameObject.Find(p1Name).name)
                {
                    p1 = p1Name;
                }
            }
            catch
            {
            }

        }
        foreach (string p2Name in posPlayer2Names)
        {
            try
            {
                if (p2Name == GameObject.Find(p2Name).name)
                {
                    p2 = p2Name;
                }
            }
            catch
            {
            }

        }
        //MonoBehaviour[] p1Scripts = GameObject.Find(p1).GetComponentsInChildren<MonoBehaviour>();
        // Below goes through each character and disables their script
        foreach (MonoBehaviour component in GameObject.Find(p1).GetComponentsInChildren<MonoBehaviour>())
        {
            component.enabled = false;
        }
        foreach (MonoBehaviour component in GameObject.Find(p2).GetComponentsInChildren<MonoBehaviour>())
        {
            component.enabled = false;
        }

    }
    void AnnounceResults(string winner)
    {
        string player1 = "Player 1";
        string player2 = "Player 2";
        string theWinner = "";
        switch (winner)
        {
            case "PlayerOneScore":
                theWinner = player1;
                break;
            case "PlayerTwoScore":
                theWinner = player2;
                break;
        }

        //Changes the display text on the EndGameMenu
        GameObject.Find("AnnouncementText").GetComponent<Text>().text = changeDisplayText("[PLAYERNAME]", theWinner, GameObject.Find("AnnouncementText").GetComponent<Text>());
        GameObject.Find("AnnouncementText").GetComponent<Text>().text = changeDisplayText("[P1SCORE]", StaticInfoScript.p1curScore.ToString(), GameObject.Find("AnnouncementText").GetComponent<Text>());
        GameObject.Find("AnnouncementText").GetComponent<Text>().text = changeDisplayText("[P2SCORE]", StaticInfoScript.p2curScore.ToString(), GameObject.Find("AnnouncementText").GetComponent<Text>());
        GameObject.Find("AnnouncementText").GetComponent<Text>().text = changeDisplayText("[P1GAMEWINS]", StaticInfoScript.p1AmtOfWins.ToString(), GameObject.Find("AnnouncementText").GetComponent<Text>());
        GameObject.Find("AnnouncementText").GetComponent<Text>().text = changeDisplayText("[P2GAMEWINS]", StaticInfoScript.p2AmtOfWins.ToString(), GameObject.Find("AnnouncementText").GetComponent<Text>());
    }
    string changeDisplayText(string textToReplace, string textReplacement, Text displayText)
    {
        string preChngText = displayText.text;
        return preChngText.Replace(textToReplace, textReplacement);
    }
    void ShowEndGameMenu()
    {
        EndGameMenu.enabled = true;
        EGMS.enabled = true;
    }

    public void FightAgain()
    {
        SceneManager.LoadScene(StaticInfoScript.levelChoiceName);
    }
    public void BackToCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelectScreen");
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Intro");
    }
}
