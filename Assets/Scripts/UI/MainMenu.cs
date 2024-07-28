using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private LevelLoader _levelLoader;

    private void Start()
    {
        // initialize level loader from the sceneloader object
        _levelLoader = GameObject.FindWithTag("sceneLoader").GetComponent<LevelLoader>();
    }
    public void NewGame()
    {
        _levelLoader.LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
