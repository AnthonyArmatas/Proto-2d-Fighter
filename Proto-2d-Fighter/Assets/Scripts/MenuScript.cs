using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {

    //Each of the buttons to take the user to a different scene
    public Button arcadeBut;
    public Button vsBut;
    public Button settingsBut;
    public Button exitBut;
    //The canvas which appears and disapears when exit is selected
    public Canvas quitMenu;
    //The scripts which manage how selection occurs
    public MenuNavScript MNS;
    public QuitMenuNavScript QMS;





    // Use this for initialization
    void Start () {
		quitMenu = quitMenu.GetComponent<Canvas>();
        //I can use just GetComponent to find the script because it is attached to the same game object that MenuScript is
        MNS = GetComponent<MenuNavScript>();
        //I have to use this GameObject.Find to find the name of the game object that QuitMenuNavScript is attached to before I can actually access it. 
        QMS = GameObject.Find("QuitMenu").GetComponent<QuitMenuNavScript>();
        arcadeBut = GameObject.Find("ArcadeButton").GetComponent<Button>();
        vsBut       = GameObject.Find("VSButton").GetComponent<Button>();
        settingsBut = GameObject.Find("SettingsButton").GetComponent<Button>();
        exitBut     = GameObject.Find("ExitButton").GetComponent<Button>();
        quitMenu.enabled = false;
        StaticInfoScript.p1AmtOfWins = 0;
        StaticInfoScript.p2AmtOfWins = 0;

    }

    void Update()
    {

        //I think this code had a purpose to help make sure exit would be selected or highlighted, but nothing seems to happen when it is commented out
        //IIRC I was experementing to see how to make the scripts for the exit code to work properly but im not 100%. This will probably deleted soon.
        //if(EventSystem.current.currentSelectedGameObject == GameObject.Find("exitBut"))
        //{
        //    EventSystem.current.SetSelectedGameObject(GameObject.Find("exitBut"));
        //}


    }

    public void ExitPress()
    {

            arcadeBut.enabled = false;
            vsBut.enabled = false;
            settingsBut.enabled = false;
            exitBut.enabled = false;
            quitMenu.enabled = true;
            //Changes the enabled status of the scripts when pressed
            QMS.enabled = !QMS.enabled;
            MNS.enabled = !MNS.enabled;
            //These event systems make it so that when the new exit window pops up the no button is highlighted
            EventSystem.current.SetSelectedGameObject(GameObject.Find("NoQuitButton"));

        

    }

    public void NoPress()
    {

            quitMenu.enabled = false;
            arcadeBut.enabled = true;
            vsBut.enabled = true;
            settingsBut.enabled = true;
            exitBut.enabled = true;
            QMS.enabled = !QMS.enabled;
            MNS.enabled = !MNS.enabled;
            //Both Below Work to select the exit button after the exit canvas exits
            //These event systems make it so that when the new exut window closes the exit button is highlighted
            EventSystem.current.SetSelectedGameObject(GameObject.Find("ExitButton"));
            //GameObject.Find("ExitButton").GetComponent<Button>().Select();


    }

    public void StartArcade()
    {
        //I dont remember how exactly I set which scene was which numbered level. Its a little embarressing
        //So now I am using LoadScene
        //Application.LoadLevel(1);
        SceneManager.LoadScene("CharacterSelectScreen");
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }

    public void StartVS()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene("devTestGlidePlatform");

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}