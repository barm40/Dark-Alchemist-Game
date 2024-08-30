using System;
using _managers;
using Infra.Channels;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class NextLevel : MonoBehaviour
{
    [Header("Input Channel")] [SerializeField, Tooltip("Add Player Input Channel Scriptable Object")]
    private PlayerInputChannel input;
    
    private bool _interactIntent;

    private void OnEnable()
    {
        input.InteractEvent += InteractIntent;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_interactIntent) return;
        
        _interactIntent = false;
        Destroy(gameObject);
        LevelLoader.Instance.LoadNextLevel(++LevelLoader.Instance.CurrSceneIndex);
    }

    public void InteractIntent(bool intent)
    {
        _interactIntent = intent;
    }
    
    
}
