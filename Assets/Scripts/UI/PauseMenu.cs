using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private LevelLoader _levelLoader;

    private void Start()
    {
        // initialize level loader from the sceneloader object
        _levelLoader = GameObject.FindWithTag("sceneLoader").GetComponent<LevelLoader>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        Resume();
    }

    // Pauses game and opens menu
    public void Pause()
    {
        gameObject.SetActive(gameObject);
        Time.timeScale = 0;
    }

    // Unpauses the game
    public void Resume()
    {
        Time.timeScale = 1;
    }

    // placeholder for save game
    public void SaveGame()
    {
        _levelLoader.LoadNextLevel(0);
    }
    
    // placeholder for load game
    public void LoadGame()
    {
        _levelLoader.LoadNextLevel(0);
    }
    
    // exit to main menu
    public void QuitLevel()
    {
        _levelLoader.LoadNextLevel(0);
    }
}
