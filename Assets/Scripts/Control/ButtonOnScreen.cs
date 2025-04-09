using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnScreen : MonoBehaviour
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;

    enum ButtonsDirection
    {
        LeftButton, RightButton, UpButton, DownButton
    }

    private Vector2 _direction;

    private void Awake()
    {
        _leftButton.onClick.AddListener(() => ButtonPressed(ButtonsDirection.LeftButton));
        _rightButton.onClick.AddListener(() => ButtonPressed(ButtonsDirection.RightButton));
        _upButton.onClick.AddListener(() => ButtonPressed(ButtonsDirection.UpButton));
        _downButton.onClick.AddListener(() => ButtonPressed(ButtonsDirection.DownButton));
    }

    private void ButtonPressed(ButtonsDirection buttonsDirection)
    {
        switch (buttonsDirection)
        {
            case ButtonsDirection.RightButton: _direction = new Vector2(1, 0); break;
            case ButtonsDirection.LeftButton: _direction = new Vector2(-1, 0); break;
            case ButtonsDirection.UpButton: _direction = new Vector2(0, 1); break;
            case ButtonsDirection.DownButton: _direction = new Vector2(0, -1); break;
        }
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }
}
