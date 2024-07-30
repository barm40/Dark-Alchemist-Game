using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages activation of pause menu, since it's disabled by default we can't use its own scripts 
/// </summary>

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    
    
    public static PauseMenuManager MenuManager { get; private set; }

    private void Awake()
    {
        if (MenuManager != null)
        {
            Debug.Log("An instance of the save manager already exists, destroying the newest one");
            Destroy(gameObject);
            return;
        }

        MenuManager = this;
        
        if (SceneManager.GetActiveScene().buildIndex != 0)
            DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        gameObject.SetActive(gameObject.transform.GetChild(0));
        
        pauseMenu.Pause();
    }
}
