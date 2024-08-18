using System;
using Infra.Channels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DeathMenuManager : MonoBehaviour
    {
        [SerializeField, Header("UI Death Menu"), Tooltip("Add a UI menu to appear when you die")]
        private DeathMenu deathMenu;
        [SerializeField, Header("Health Channel"), Tooltip("Add a Health Channel to listen to")]
        private PlayerHealthChannel healthChannel;

        private bool IsDead { get; set; }
    
        public static DeathMenuManager MenuManager { get; private set; }

        private void OnEnable()
        {
            healthChannel.HealthEvent += IsAlive;
        }

        private void OnDisable()
        {
            healthChannel.HealthEvent -= IsAlive;
        }

        private void Awake()
        {
            IsDead = false;
        
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

        private void IsAlive(float hp)
        {
            if (hp <= 0)
            {
                Death();
            }
        }
        
        public void Death()
        {
            if (IsDead) return;

            IsDead = true;
        
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        
            deathMenu.DeathSequence();
        }
        
        public void Revive()
        {
            if (!IsDead) return;

            IsDead = false;
        
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            healthChannel.ResetHealth();
        }
    }
}
