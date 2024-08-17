using System.Collections.Generic;
using Infra;
using UnityEngine;

namespace _managers
{
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
}
