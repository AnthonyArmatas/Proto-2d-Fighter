using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

    //This script will handle the overarching selection of the level.
    //PCtrlCharSeleScript Will handle the actual calculation
    //LevelSelectVisualScript Will handle the visual changes
    public Canvas continueMenu;
    public PCtrlLvlSleleScript PLCS;
    public LevelSelectVisualScript LSVS;
    public CharacterSelectContScript CSCS;

    // Use this for initialization
    void Start () {
        continueMenu = GameObject.Find("ContinueMenu").GetComponent<Canvas>();
        continueMenu.enabled = false;
        PLCS = GetComponent<PCtrlLvlSleleScript>();
        LSVS = GetComponent<LevelSelectVisualScript>();
        CSCS = GameObject.Find("ContinueMenu").GetComponent<CharacterSelectContScript>();
    }
	
	void Update () {
        if (PLCS.lvlLocked == true && continueMenu.enabled == false)
        {
            //Shows the continue canvas
            continueMenu.enabled = true;
            //Disables the selectiopn script and enables the continue script
            CSCS.enabled = !CSCS.enabled;
            PLCS.enabled = !PLCS.enabled;
            //This eventsystem makes sure the new canvas highlights yes
            EventSystem.current.SetSelectedGameObject(GameObject.Find("YesContinueButton"));

        }
    }
    public void NotReadyPress()
    {

        //Hides the continue canvas
        continueMenu.enabled = false;
        //This eventsystem makes sure the focus is on the Level select canvas and the controller input is not read by the characterselectcontscript
        EventSystem.current.SetSelectedGameObject(GameObject.Find("CharacterFrameHolder"));
        //Disables the continue script and reenables the selectiopn script
        CSCS.enabled = !CSCS.enabled;
        PLCS.enabled = !PLCS.enabled;
        //Sends a message to functionally unlock the selectors
        PLCS.lvlLocked = false;
        //Sends a message to visually unlock the selectors
        LSVS.SendMessage("unlockSelection", PLCS.lvlSelector);
    }
    public void RdyToCont()
    {
        //This sets the chosen level in the static script to place later
        StaticInfoScript.levelChoice = PLCS.lvlSelector;
        Debug.Log("Level Choosen: " + StaticInfoScript.levelChoice);
        Debug.Log("SceneManager.LoadScene(\"genLevel\");");
        //SET AFTER GENLEVEL SCRIPT IS CREATED. CHANGE LOADSCRENE TO SCENES NAME OR MAKE GENLEVELSCENE WHICH GENERATES A LEVEL
        //SceneManager.LoadScene("genLevel");

    }
    public void backPress()
    {
        SceneManager.LoadScene("CharacterSelectScreen");
    }
}
