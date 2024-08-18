using _managers;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// A script to control the functionality of the main menu
    /// </summary>

    public class MainMenu : MonoBehaviour
    {
        // loads the first level
        public void NewGame()
        {
            // Create a new game, initialize all data
            // SaveDataManager.Instance.NewGame();
            // Load gameplay, which will save the game 
            LevelLoader.Instance.LoadNextLevel(++LevelLoader.Instance.CurrSceneIndex);
        }

        // closes the game
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
