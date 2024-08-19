using Infra;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// Manages activation of pause menu, since it's disabled by default we can't use its own scripts 
    /// </summary>

    public class PauseMenuManager : TrueSingleton<PauseMenuManager>
    {
        [SerializeField] private PauseMenu pauseMenu;
        
        // Update is called once per frame
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || pauseMenu.IsActive || SceneManager.GetActiveScene().buildIndex == 0) return;
        
            gameObject.SetActive(gameObject.transform.GetChild(0));
        
            pauseMenu.Pause();
        }
    }
}
