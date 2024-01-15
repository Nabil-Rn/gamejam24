using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScreen : MonoBehaviour
{
    public string sceneToLoad  = "LevelOne";

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
