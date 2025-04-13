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

    private Vector2 _direction;

    private void Awake()
    {
        _leftButton.Add(() => _direction = new Vector2(1, 0));
        _rightButton.Add(() => _direction = new Vector2(-1, 0));
        _upButton.Add(() => _direction = new Vector2(0, 1));
        _downButton.Add(() => _direction = new Vector2(0, -1));
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }

    private void OnDisable()
    {
        _leftButton.RemoveAll();
        _rightButton.RemoveAll();
        _upButton.RemoveAll();
        _downButton.RemoveAll();
    }
}
