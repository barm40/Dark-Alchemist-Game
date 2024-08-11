using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool IsActive { get; private set; }
    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        Resume();
    }

    // Pauses game and opens menu
    public void Pause()
    {
        IsActive = true;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Unpauses the game
    public void Resume()
    {
        IsActive = false;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    // // placeholder for save game
    // public void SaveGame()
    // {
    //     SaveDataManager.Instance.SaveGame();
    // }
    //
    // // placeholder for load game
    // public void LoadGame()
    // {
    //     SaveDataManager.Instance.LoadGame();
    // }
    //
    // exit to main menu
    public void QuitLevel()
    {
        Resume();
        LevelLoader.Instance.LoadNextLevel(0);
    }
}
