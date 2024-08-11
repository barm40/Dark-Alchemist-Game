using UnityEngine;
using UnityEngine.InputSystem;

public class NextLevel : MonoBehaviour
{
    private bool _interactIntent; 
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_interactIntent) return;
        
        _interactIntent = false;
        Destroy(gameObject);
        LevelLoader.Instance.LoadNextLevel(++LevelLoader.Instance.CurrSceneIndex);
    }

    public void InteractIntent(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            _interactIntent = true;
        }

        if (context.canceled)
        {
            _interactIntent = false;
        }
    }
    
    
}
