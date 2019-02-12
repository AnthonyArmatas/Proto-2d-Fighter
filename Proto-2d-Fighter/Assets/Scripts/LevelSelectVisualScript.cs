using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectVisualScript : MonoBehaviour {

    //This script will only be used to change the visual representation of the users selector.
    //PCtrlCharSeleScript Will handle the actual calculation
    string nameWalker = "Level";
    string Overlay = "BackGroundHighlight";
    //string p2Overlay = "P2Player_Overlay_transparent";
    // Use this for initialization
    void Start () {
        SetInitialSelectorPlacement();
    }

    private void SetInitialSelectorPlacement()
    {

        this.gameObject.transform.Find(nameWalker + '1').Find(Overlay).gameObject.SetActive(true);
        //this.gameObject.transform.Find(nameWalker + '1').Find(p2Overlay).gameObject.SetActive(true);

        for (int i = 2; i <= 3; i++)
        {
            this.gameObject.transform.Find(nameWalker + i).Find(Overlay).gameObject.SetActive(false);
            //this.gameObject.transform.Find(nameWalker + i).Find(p2Overlay).gameObject.SetActive(false);
        }
    }

    private void moveSelector(int[] selectData)
    {
        this.gameObject.transform.Find(nameWalker + selectData[0]).Find(Overlay).gameObject.SetActive(false);
        this.gameObject.transform.Find(nameWalker + selectData[1]).Find(Overlay).gameObject.SetActive(true);
    }

    private void lockSelection(int selectData)
    {
        SpriteRenderer sr = this.gameObject.transform.Find(nameWalker + selectData).Find(Overlay).gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(0, 255, 0, 255);
    }

    private void unlockSelection(int selectData)
    {
        SpriteRenderer sr = this.gameObject.transform.Find(nameWalker + selectData).Find(Overlay).gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(255, 255, 0, 255);
    }

    private void hideLevelSelector(int selectData)
    {
        this.gameObject.transform.Find(nameWalker + selectData).Find(Overlay).gameObject.SetActive(false);
    }

    private void showLevelSelector(int selectData)
    {
        this.gameObject.transform.Find(nameWalker + selectData).Find(Overlay).gameObject.SetActive(true);
    }
}
