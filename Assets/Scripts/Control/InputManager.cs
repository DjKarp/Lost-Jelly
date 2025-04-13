using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private JoystickMove m_JoystickMove;
    [SerializeField] private ButtonOnScreen m_ButtonOnScreen;

    private Vector2 _direction;

    private Vector2 _tempInputDirection;
    private Vector2 _tempJoystickDirection;

    private void Awake()
    {
        if (m_JoystickMove == null)
            m_JoystickMove = gameObject.GetComponentInChildren<JoystickMove>();
        if (m_ButtonOnScreen == null)
            m_ButtonOnScreen = gameObject.GetComponentInChildren<ButtonOnScreen>();
    }

    public Vector2 GetMoveDirection()
    {
        _tempInputDirection = _tempJoystickDirection = Vector2.zero;
        _tempInputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _tempJoystickDirection = m_JoystickMove.GetMoveDirection();

        if (_tempInputDirection == Vector2.zero && _tempJoystickDirection == Vector2.zero)
        {
            _tempJoystickDirection = m_ButtonOnScreen.GetDirection();
            _direction = _tempJoystickDirection.x != 0 && _tempJoystickDirection.y != 0 ? _tempJoystickDirection : _direction;
        }
        else
        {
            _tempInputDirection = _tempJoystickDirection == Vector2.zero ? _tempInputDirection : _tempJoystickDirection;
            _direction = Mathf.Abs(_tempInputDirection.x) > Mathf.Abs(_tempInputDirection.y) ? new Vector2(_tempInputDirection.x > 0 ? 1 : -1, 0) : new Vector2(0, _tempInputDirection.y > 0 ? 1 : -1);
        }

        return _direction;
    }
}
