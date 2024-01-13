using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButton : MonoBehaviour
{
    public void onStartButton()
    {
        //SceneManager.LoadScene(/*Name of screne*/);
    }

    public void onQuitButton()
    {
        Application.Quit();
    }
}
