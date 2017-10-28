using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button startText;
    public Button vsText;
    public Button exitText;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
        startText = startText.GetComponent<Button>();
        vsText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

        quitMenu.enabled = false;

	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        vsText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        vsText.enabled = true;
        exitText.enabled = true;
    }

    public void StartArcade()
    {
        //I dont remember how exactly I set which scene was which numbered level. Its a little embarressing
        //So now I am using LoadScene
        //Application.LoadLevel(1);
        SceneManager.LoadScene("characterSelect");
    }

    public void StartVS()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene("devTest");

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
