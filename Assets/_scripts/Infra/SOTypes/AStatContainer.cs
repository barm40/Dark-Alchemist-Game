using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStatContainer<TStats> : ScriptableObject where TStats : IStats
{
}

public interface IStats
{
    
}
