using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public static readonly Dictionary<string, KeyCode> Controls = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Controls.TryAdd("jump", KeyCode.Space);
        Controls.TryAdd("dash", KeyCode.LeftShift);
        Controls.TryAdd("collect", KeyCode.F);
        Controls.TryAdd("ability", KeyCode.R);
    }
}
