using System.Collections;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private static void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }

    private void Awake()
    {
        gameObject.SetActive(gameObject);
        StartCoroutine(LoadDialogue());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Pause();
    }

    private IEnumerator LoadDialogue()
    {
        _canvasGroup = LevelLoader.Instance
            .transform.GetChild(0)
            .transform.GetChild(0).GetComponent<CanvasGroup>();
        yield return new WaitUntil(() => _canvasGroup.alpha == 0);
        
    }
}
