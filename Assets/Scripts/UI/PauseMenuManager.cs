using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// Manages activation of pause menu, since it's disabled by default we can't use its own scripts 
/// </summary>

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        gameObject.SetActive(gameObject.transform.GetChild(0));
        
        pauseMenu.Pause();
    }
}
