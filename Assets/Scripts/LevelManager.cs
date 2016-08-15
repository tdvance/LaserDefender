using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static bool autoPlay = false;
    public bool lost = false;
    public static int score = 0;

    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for: " + name);
        if (name.Equals("Start"))
        {
            lost = false;
            score = 0;
        }
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void PauseLevel()
    {
        Debug.Log("Pause requested");
        LoadLevel("Start");
    }

   
}
