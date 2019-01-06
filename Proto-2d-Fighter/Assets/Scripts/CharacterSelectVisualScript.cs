using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectVisualScript : MonoBehaviour {

    //This script will only be used to change the visual representation of the users selector.
    //PCtrlCharSeleScript Will handle the actual calculation
    string nameWalker = "Character";
    string p1Overlay = "P1Player_Overlay_transparent";
    string p2Overlay = "P2Player_Overlay_transparent";
    // Use this for initialization
    void Start () {
        SetInitialSelectorPlacement();
    }

    private void SetInitialSelectorPlacement()
    {

        //this.gameObject.transform.GetChild(0).Find(p1Overlay).gameObject.SetActive(true);
        //this.gameObject.transform.GetChild(0).Find(p2Overlay).gameObject.SetActive(true);
        this.gameObject.transform.Find(nameWalker + '1').Find(p1Overlay).gameObject.SetActive(true);
        this.gameObject.transform.Find(nameWalker + '1').Find(p2Overlay).gameObject.SetActive(true);

        for (int i = 2; i <= 4; i++)
        {
            this.gameObject.transform.Find(nameWalker + i).Find(p1Overlay).gameObject.SetActive(false);
            this.gameObject.transform.Find(nameWalker + i).Find(p2Overlay).gameObject.SetActive(false);
        }
    }


    private void moveSelector(int[] selectData)
    {
        //selectData passes in three values
        // 0 = curPos
        // 1 = newPos
        // 2 = player
        if (selectData[2] == 1)
        {
            this.gameObject.transform.Find(nameWalker + selectData[0]).Find(p1Overlay).gameObject.SetActive(false);
            this.gameObject.transform.Find(nameWalker + selectData[1]).Find(p1Overlay).gameObject.SetActive(true);

        }
        else if (selectData[2] == 2)
        {
            this.gameObject.transform.Find(nameWalker + selectData[0]).Find(p2Overlay).gameObject.SetActive(false);
            this.gameObject.transform.Find(nameWalker + selectData[1]).Find(p2Overlay).gameObject.SetActive(true);
        }
    }
    private void lockSelection(int[] selectData)
    {
        //selectData passes in three values
        // 0 = curPos
        // 1 = player
        if (selectData[1] == 1)
        {
            SpriteRenderer sr = this.gameObject.transform.Find(nameWalker + selectData[0]).Find(p1Overlay).gameObject.GetComponent<SpriteRenderer>();
            sr.color = new Color(0, 255, 0, 255);

        }
        else if (selectData[1] == 2)
        {
            SpriteRenderer sr = this.gameObject.transform.Find(nameWalker + selectData[0]).Find(p2Overlay).gameObject.GetComponent<SpriteRenderer>();
            sr.color = new Color(0, 255, 0, 255);
        }
    }
    private void unlockSelection(int[] selectData)
    {
        //selectData passes in three values
        // 0 = curPos
        // 1 = player    
        if (selectData[1] == 1)
        {
            SpriteRenderer sr = this.gameObject.transform.Find(nameWalker + selectData[0]).Find(p1Overlay).gameObject.GetComponent<SpriteRenderer>();
            sr.color = new Color(255, 255, 255, 255);

        }
        else if (selectData[1] == 2)
        {
            SpriteRenderer sr = this.gameObject.transform.Find(nameWalker + selectData[0]).Find(p2Overlay).gameObject.GetComponent<SpriteRenderer>();
            sr.color = new Color(255, 255, 255, 255);
        }
    }

    //This will hide the character selector if the player hits the back button
    private void hideCharacterSelector (int player)
    {
        for (int i = 1; i <= 4; i++)
        {
            //This only works for two player NEED TO MODIFY WHEN ALLOWING FOR MORE
            if (player == 1)
            {
                this.gameObject.transform.Find(nameWalker + i).Find(p1Overlay).gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.transform.Find(nameWalker + i).Find(p2Overlay).gameObject.SetActive(false);
            }
        }
    }
    //This will show the character selector on the fourth character if the player hits the any movment buttons while on the back button
    private void showCharacterSelector(int player)
    {
        if (player == 1)
        {
            this.gameObject.transform.Find(nameWalker + 4).Find(p1Overlay).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.Find(nameWalker + 4).Find(p2Overlay).gameObject.SetActive(true);
        }
    }
}
