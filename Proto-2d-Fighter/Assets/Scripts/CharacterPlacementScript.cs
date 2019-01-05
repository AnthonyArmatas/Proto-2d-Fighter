using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPlacementScript : MonoBehaviour {

    //private GameObject;
    CharacterSelectScript CSS;
    GameObject charPanel;
    private SpriteRenderer spriteImg;
    
	// Use this for initialization
	void Start () {
        charPanel = gameObject;//.transform.parent.gameObject.name
        charPanel = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        //Debug.Log("Currents name " + charPanel.name);
        CSS = charPanel.GetComponent<CharacterSelectScript>();
        //Debug.Log("CharacterPlacement script it in " + gameObject.name);
        //Debug.Log("This is CSS " + CSS);
        spriteImg = gameObject.GetComponent<SpriteRenderer>();//.sprite = CSS.spriteList[0];
        charPanel = gameObject;
        //Debug.Log("Currents name " + charPanel.name);
       /* for(int i = 0; i < CSS.spriteList.Length;i++){
            if (charPanel.name.Equals("Character" + (i+1)))
            {
                spriteImg.sprite = CSS.spriteList[i];
            }
        } */

        //spriteImg.sprite = CSS.spriteList[1];

 	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
