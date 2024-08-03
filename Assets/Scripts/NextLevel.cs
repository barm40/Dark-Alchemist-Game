using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(ControlsManager.Instance.Controls["collect"]))
        {
            Destroy(gameObject);
            LevelLoader.Loader.LoadNextLevel(LevelLoader.Loader.CurrSceneIndex + 1);
        }
    }
}
