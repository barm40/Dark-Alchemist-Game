using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{


    // Die when HP reaches 0
    public void DeathSequence()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Continue
    public void Retry()
    {
        Time.timeScale = 1;
        LevelLoader.Loader.LoadNextLevel(SceneManager.GetActiveScene().buildIndex);

        gameObject.SetActive(false);
    }
    
    // exit to main menu
    public void QuitLevel()
    {
        Time.timeScale = 1;
        LevelLoader.Loader.LoadNextLevel(0);
        gameObject.SetActive(false);
    }
}
