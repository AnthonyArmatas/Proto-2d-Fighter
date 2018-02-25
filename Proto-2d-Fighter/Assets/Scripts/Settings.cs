using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Button mainMenuButton;

    // Use this for initialization
    void Start()
    {
        mainMenuButton = mainMenuButton.GetComponent<Button>();
    }

    public void MainMenu()
    {
        // Main Menu
        SceneManager.LoadScene("intro");
    }
}