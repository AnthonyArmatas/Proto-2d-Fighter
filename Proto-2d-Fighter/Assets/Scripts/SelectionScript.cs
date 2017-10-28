using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour {

    public Canvas CharReadyText;
    public Button backToMain;


	// Use this for initialization
	void Start () {
        CharReadyText = CharReadyText.GetComponent<Canvas>();
        backToMain = backToMain.GetComponent<Button>();

        CharReadyText.enabled = false;
	}

    public void backPress()
    {
        SceneManager.LoadScene("intro");
    }
}
