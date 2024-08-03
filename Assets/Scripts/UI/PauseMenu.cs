using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        Resume();
    }

    // Pauses game and opens menu
    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Unpauses the game
    public void Resume()
    {
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
        LevelLoader.Loader.LoadNextLevel(0);
    }
}
