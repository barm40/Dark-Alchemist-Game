using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{


    // Die when HP reaches 0
    public void DeathSequence()
    {
        Time.timeScale = 0;
    }

    // Continue
    public void Retry()
    {
        Time.timeScale = 1;
        LevelLoader.Loader.LoadNextLevel(LevelLoader.Loader.CurrSceneIndex);
    }
    
    // exit to main menu
    public void QuitLevel()
    {
        Time.timeScale = 1;
        LevelLoader.Loader.LoadNextLevel(0);
    }
}
