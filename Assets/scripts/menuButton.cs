using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButton : MonoBehaviour
{
    public void onStartLevelButton()
    {
        SceneManager.LoadScene("level1");
    }

    public void onStartGameButton()
    {
        // SceneManager.LoadScene(/* Name of the dialogue scene*/);
        Debug.Log("start game");
    }
    public void onQuitButton()
    {
        Application.Quit();
    }

    public void onMenuButton()
    {
        SceneManager.LoadScene("startScreen");
    }
}
