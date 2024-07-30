using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public Dictionary<string, KeyCode> Controls { get; } = new();
    
    public static ControlsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("An instance of the controls already exists, destroying the newest one");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        Controls.TryAdd("jump", KeyCode.Space);
        Controls.TryAdd("dash", KeyCode.LeftShift);
        Controls.TryAdd("collect", KeyCode.F);
        Controls.TryAdd("ability", KeyCode.R);
        
        DontDestroyOnLoad(gameObject);
    }
}
