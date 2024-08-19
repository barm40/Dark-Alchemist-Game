using System;
using Infra;
using Infra.Channels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DeathMenuManager : TrueSingleton<DeathMenuManager>
    {
        [SerializeField, Header("UI Death Menu"), Tooltip("Add a UI menu to appear when you die")]
        private DeathMenu deathMenu;
        [SerializeField, Header("Health Channel"), Tooltip("Add a Health Channel to listen to")]
        private PlayerHealthChannel healthChannel;

        private bool IsDead { get; set; }
        private void OnEnable()
        {
            healthChannel.HealthEvent += IsAlive;
        }

        private void OnDisable()
        {
            healthChannel.HealthEvent -= IsAlive;
        }

        private void IsAlive(float hp)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
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
