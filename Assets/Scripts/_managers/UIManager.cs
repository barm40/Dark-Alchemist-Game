using System;
using Infra;
using Infra.Channels;
using Infra.Patterns;
using TMPro;
using UnityEngine;

namespace _managers
{
    public class UIManager : TrueSingleton<UIManager>
    {
        [SerializeField, Tooltip("Scriptable Object Channel player HP")] 
        private PlayerHealthChannel healthChannel;
    
        [SerializeField, Tooltip("UI Health")]
        private GameObject healthBar;
        private TMP_Text _hpText;
        
        [SerializeField, Tooltip("UI Time")]
        private GameObject timer;
        private TMP_Text _timerText;
        
        private float _time;

        protected override void Awake()
        {
            base.Awake();
            _hpText = healthBar.GetComponent<TMP_Text>();
            _timerText = timer.GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            healthChannel.HealthEvent += UpdateHealth;
        }
    
        private void OnDisable()
        {
            healthChannel.HealthEvent -= UpdateHealth;
        }

        private void Update()
        {
            _timerText.text = TimeSpan.FromSeconds(_time).ToString(@"m\:ss\:ff");
            _time += Time.deltaTime;
        }

        private void UpdateHealth(float hp)
        {
            _hpText.text = $"HP: {(int)hp}";
        }

        private void ResetTime()
        {
            _time = 0;
        }
    }
}
