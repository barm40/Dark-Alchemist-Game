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
        LevelLoader.Instance.LoadNextLevel(LevelLoader.Instance.CurrSceneIndex);
    }
    
    // exit to main menu
    public void QuitLevel()
    {
        Time.timeScale = 1;
        LevelLoader.Instance.LoadNextLevel(0);
    }
}
