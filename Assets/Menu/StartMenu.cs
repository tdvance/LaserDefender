using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : GenericMenu
{
    new void StartGame()
    {
        Debug.Log("Start Game: " + gameScreen);
        SceneManager.LoadScene(gameScreen);
    }

    new void NextScreen()
    {
        Debug.Log("Next Screen: " + nextScreen);
        SceneManager.LoadScene(nextScreen);
    }
}
