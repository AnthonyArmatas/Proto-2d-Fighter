using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class CharacterSelectScript : MonoBehaviour {

    //This script will handle the overarching selection of characters.
    //PCtrlCharSeleScript Will handle the actual calculation
    //CharacterSelectVisualScript Will handle the visual changes
    public Canvas continueMenu;
    public PCtrlCharSeleScript PCCS;
    public CharacterSelectVisualScript CSVS;
    public CharacterSelectContScript CSCS;


    void Start () {
        continueMenu = GameObject.Find("ContinueMenu").GetComponent<Canvas>();
        continueMenu.enabled = false;
        PCCS = GetComponent<PCtrlCharSeleScript>();
        CSVS = GetComponent<CharacterSelectVisualScript>();
        CSCS = GameObject.Find("ContinueMenu").GetComponent<CharacterSelectContScript>();

    }

    void Update()
    {
        if(PCCS.p1Locked == true && PCCS.p2Locked == true && continueMenu.enabled == false)
        {
            //Shows the continue canvas
            continueMenu.enabled = true;
            //Disables the selectiopn script and enables the continue script
            CSCS.enabled = !CSCS.enabled;
            PCCS.enabled = !PCCS.enabled;
            //This eventsystem makes sure the new canvas highlights yes
            EventSystem.current.SetSelectedGameObject(GameObject.Find("YesContinueButton"));

        }
    }


    public void NotReadyPress()
    {

        //Hides the continue canvas
        continueMenu.enabled = false;
        //This eventsystem makes sure the focus is on the character select canvas and the controller input is not read by the characterselectcontscript
        EventSystem.current.SetSelectedGameObject(GameObject.Find("CharacterFrameHolder"));
        //Disables the continue script and reenables the selectiopn script
        CSCS.enabled = !CSCS.enabled;
        PCCS.enabled = !PCCS.enabled;
        //Sends a message to functionally unlock the selectors
        PCCS.p1Locked = false;
        PCCS.p2Locked = false;
        //Sends a message to visually unlock the selectors
        CSVS.SendMessage("unlockSelection", new int[] { PCCS.p1CharSelector, 1 });
        CSVS.SendMessage("unlockSelection", new int[] { PCCS.p2CharSelector, 2 });
    }

    public void RdyToCont()
    {
        //Debug.Log("P1 Choose Character Before: " + StaticInfoScript.p1CharacterChoice);
        //Debug.Log("P2 Choose Character Before: " +  StaticInfoScript.p2CharacterChoice);
        //This sets the chosen characters in the static script to place later
        StaticInfoScript.p1CharacterChoice = PCCS.p1CharSelector;
        StaticInfoScript.p2CharacterChoice = PCCS.p2CharSelector;
        //Debug.Log("P1 Choose Character After: " + StaticInfoScript.p1CharacterChoice);
        //Debug.Log("P2 Choose Character After: " + StaticInfoScript.p2CharacterChoice);

    }

        public void backPress()
    {
        SceneManager.LoadScene("intro");
    }


}
