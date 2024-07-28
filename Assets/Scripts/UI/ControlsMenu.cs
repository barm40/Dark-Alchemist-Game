using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private bool _listening = false;
    private string _controlName = "";

    private void Update()
    {
        if (_listening)
        {
            SetControls(_controlName);
        }
    }

    public void Awake()
    {
        foreach (var control in ControlsManager.Controls)
        {
            foreach (var button in buttons)
            {
                if (button.name == control.Key + "Button")
                {
                    button.GetComponentsInChildren<TMP_Text>()[0].text = control.Value.ToString();
                }
            }
        }
    }

    public void SetControls(string control)
    {
        _listening = true;
        _controlName = control;
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (!Input.GetKeyDown(key)) continue;
            ControlsManager.Controls[control] = key;
            _listening = false;
            foreach (var button in buttons)
            {
                if (button.name == control + "Button")
                {
                    button.GetComponentsInChildren<TMP_Text>()[0].text = key.ToString();
                }
            }
        }
    }
}
