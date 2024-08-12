using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("Scriptable Object Channel player HP")] 
    private PlayerHealthChannel healthChannel;
    
    private TMP_Text _hpText;
    private TMP_Text _timerText;
    private float _time;

    private void Awake()
    {
        _hpText = GameObject.FindGameObjectWithTag("hpText").GetComponent<TMP_Text>();
        _timerText = GameObject.FindGameObjectWithTag("timerText").GetComponent<TMP_Text>();
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
}
