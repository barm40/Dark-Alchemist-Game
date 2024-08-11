using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : PersistentSingleton<ControlsManager>
{
    public Dictionary<string, KeyCode> Controls { get; } = new();
    
    protected override void Awake()
    {
        base.Awake();
        
        Controls.TryAdd("jump", KeyCode.Space);
        Controls.TryAdd("dash", KeyCode.LeftShift);
        Controls.TryAdd("collect", KeyCode.F);
        Controls.TryAdd("ability", KeyCode.R);
    }
}
