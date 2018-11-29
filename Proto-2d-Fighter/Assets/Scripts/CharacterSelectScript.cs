using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelectScript : MonoBehaviour {

    //This script will only be used to change the visual representation of the users selector.
    //PCtrlCharSeleScript Will handle the actual calculation
    void Start () {
        SetInitialSelectorPlacement();
    }

    void Update()
    {

    }

    private void SetInitialSelectorPlacement()
    {
        string nameWalker = "Character";
        string p1Overlay = "P1Player_Overlay_transparent";
        string p2Overlay = "P2Player_Overlay_transparent";
        //this.gameObject.transform.GetChild(0).Find(p1Overlay).gameObject.SetActive(true);
        //this.gameObject.transform.GetChild(0).Find(p2Overlay).gameObject.SetActive(true);
        this.gameObject.transform.Find(nameWalker + '1').Find(p1Overlay).gameObject.SetActive(true);
        this.gameObject.transform.Find(nameWalker + '1').Find(p2Overlay).gameObject.SetActive(true);

        for(int i = 2; i <= 4; i++)
        {
            this.gameObject.transform.Find(nameWalker + i).Find(p1Overlay).gameObject.SetActive(false);
            this.gameObject.transform.Find(nameWalker + i).Find(p2Overlay).gameObject.SetActive(false);
        }
    }

    private void moveLeft()
    {

    }
    private void moveright()
    {

    }
    private void moveUp()
    {

    }
    private void moveDown()
    {

    }
}
