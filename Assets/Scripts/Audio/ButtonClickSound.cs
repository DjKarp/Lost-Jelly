using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
/// <summary>
/// The script is attached to any one. press the button so that the sound is played when pressed.
/// </summary>

public class ButtonClickSound : ButtonsUI
{
    [SerializeField] private string _handleEvent;
    private string _buttonClick = "event:/UIFX/ButtonClick";
    private Camera _camera;
    public new void Initialization()
    {
        base.Initialization();

        _camera = FindAnyObjectByType<Camera>();

        if (_handleEvent.Length > 0)
            _buttonClick = _handleEvent;
    }

    protected override void ButtonClick()
    {
        if (_camera == null)
            _camera = FindAnyObjectByType<Camera>();

        if (_camera != null)
            RuntimeManager.PlayOneShotAttached(_buttonClick, _camera.gameObject);
    }    
}
