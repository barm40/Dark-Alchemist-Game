using UnityEngine;

/// <summary>
/// A script to control the functionality of the main menu
/// </summary>

public class MainMenu : MonoBehaviour
{
    private LevelLoader _levelLoader;
    
    private void Awake()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    // public void ContinueGame()
    // {
    //     // load game with existing data
    //     SaveDataManager.Instance.LoadGame();
    // }
    
    // loads the first level
    public void NewGame()
    {
        // Create a new game, initialize all data
        // SaveDataManager.Instance.NewGame();
        // Load gameplay, which will save the game 
        _levelLoader.LoadNextLevel(++LevelLoader.Loader.CurrSceneIndex);
    }

    // closes the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
