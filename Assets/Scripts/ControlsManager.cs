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
        Controls.Add("jump", KeyCode.Space);
        Controls.Add("dash", KeyCode.LeftShift);
        Controls.Add("collect", KeyCode.F);
        Controls.Add("ability", KeyCode.R);
    }
}
