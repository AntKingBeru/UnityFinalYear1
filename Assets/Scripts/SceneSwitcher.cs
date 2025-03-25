using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadMain()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMaps()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadForest()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGuide()
    {
        SceneManager.LoadScene(3);
    }
}
