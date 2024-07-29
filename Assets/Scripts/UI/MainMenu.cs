using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A script to control the functionality of the main menu
/// </summary>

public class MainMenu : MonoBehaviour
{
    private LevelLoader _levelLoader;

    private void Start()
    {
        // initialize level loader from the sceneloader object
        _levelLoader = GameObject.FindWithTag("sceneLoader").GetComponent<LevelLoader>();
    }
    
    // loads the first level
    public void NewGame()
    {
        _levelLoader.LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // closes the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
