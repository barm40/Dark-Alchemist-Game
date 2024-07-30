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

public class LevelLoader : MonoBehaviour, ISaveData
{
    // animation is set to crossfade, but can be changed
    public Animator transition;
    public float transitionTime = 1f; // transition time in seconds (animation is 1 second)
    private static readonly int Start = Animator.StringToHash("Start");
    private static readonly int End = Animator.StringToHash("End");

    public int CurrSceneIndex { get; private set; }

    [SerializeField] private bool debug;
    
    private void Awake()
    {
        CurrSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                var sceneCount = SceneManager.sceneCountInBuildSettings;
                var nextScene = CurrSceneIndex + 1;
                if (nextScene == sceneCount)
                {
                    LoadNextLevel(0);
                }
                else
                {
                    LoadNextLevel(nextScene);
                }
                
            }
        }
    }

    public void LoadNextLevel(int sceneIndex)
    {
        SaveDataManager.Instance.SaveGame();
        
        // perform each line of code in the enumerator in parallel
        StartCoroutine(LoadLevel(sceneIndex));

        StartCoroutine(WaitLoaded());
    }

    // definition of coroutine
    private IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger(Start);

        yield return new WaitForSeconds(transitionTime);
        
        // load given scene
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    
    private IEnumerator WaitLoaded()
    {
        transition.SetTrigger(End);

        yield return new WaitForSeconds(transitionTime);
    }

    public void LoadData(GameData data)
    {
        CurrSceneIndex = data.sceneIndex;
    }

    public void SaveData(GameData data)
    {
        data.sceneIndex = CurrSceneIndex;
    }
}
