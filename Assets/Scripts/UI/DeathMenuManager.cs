using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class DeathMenuManager : MonoBehaviour
{
    [SerializeField] private DeathMenu deathMenu;
    public static DeathMenuManager MenuManager { get; private set; }

    private void Awake()
    {
        if (MenuManager != null)
        {
            Debug.Log("An instance of the save manager already exists, destroying the newest one");
            Destroy(gameObject);
            return;
        }
        MenuManager = this;
        
        if (SceneManager.GetActiveScene().buildIndex != 0)
            DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    public void Death()
    {
        gameObject.SetActive(gameObject.transform.GetChild(0));
        
        deathMenu.DeathSequence();
    }
    
    
}
