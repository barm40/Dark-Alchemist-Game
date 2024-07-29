using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// This script manages a few things:
/// It allows to load the next level when calling the LoadNextLevel function
/// It has a game debug mode that allows moving to next scene by pressing a key
/// It triggers a fade animation when exiting and loading screens
/// </summary>

public class LevelLoader : MonoBehaviour
{
    // animation is set to crossfade, but can be changed
    public Animator transition;
    public float transitionTime = 1f; // transition time in seconds (animation is 1 second)
    private static readonly int Start = Animator.StringToHash("Start");

    [SerializeField] private bool debug;

    private void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                var sceneCount = SceneManager.sceneCountInBuildSettings;
                var nextScene = SceneManager.GetActiveScene().buildIndex;
                if (nextScene == sceneCount)
                {
                    LoadNextLevel(0);
                }
                else
                {
                    LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
                }
                
            }
        }
    }

    public void LoadNextLevel(int sceneIndex)
    {
        // perform each line of code in the enumerator in paralle
        StartCoroutine(LoadLevel(sceneIndex));
    }

    // definition of coroutine
    IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger(Start);

        yield return new WaitForSeconds(transitionTime);
        
        // load given scene
        SceneManager.LoadScene(sceneIndex);
    }
}
