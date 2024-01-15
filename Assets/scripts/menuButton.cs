using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButton : MonoBehaviour
{
    public void onStartLevelButton()
    {
        SceneManager.LoadScene("levelOne");
    }

    public void onStartGameButton()
    {
        // SceneManager.LoadScene(/* Name of the dialogue scene*/);
        Debug.Log("start game");
    }
     public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void onMenuButton()
    {
        SceneManager.LoadScene("startScreen");
    }
}
