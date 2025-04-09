using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : Joystick
{
    public Vector2 GetMoveDirection()
    {
        if (_joystickDirection.x != 0.0f || _joystickDirection.y != 0.0f)
            return _joystickDirection;
        else
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
