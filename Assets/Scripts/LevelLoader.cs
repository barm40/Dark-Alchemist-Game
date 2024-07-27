using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // animation is set to crossfade, but can be changed
    public Animator transition;
    public float transitionTime = 1f; // transition time in seconds (animation is 1 second)
    private static readonly int Start = Animator.StringToHash("Start");

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
