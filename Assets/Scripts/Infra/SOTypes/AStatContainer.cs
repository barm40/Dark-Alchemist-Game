using UnityEngine;

namespace Infra.SOTypes
{
    public abstract class AStatContainer<TStats> : ScriptableObject where TStats : IStats
    {
    }

    public interface IStats
    {
    
    }
}