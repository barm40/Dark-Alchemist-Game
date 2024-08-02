using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private static readonly int End = Animator.StringToHash("End");
    
    public int CurrSceneIndex { get; set; }
    
    
    [SerializeField] private bool debug;
    public static LevelLoader Loader { get; private set; }
    
    private void Awake()
    {
        if (Loader != null)
        {
            Debug.Log("An instance of the level loader already exists, destroying the newest one");
            Destroy(gameObject);
            return;
        }

        Loader = this;
        
        DontDestroyOnLoad(gameObject);
        
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
        // SaveDataManager.Instance.SaveGame();
        
        // perform each line of code in the enumerator in parallel
        StartCoroutine(LoadLevel(sceneIndex));
    }

    // definition of coroutine
    private IEnumerator LoadLevel(int sceneIndex)
    {
        transition.SetTrigger(End);

        yield return new WaitForSeconds(transitionTime);
        
        // load given scene
        SceneManager.LoadScene(sceneIndex);
        
        transition.SetTrigger(Start);
    }
}
